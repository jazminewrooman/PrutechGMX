using System;
using Xamarin.Forms;
using GMX.SegmentedControl.Android;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Views;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(GMX.SegmentedControl.SegmentedControl), typeof(SegmentedControlRenderer))]
namespace GMX.SegmentedControl.Android
{
	/// <summary>
	/// SegmentedControl Renderer
	/// </summary>
	public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, RadioGroup>
	{
		static RadioGroup nativeControl;
		RadioButton _v;

		protected override void OnElementChanged(ElementChangedEventArgs<SegmentedControl> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				// Instantiate the native control and assign it to the Control property with
				// the SetNativeControl method
                Element_SizeChanged();
			}

			if (e.OldElement != null)
			{
				// Unsubscribe from event handlers and cleanup any resources

				if (nativeControl != null)
					nativeControl.CheckedChange -= NativeControl_ValueChanged;

				if (Element != null)
					Element.SizeChanged -= Element_SizeChanged;
			}

			if (e.NewElement != null)
			{
                // Configure the control and subscribe to event handlers

                Element.SizeChanged += Element_SizeChanged;
                Element_SizeChanged();
			}
		}

        void Element_SizeChanged ()
		{
			try
			{
				var layoutInflater = LayoutInflater.From(Forms.Context);

				nativeControl = (RadioGroup)layoutInflater.Inflate(Resource.Layout.RadioGroup, null);

				for (var i = 0; i < Element.Children.Count; i++)
				{
					var o = Element.Children[i];
					var v = (RadioButton)layoutInflater.Inflate(Resource.Layout.RadioButton, null);

					v.LayoutParameters = new RadioGroup.LayoutParams(0, LayoutParams.WrapContent, 1f);
					v.Text = o.Text;

					if (i == 0)
						v.SetBackgroundResource(Resource.Drawable.segmented_control_first_background);
					else if (i == Element.Children.Count - 1)
						v.SetBackgroundResource(Resource.Drawable.segmented_control_last_background);

					ConfigureRadioButton(i, v);

					nativeControl.AddView(v);
				}

				var option = (RadioButton)nativeControl.GetChildAt(Element.SelectedSegment);

				option.Checked = true;

				nativeControl.CheckedChange += NativeControl_ValueChanged;

				SetNativeControl(nativeControl);
			}
			catch (ObjectDisposedException oex)
			{
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			//var layoutInflater = LayoutInflater.From(Forms.Context);
			try
			{
				//nativeControl = (RadioGroup)layoutInflater.Inflate(Resource.Layout.RadioGroup, null);
				switch (e.PropertyName)
				{
					case "Renderer":
						Element.ValueChanged?.Invoke(Element, null);
						break;
					case "SelectedSegment":
						var option = (RadioButton)nativeControl.GetChildAt(Element.SelectedSegment);
						option.Checked = true;
						Element.ValueChanged?.Invoke(Element, null);
						break;
					case "TintColor":
						OnPropertyChanged();
						break;
					case "IsEnabled":
						OnPropertyChanged();
						break;
					case "SelectedTextColor":
						var v = (RadioButton)nativeControl.GetChildAt(Element.SelectedSegment);
						v.SetTextColor(Element.SelectedTextColor.ToAndroid());
						break;
				}
			}
			catch (Exception ex)
			{
			}
		}

		void OnPropertyChanged()
		{
			try
			{
				for (var i = 0; i < Element.Children.Count; i++)
				{
					var v = (RadioButton)nativeControl.GetChildAt(i);

					ConfigureRadioButton(i, v);
				}
			}
			catch (Exception ex)
			{
			}
		}

		void ConfigureRadioButton(int i, RadioButton v)
		{
			if (i == Element.SelectedSegment)
			{
				v.SetTextColor(Element.SelectedTextColor.ToAndroid());
				_v = v;
			}
			else
			{
				var textColor = Element.IsEnabled ? Element.TintColor.ToAndroid() : Color.Gray.ToAndroid();
				v.SetTextColor(textColor);
			}

			GradientDrawable selectedShape;
			GradientDrawable unselectedShape;

			var gradientDrawable = (StateListDrawable)v.Background;
			var drawableContainerState = (DrawableContainer.DrawableContainerState)gradientDrawable.GetConstantState();
			var children = drawableContainerState.GetChildren();

			// Doesnt works on API < 18
			selectedShape = children[0] is GradientDrawable? (GradientDrawable)children[0] : (GradientDrawable)((InsetDrawable)children[0]).Drawable;
			unselectedShape = children[1] is GradientDrawable? (GradientDrawable)children[1] : (GradientDrawable)((InsetDrawable)children[1]).Drawable;

			var color = Element.IsEnabled ? Element.TintColor.ToAndroid() : Color.Gray.ToAndroid();

			selectedShape.SetStroke(3, color);
			selectedShape.SetColor(color);
			unselectedShape.SetStroke(3, color);

			v.Enabled = Element.IsEnabled;
		}

		void NativeControl_ValueChanged(object sender, RadioGroup.CheckedChangeEventArgs e)
		{
			var rg = (RadioGroup)sender;
			if (rg.CheckedRadioButtonId != -1)
			{
				var id = rg.CheckedRadioButtonId;
				var radioButton = rg.FindViewById(id);
				var radioId = rg.IndexOfChild(radioButton);

				var v = (RadioButton)rg.GetChildAt(radioId);

				var color = Element.IsEnabled ? Element.TintColor.ToAndroid() : Color.Gray.ToAndroid();
				_v.SetTextColor(color);
				v.SetTextColor(Element.SelectedTextColor.ToAndroid());
				_v = v;

				Element.SelectedSegment = radioId;
			}
		}

        private void Element_SizeChanged(object sender, EventArgs ea){
            Element_SizeChanged();   
        }

		protected override void OnAttachedToWindow()
		{
			// I gave up and added this ugly catch
			// because:
			// - the Element is not null, 
			// - the native Handle is not null,
			// - Dispose() has not been called yet,
			// but still the base crashed with ObjectDisposedException
			// when it tried to read Context of Android View
			try
			{
				base.OnAttachedToWindow();
			}
			catch (ObjectDisposedException) { }
		}

		protected override void Dispose(bool disposing)
		{
			if (nativeControl != null)
			{
				nativeControl.CheckedChange -= NativeControl_ValueChanged;
				nativeControl.Dispose();
				nativeControl = null;
				_v = null;	
			}

			if (Element != null)
				Element.SizeChanged -= Element_SizeChanged;

			try
			{
				base.Dispose(disposing);
				GC.Collect();
			}
			catch (Exception ex)
			{
				return;
			}
		}
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() {
			var temp = DateTime.Now;
		}
    }
}