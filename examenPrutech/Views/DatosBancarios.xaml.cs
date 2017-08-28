using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class DatosBancarios : ContentPage
	{
        public DatosBancarios(VMCotizar vmc, Modo modo)
		{
			InitializeComponent();

			var vm = new VMDatosBancarios(UserDialogs.Instance, Navigation, vmc, modo);
			BindingContext = vm;
			Title = "Datos bancarios";
		}
	}
}
