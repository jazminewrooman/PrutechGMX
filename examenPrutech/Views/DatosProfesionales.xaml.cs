using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class DatosProfesionales : ContentPage
	{
        public DatosProfesionales(VMCotizar vmcotiz)
		{
			InitializeComponent();

			var vm = new VMDatosProfesionales(UserDialogs.Instance, Navigation, vmcotiz);
			BindingContext = vm;
			Title = "Datos profesionales";
		}
	}
}
