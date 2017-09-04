using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using GMX.Services.DTOs;
using Xamarin.Forms;

namespace GMX.Views
{
    public partial class DetallePolizas : ContentPage
    {
        public DetallePolizas(polizaemitida res) 
        {
            InitializeComponent();

            var vm = new VMDetallePoliza(res, UserDialogs.Instance, Navigation);
            BindingContext = vm;
            Title = "PolizasEmitidas";
        }
    }
}
