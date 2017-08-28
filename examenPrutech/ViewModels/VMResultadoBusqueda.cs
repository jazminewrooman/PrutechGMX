﻿using System;
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
using CreditCardValidator;
using Rg.Plugins.Popup.Extensions;

namespace GMX
{
    public class VMResultadoBusqueda : VMGmx
    {
		INavigation nav;

        public ICommand Item_Tapped { get; private set; }

        public VMResultadoBusqueda(DateTime FechaDesde, DateTime FechaHasta, IUserDialogs diag, INavigation n) : base(diag)
        {
            nav = n;
            Cargar(FechaDesde.ToString("yyyy/MM/dd"), FechaHasta.ToString("yyyy/MM/dd"));
        }

		public class ItemTappedEventArgs : EventArgs
		{
			public resum sel { set; get; }
		}

        public event EventHandler<ItemTappedEventArgs> OpcionSeleccionada;
        protected virtual void OnOpcionSeleccionada(ItemTappedEventArgs e)
        {
            var handler = OpcionSeleccionada;
            if (handler != null)
                handler(this, e);
        }

        private resultado tappeditem;
        public resultado TappedItem
        {
            get { return tappeditem; }
            set 
            {
                if (tappeditem != value)
                {
                    tappeditem = value;
                    OnPropertyChanged("TappedItem");

                    //Evento para llamar el detalle de la búsqueda
                }
            }
        }

        private async Task Cargar(string fini, string ffin)
        {
            Ocupado = true;
            try
            {
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100));
                wsbd.Service ws = new wsbd.Service(config.Config["APIBD"]);
                ws.Timeout = 2000;
                string jsonpolizas = ws.get_catalogos("GetEmisionMedicoByIdAgenteAndEmisionALL", $"@Emision_Low='{fini}',@Emision_Hgh='{ffin}'");
                datospolizaemitida lst = JsonConvert.DeserializeObject<datospolizaemitida>(jsonpolizas);
                ListaDatos = lst.Table.Select(x => new resultado { Nombre = x.Nombre_Cliente, Poliza = x.Poliza }).ToList();
            }
            catch { }
            Ocupado = false;
        }

        List<resultado> listadatos;
        public List<resultado> ListaDatos
		{
			get { return listadatos; }
			set
			{
				if (listadatos != value)
				{
					listadatos = value;
					OnPropertyChanged("ListaDatos");
				}

			}
		}

        public async void DetalleBusqueda(resultado lst, int id)
        {
            //await nav.PushAsync(new DetallePoliza(vmcotizar, lst, id));
        }

    }

    public class resultado
    {
        public string Nombre { get; set; }
        public string Poliza { get; set; }
        public int id { get; set; }
        public bool sel { set; get; }
    }
}
