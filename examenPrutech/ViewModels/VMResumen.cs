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
        //VMCotizar vmcotizar;

		public ICommand BuscarCommand { get; private set; }

        public VMResumen(IUserDialogs diag, INavigation n) : base(diag)
        {
            nav = n;
            //vmcotizar = vmc;
            BuscarCommand = new Command(async () =>
            {
                if (!Validar())
                    await diag.AlertAsync("Seleccione las fechas de busqueda", "Error", "Ok");
                else
                {
					await nav.PushAsync(new ResultadoBusqueda(FechaDesde, FechaHasta));
				}
                    
            });
        }

        DateTime fechadesde;
        public DateTime FechaDesde
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

		DateTime fechahasta;
		public DateTime FechaHasta
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
            if (FechaHasta == DateTime.MinValue || FechaDesde == DateTime.MinValue)
				return false;
			else
				return true;
		}
	}
}
