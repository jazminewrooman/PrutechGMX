using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;

namespace GMX.Views
{
    public partial class VerResumen : PopupPage
    {
        public VerResumen(VMCotizar vmc, FormattedString resumen, TipoResumen tr)
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
                var fs = new FormattedString();
                fs.Spans.Add(new Span{Text="Datos " + tr.ToString() + " \n ", ForegroundColor = Color.Red, FontSize = 18 });
                fs.Spans.Add(new Span { Text = "No hay información para mostrar", ForegroundColor = Color.Black });
                vmc.Resumen1 = fs;
                OnPropertyChanged("Resumen1");
            }

			btnCerrar.Clicked += (s, e) =>
			{
				Navigation.PopPopupAsync(true);
			};
        }
    }
}
