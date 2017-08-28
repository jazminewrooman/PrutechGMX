using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class DatosProfesionales : ContentPage
	{
        public DatosProfesionales(VMCotizar vmcotiz, Modo modo)
		{
			InitializeComponent();

			var vm = new VMDatosProfesionales(UserDialogs.Instance, Navigation, vmcotiz, modo);
			BindingContext = vm;
		}
	}
}
