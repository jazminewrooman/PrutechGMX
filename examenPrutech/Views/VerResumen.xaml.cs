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

            var cadena = resumen;

            if (cadena != null)
            {
                vmc.Resumen1 = cadena;
                OnPropertyChanged("Resumen1");
            }
            else
            {
                vmc.Resumen1 = "No hay información para mostrar";
                OnPropertyChanged("Resumen1");
            }

			btnCerrar.Clicked += (s, e) =>
			{
				Navigation.PopPopupAsync(true);
			};
        }
    }
}
