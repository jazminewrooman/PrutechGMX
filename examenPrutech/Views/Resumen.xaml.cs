using System;
using System.Collections.Generic;
using GMX;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace GMX.Views
{
    public partial class Resumen : ContentPage
    {
        public Resumen()
        {
            InitializeComponent();

            var vm = new VMResumen(UserDialogs.Instance, Navigation);
            BindingContext = vm;
            Title = "Pólizas emitidas";
        }

    }
}
