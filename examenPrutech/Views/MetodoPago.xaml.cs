using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class MetodoPago : ContentPage
	{
		public MetodoPago()
		{
			InitializeComponent();

            var vm = new VMMetodoPago(UserDialogs.Instance, Navigation);
			BindingContext = vm;
			Title = "Método de Pago";

		}
	}
}
