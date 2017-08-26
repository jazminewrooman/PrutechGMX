using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class DetallePoliza : ContentPage
	{
		public DetallePoliza(VMCotizar vmc, VMResultadoBusqueda.resultado, int id)
		{
			InitializeComponent();

            var vm = new VMDetallePoliza(UserDialogs.Instance, Navigation, vmc);
            BindingContext = vm;
            Title = "Pólizas Emitidas"
		}
	}
}
