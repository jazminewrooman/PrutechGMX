using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class ResumenDatos : ContentPage
	{
		public ResumenDatos()
		{
			InitializeComponent();

			var vm = new VMResumenDatos(UserDialogs.Instance, Navigation);
			BindingContext = vm;
			Title = "Resumen";

			lstResumen.ItemSelected += (s, e) =>
			 {
				var id = (int)e.SelectedItem;

			 };
		}
	}
}
