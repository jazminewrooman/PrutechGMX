using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Collections.Generic;
using GMX.Views;
using System.Collections.ObjectModel;
using GMX.Services.DTOs;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

namespace GMX
{
	public class VMMetodoPago : VMGmx
	{
		INavigation nav;

		public ICommand NextCommand { get; private set; }
        public ICommand VerCotizaCommand { get; private set; }
        VMCotizar vmcotizar;
        public TipoPago tipopago { get; set; }

		public VMMetodoPago(IUserDialogs diag, INavigation n, VMCotizar vmc) : base(diag)
		{
            nav = n;
            vmcotizar = vmc;
			TxtTerminos = Resources.Terminos;
			TxtManifiesto = Resources.Manifiesto;
            VerCotizaCommand = new Command(async () =>
            {
                await nav.PushPopupAsync(new VerCotiza(vmcotizar), true);
            });
			NextCommand = new Command(async () =>
			{
                if (!Validar())
                    await Diag.AlertAsync(Resources.FaltanOb, "Error", "Ok");
                else
                {
                    if (tipopago == TipoPago.tarjeta)
                        await nav.PushAsync(new DatosBancarios(vmcotizar, Modo.Captura));
                    if (tipopago == TipoPago.banco)
                    {
                        //await nav.PushAsync(new ResumenDatos(vmcotizar));
                        var cotizar = nav.NavigationStack.OfType<Cotizar>().FirstOrDefault();
                        if (cotizar != null)
                        {
                            //nav.InsertPageBefore(new ResumenDatos(vmcotizar), cotizar);
                            //await nav.PopToRootAsync(true);

                            await nav.PopToRootAsync(true);
                            var resumen = new ResumenDatos(vmcotizar);
							var MainP = new NavigationPage(resumen)
							{
								BarTextColor = Color.FromHex("#04b5b5"),
								BarBackgroundColor = Color.White,
							};
							var md = new MasterDetailPage();
							md.Master = new menu();
							md.Detail = MainP;
							App.Current.MainPage = md;
                        }
                    }
                }
			});

		}

		private bool Validar()
		{
            if ((Manifiesto && Terminos))
                return true;
            else
                return false;
		}


		string txtterminos;
		public string TxtTerminos
		{
			get { return txtterminos; }
			set
			{
				if (txtterminos != value)
				{
					txtterminos = value;
					OnPropertyChanged("TxtTerminos");
				}

			}
		}
		string txtmanifiesto;
		public string TxtManifiesto
		{
			get { return txtmanifiesto; }
			set
			{
				if (txtmanifiesto != value)
				{
					txtmanifiesto = value;
					OnPropertyChanged("TxtManifiesto");
				}
			}
		}


		bool terminos;
		public bool Terminos
		{
			get { return terminos; }
			set
			{
				if (terminos != value)
				{
					terminos = value;
					OnPropertyChanged("Terminos");
				}
			}
		}
		bool manifiesto;
		public bool Manifiesto
		{
			get { return manifiesto; }
			set
			{
				if (manifiesto != value)
				{
					manifiesto = value;
					OnPropertyChanged("Manifiesto");
				}
			}
		}
	}
}
