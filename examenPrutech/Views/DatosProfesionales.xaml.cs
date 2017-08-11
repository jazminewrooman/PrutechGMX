using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class DatosProfesionales : ContentPage
	{
		public DatosProfesionales()
		{
			InitializeComponent();

			var vm = new VMDatosProfesionales(UserDialogs.Instance, Navigation);
			BindingContext = vm;
			Title = "Datos profesionales";
		}
	}
}
