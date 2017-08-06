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

            BindingContext = new VMCotizar(UserDialogs.Instance, Navigation);
        }

	}
}
