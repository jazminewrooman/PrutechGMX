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
		private ListaOpciones lo;

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

        public DateTime DateSel
		{
            get { return (DateTime)GetValue(DateSelProperty); }
			set { base.SetValue(DateSelProperty, value); }
		}
        public static readonly BindableProperty DateSelProperty = BindableProperty.Create(propertyName: "DateSel", returnType: typeof(DateTime), declaringType: typeof(VwOpcion), defaultValue: DateTime.MinValue, defaultBindingMode: BindingMode.TwoWay);

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
                var result = await UserDialogs.Instance.DatePromptAsync(new DatePromptConfig
                {
                    IsCancellable = true,
                    MinimumDate = DateTime.Now,
                    MaximumDate = DateTime.Now.AddYears(1)
                });
                Detail = result.SelectedDate.ToString("dd/MM/yyyy");
                DateSel = result.SelectedDate;
            };
            stack.GestureRecognizers.Add(tap);
        }
	}
}
