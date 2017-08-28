using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

using Xamarin.Forms;

namespace GMX.Views
{
    public partial class VerResumen : PopupPage
    {
        INavigation nav;

        public VerResumen()
        {

        }

        public VerResumen(VMCotizar vmc, FormattedString resumen, TipoResumen tr, INavigation n)
        {
            nav = n;
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

			btnCerrar.Clicked += async (s, e) =>
			{
				await Navigation.PopPopupAsync(true);
			};
            btnEditar.Clicked += async (s, e) =>
            {
                await Navigation.PopPopupAsync(true);
                if (tr == TipoResumen.Generales)
                    await nav.PushAsync(new DatosGenerales(vmc.DatosGrales, TipoDatos.Generales, vmc, Modo.Edicion));
                if (tr == TipoResumen.Fiscales)
                    await nav.PushAsync(new DatosGenerales(vmc.DatosFiscales, TipoDatos.Fiscales, vmc, Modo.Edicion));
                if (tr == TipoResumen.Profesionales)
                    await nav.PushAsync(new DatosProfesionales(vmc, Modo.Edicion));
                if (tr == TipoResumen.Bancarios)
                    await nav.PushAsync(new DatosBancarios(vmc, Modo.Edicion));
			};
        }
    }
}
