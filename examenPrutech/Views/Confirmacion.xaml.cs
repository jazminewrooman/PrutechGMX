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
            lblpoliza.Text = vm.PolizaGenerada.Referencia;

            if (vm.DatosBank != null && vm.DatosBank.TipoTarj != CreditCardValidator.CardIssuer.Unknown && vm.TransBanco != null)
            {
                if (String.IsNullOrEmpty(vm.TransBanco.Error))
                {
                    lblNombre.Text = vm.DatosBank.Nombre;
                    lblTarjeta.Text = "XXXX-XXXX-XXXX-" + vm.DatosBank.NumTarjeta.Substring(21, 4);
                    lblTipo.Text = "Visa";
                    lblOperacion.Text = vm.TransBanco.Operacion;
                    lblAutorizacion.Text = vm.TransBanco.Autorizacion;
                    lblRefPago.Text = vm.TransBanco.ReferenciaPago;
                }
                slTarjeta.IsVisible = true;
                slEnBanco.IsVisible = false;
            }
            else
            {
                slTarjeta.IsVisible = false;
                slEnBanco.IsVisible = true;
            }
        }
    }
}
