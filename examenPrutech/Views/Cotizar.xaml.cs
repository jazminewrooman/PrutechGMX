using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace GMX.Views
{
    public partial class Cotizar : ContentPage
    {
        public Cotizar()
        {
            InitializeComponent();

            var vm = new VMCotizar(UserDialogs.Instance, Navigation);
            BindingContext = vm;
			Title = "Cotizar";

		}

	}
}
