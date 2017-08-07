using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

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

		public bool On
		{
			get { return (bool)GetValue(OnProperty); }
			set { base.SetValue(OnProperty, value); }
		}
        public static readonly BindableProperty OnProperty = BindableProperty.Create(propertyName: "On", returnType: typeof(bool), declaringType: typeof(VwOpcion), defaultValue: false, defaultBindingMode: BindingMode.TwoWay);

        public VwSwitch()
        {
            InitializeComponent();
            swOn.Toggled += (sender, ea) => On = ea.Value;
        }
    }
}
