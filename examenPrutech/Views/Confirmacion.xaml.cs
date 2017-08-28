using System;
using System.Collections.Generic;
using GMX;
using Xamarin.Forms;

namespace GMX.Views
{
    public partial class Confirmacion : ContentPage
    {
        public Confirmacion(VMCotizar vm)
        {
            InitializeComponent();

            lblSehaenviado.Text = $"{GMX.Resources.SeEnvio} {vm.DatosGrales.Correo}, {GMX.Resources.NoLocaliza}";
            lblprima.Text = vm.PrimaTotal.ToString("c");

        }
    }
}
