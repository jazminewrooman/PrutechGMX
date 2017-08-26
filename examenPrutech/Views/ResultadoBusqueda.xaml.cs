using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class ResultadoBusqueda : ContentPage
	{
        public ResultadoBusqueda(VMCotizar vmc, string FechaDesde, string FechaHasta)
		{
			InitializeComponent();

            var vm = new VMResultadoBusqueda(UserDialogs.Instance, Navigation, vmc);
            BindingContext = vm;
            Title = "Pólizas Emitidas";
		}
	}
}
