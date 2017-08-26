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

namespace GMX
{
    public class VMResumen : VMGmx
	{
		INavigation nav;
        VMCotizar vmcotizar;

		public ICommand BuscarCommand { get; private set; }

        public VMResumen(IUserDialogs diag, INavigation n, VMCotizar vmc) : base(diag)
        {
            nav = n;
            vmcotizar = vmc;
            BuscarCommand = new Command(async () =>
            {
                if (!Validar())
                    await diag.AlertAsync(Resources.FaltanOb, "Error", "Ok");
                else
                {
                    int i = 0;
					//await nav.PushAsync(new ResultadoBusqueda(vmc, FechaDesde, FechaHasta));
				}
                    
            });
        }

        string fechadesde;
		public string FechaDesde
        {
            get { return fechadesde; }
            set
            {
                if (fechadesde != value)
                {
                    fechadesde = value;
                    OnPropertyChanged("FechaDesde");
                }
            }
        }

		string fechahasta;
		public string FechaHasta
        {
            get { return fechahasta; }
            set
            {
                if (fechahasta != value)
                {
                    fechahasta = value;
                    OnPropertyChanged("FechaHasta");
                }
            }
        }

		private bool Validar()
		{
            if (String.IsNullOrEmpty(FechaHasta) || String.IsNullOrEmpty(FechaDesde))
				return false;
			else
				return true;
		}
	}
}
