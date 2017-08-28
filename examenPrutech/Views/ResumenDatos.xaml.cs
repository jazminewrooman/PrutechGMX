using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
    public partial class ResumenDatos : ContentPage
    {
        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {

            throw new NotImplementedException();
        }

        public ResumenDatos(VMCotizar vmc)
        {
            InitializeComponent();

            var vm = new VMResumenDatos(UserDialogs.Instance, Navigation, vmc);
            BindingContext = vm;

            Title = "Resumen";

            LstResumenDatos.ItemSelected += (sender, e) =>
			{
			    if (e.SelectedItem == null)
			        return;
			    //((ListView)sender).SelectedItem = null;
			};

        }
    }
}
