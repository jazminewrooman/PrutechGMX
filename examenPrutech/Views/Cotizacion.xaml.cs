using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GMX.Views
{
    public partial class Cotizacion : ContentPage
    {
        public Cotizacion(VMCotizar vm)
        {
            InitializeComponent();

            BindingContext = vm;
            Title = "Cotización";
            lblLeyenda.Text = GMX.Resources.Montosexpresados;
        }
    }
}
