using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Acr.UserDialogs;

namespace GMX.Views
{
	public partial class VwCal : ContentView
	{
		public Color TitleColor
		{
			get { return (Color)GetValue(TitleColorProperty); }
			set { base.SetValue(TitleColorProperty, value); }
		}
        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create(propertyName: "TitleColor", returnType: typeof(Color), declaringType: typeof(VwCal), defaultValue: Color.Black, defaultBindingMode: BindingMode.TwoWay);

		public string Title
		{
			get { return GetValue(TitleProperty).ToString(); }
			set { base.SetValue(TitleProperty, value); }
		}
		public static readonly BindableProperty TitleProperty = BindableProperty.Create(propertyName: "Title", returnType: typeof(string), declaringType: typeof(VwCal), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

		public string Detail
		{
			get { return GetValue(DetailProperty).ToString(); }
			set { base.SetValue(DetailProperty, value); }
		}
		public static readonly BindableProperty DetailProperty = BindableProperty.Create(propertyName: "Detail", returnType: typeof(string), declaringType: typeof(VwCal), defaultValue: "", defaultBindingMode: BindingMode.TwoWay);

        public DateTime DateSel
		{
            get { return (DateTime)GetValue(DateSelProperty); }
			set { base.SetValue(DateSelProperty, value); }
		}
        public static readonly BindableProperty DateSelProperty = BindableProperty.Create(propertyName: "DateSel", returnType: typeof(DateTime), declaringType: typeof(VwCal), defaultValue: DateTime.MinValue, defaultBindingMode: BindingMode.TwoWay, propertyChanged:Seleccion);

		public DateTime Fini
		{
			get { return (DateTime)GetValue(FiniProperty); }
			set { base.SetValue(FiniProperty, value); }
		}
		public static readonly BindableProperty FiniProperty = BindableProperty.Create(propertyName: "Fini", returnType: typeof(DateTime), declaringType: typeof(VwCal), defaultValue: DateTime.MinValue, defaultBindingMode: BindingMode.TwoWay);

        public DateTime Ffin
		{
			get { return (DateTime)GetValue(FfinProperty); }
			set { base.SetValue(FfinProperty, value); }
		}
		public static readonly BindableProperty FfinProperty = BindableProperty.Create(propertyName: "Ffin", returnType: typeof(DateTime), declaringType: typeof(VwCal), defaultValue: DateTime.MinValue, defaultBindingMode: BindingMode.TwoWay);
		
        private static void Seleccion(BindableObject bindable, object oldValue, object newValue)
		{
            var obj = bindable as VwCal;
			try
			{
                if (obj.DateSel != DateTime.MinValue)
                {
                    obj.Detail = obj.DateSel.ToString("dd/MM/yyyy");
                    obj.TitleColor = Color.Black;
                }
			}
			catch
			{

			}
		}

		public VwCal()
        {
            InitializeComponent();
            Detail = " ";
            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += async (s, e) =>
            {
                stack.BackgroundColor = Color.FromHex("#e5e5e5");
                await Task.Delay(100);
                stack.BackgroundColor = Color.Transparent;
                var cfg = new DatePromptConfig { IsCancellable = true, };
                if (Fini != DateTime.MinValue)
                    cfg.MinimumDate = Fini.Date;
                if (Ffin != DateTime.MinValue)
                    cfg.MaximumDate = Ffin.Date; //.AddDays(1);
                var result = await UserDialogs.Instance.DatePromptAsync(cfg);
                Detail = result.SelectedDate.ToString("dd/MM/yyyy");
                DateSel = result.SelectedDate;
                TitleColor = Color.Black;
            };
            stack.GestureRecognizers.Add(tap);
			if (String.IsNullOrEmpty(Detail.Trim()))
			{
				TitleColor = Color.Red;
				Title = "*" + Title;
			}
			else
				TitleColor = Color.Black;
		}
	}
}
