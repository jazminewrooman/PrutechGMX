using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class MetodoPago : ContentPage
	{
        public MetodoPago(){
            InitializeComponent();
        }

		public MetodoPago(VMCotizar vmcot)
		{
			InitializeComponent();

            var vm = new VMMetodoPago(UserDialogs.Instance, Navigation, vmcot);
			BindingContext = vm;
			Title = "Método de Pago";

            vm.tipopago = (TipoPago)(seg.SelectedSegment + 1);
            seg.ValueChanged += (sender, e) => {
                vm.tipopago = (TipoPago)(seg.SelectedSegment + 1);
            };
		}
	}
}
