using System;
using System.Collections.Generic;
using Acr.UserDialogs;

using Xamarin.Forms;

namespace GMX.Views
{
    public partial class DetallePolizas : ContentPage
    {
        public DetallePolizas(resultado res, int id) 
        {
            InitializeComponent();

            var vm = new VMDetallePoliza(res, UserDialogs.Instance, Navigation);
            BindingContext = vm;
            Title = "PolizasEmitidas";
        }
    }
}
