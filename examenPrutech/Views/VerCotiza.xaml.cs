using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;

namespace GMX.Views
{
    public partial class VerCotiza : PopupPage
    {
        public VerCotiza(VMCotizar vm)
        {
            InitializeComponent();

            BindingContext = vm;

            btnCerrar.Clicked += (s, e) =>
            {
                Navigation.PopPopupAsync(true);
            };
        }
    }
}
