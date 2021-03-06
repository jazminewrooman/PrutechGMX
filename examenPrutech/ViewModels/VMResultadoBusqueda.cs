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


        public class SelectedOptionEventArgs : EventArgs
        {
            public resum sel { set; get; }
        }

        public event EventHandler<SelectedOptionEventArgs> OpcionSeleccionada;
        protected virtual void OnOpcionSeleccionada(SelectedOptionEventArgs e)
        {
            var handler = OpcionSeleccionada;
            if (handler != null)
                handler(this, e);
        }

        private polizaemitida _ItemSelected;
        public polizaemitida objItemSelected
        {
            get { return _ItemSelected; }
            set
            {
                if (_ItemSelected != value)
                {
                    DetalleBusqueda(value);
                    _ItemSelected = null;
                    OnPropertyChanged("objItemSelected");

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
                string jsonpolizas = ws.get_catalogos("GetEmisionMedicoByIdAgenteAndEmision", $"@UserId={App.usr.UserId},@Emision_Low='{fini}',@Emision_Hgh='{ffin}'");
                datospolizaemitida lst = JsonConvert.DeserializeObject<datospolizaemitida>(jsonpolizas);
                ListaDatos = lst.Table.ToList(); //.Select(x => new resultado { Nombre = x.Nombre_Cliente, Poliza = x.Poliza, Emision = x.Emision, PrimaNeta = x.PrimaNeta, Derechos = x.Derechos, Iva = x.Iva, PrimaTotal = x.PrimaTotal, polizasante = (!String.IsNullOrEmpty(x.PolizasAnt) ? true : false) }).ToList();
                var cc = ListaDatos.Where(x => x.Tarjeta != "");
            }
            catch (Exception ex)
            {
            }
            Ocupado = false;
        }

        List<polizaemitida> listadatos;
        public List<polizaemitida> ListaDatos
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

        public async void DetalleBusqueda(polizaemitida lst)
        {
            await nav.PushAsync(new DetallePolizas(lst));
        }

    }

    public class resultado
    {
        public string Nombre { get; set; }
        public string Poliza { get; set; }
        public DateTime Emision { get; set; }
        public double PrimaNeta { get; set; }
        public double Derechos { get; set; }
		public double Iva { get; set; }
		public double PrimaTotal { get; set; }
        public bool polizasante { get; set; }
        public int id { get; set; }
        public bool sel { set; get; }
    }
}
