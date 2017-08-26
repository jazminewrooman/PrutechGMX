using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class Documentos : ContentPage
	{
		public Documentos(VMCotizar vmc)
		{
			InitializeComponent();

            var vm = new VMDocumentos(UserDialogs.Instance, Navigation, vmc);
            BindingContext = vm;
            Title = "Pólizas Emitidas";
		}
	}
}
