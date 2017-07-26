using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GMX.Views
{
[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatosGenerales : ContentPage
	{
		public VMDatosGenerales vm = new VMDatosGenerales(UserDialogs.Instance);
		public DatosGenerales()
		{
			InitializeComponent();

			this.BindingContext = vm;
		}
	}
}
