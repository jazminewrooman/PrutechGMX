using System;
using System.Collections.Generic;
using GMX;
using Xamarin.Forms;

namespace GMX.Views
{
    public partial class Confirmacion : ContentPage
    {
        public Confirmacion()
        {
            InitializeComponent();
        }

        public Confirmacion(VMCotizar vm)
        {
            InitializeComponent();

            lblSehaenviado.Text = $"{GMX.Resources.SeEnvio} {vm.DatosGrales.Correo}, {GMX.Resources.NoLocaliza}";
            lblprima.Text = vm.PrimaTotal.ToString("c");
            lblpoliza.Text = vm.PolizaGenerada.NumPoliza;

            if (vm.DatosBank != null && vm.DatosBank.TipoTarj != CreditCardValidator.CardIssuer.Unknown && vm.TransBanco != null)
            {
                if (vm.TransBanco.response == "approved")
                {
                    lblNombre.Text = vm.TransBanco.cc_name;
                    lblTarjeta.Text = "XXXX-XXXX-XXXX-" + vm.TransBanco.cc_number;
                    lblTipo.Text = vm.TransBanco.cc_type;
                    lblOperacion.Text = vm.TransBanco.tp_operation;
                    lblAutorizacion.Text = vm.TransBanco.auth;
                    lblRefPago.Text = vm.TransBanco.foliocpagos;
                    slVolver.IsVisible = false;
                }
                if (vm.TransBanco.response == "denied")
                {
                    DisplayAlert("Error", vm.TransBanco.friendly_response, "Ok");
                    slVolver.IsVisible = true;
                }
                if (vm.TransBanco.response == "error")
                {
                    DisplayAlert("Error", vm.TransBanco.nb_error, "Ok");
                    slVolver.IsVisible = true;
                }
                slTarjeta.IsVisible = true;
                slEnBanco.IsVisible = false;
            }
            else
            {
                slVolver.IsVisible = false;
                slTarjeta.IsVisible = false;
                slEnBanco.IsVisible = true;
            }

            btnVolver.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new DatosBancarios(vm, Modo.Compra));
            };
        }
    }
}
