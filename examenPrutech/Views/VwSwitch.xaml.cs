using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace GMX.Views
{
    public partial class VwSwitch : ContentView
    {
		public string Title
		{
			get { return GetValue(TitleProperty).ToString(); }
			set { base.SetValue(TitleProperty, value); }
		}
		public static readonly BindableProperty TitleProperty = BindableProperty.Create(propertyName: "Title", returnType: typeof(string), declaringType: typeof(VwOpcion), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

		public string Detail
		{
			get { return GetValue(DetailProperty).ToString(); }
			set { base.SetValue(DetailProperty, value); }
		}
		public static readonly BindableProperty DetailProperty = BindableProperty.Create(propertyName: "Detail", returnType: typeof(string), declaringType: typeof(VwOpcion), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);


		public string IdDetail
		{
			get { return GetValue(IdDetailProperty).ToString(); }
			set { base.SetValue(IdDetailProperty, value); }
		}
		public static readonly BindableProperty IdDetailProperty = BindableProperty.Create(propertyName: "IdDetail", returnType: typeof(string), declaringType: typeof(VwOpcion), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

        public VwSwitch()
		{
			InitializeComponent();
			/*Detail = " ";
			TapGestureRecognizer tap = new TapGestureRecognizer();
			tap.Tapped += async (s, e) =>
			{
				lo = new ListaOpciones(Lst, Title);
				stack.BackgroundColor = Color.FromHex("#e5e5e5");
				await Task.Delay(100);
				stack.BackgroundColor = Color.Transparent;
				lo.OpcionSeleccionada += (sender, ea) =>
				{
					Detail = ea.sel.opc;
					IdDetail = ea.sel.idopc;
				};
				await Navigation.PushAsync(lo, true);
			};
			stack.GestureRecognizers.Add(tap);
*/
		}
    }
}
