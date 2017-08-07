using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GMX.Views
{
	public partial class VwText : ContentView
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

		public VwText()
		{
			InitializeComponent();
		}
	}
}
