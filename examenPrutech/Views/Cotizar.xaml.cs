using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace GMX.Views
{
    public partial class Cotizar : ContentPage
    {
        VMCotizar vm;
        static bool primeravez = true;

        public Cotizar()
        {
            InitializeComponent();

            vm = new VMCotizar(UserDialogs.Instance, Navigation);
            BindingContext = vm;
			Title = "Cotizar";
            vm.IniciaCarga();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (primeravez)
            {
                primeravez = false;
                vm.ClickAuto = true;
            }
		}

	}
}
