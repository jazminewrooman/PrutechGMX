using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class DatosBancarios : ContentPage
	{
        public DatosBancarios(VMCotizar vmc)
		{
			InitializeComponent();

			var vm = new VMDatosBancarios(UserDialogs.Instance, Navigation, vmc);
			BindingContext = vm;
			Title = "Datos bancarios";
		}
	}
}
