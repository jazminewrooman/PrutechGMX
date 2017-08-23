using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;

namespace GMX.Views
{
    public partial class VerResumen : PopupPage
    {
        public VerResumen(VMCotizar vmc, FormattedString resumen)
        {
            InitializeComponent();

			BindingContext = vmc;

            vmc.Resumen1 = resumen.ToString();

			btnCerrar.Clicked += (s, e) =>
			{
				Navigation.PopPopupAsync(true);
			};
        }
    }
}
