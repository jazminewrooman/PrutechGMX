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
using CreditCardValidator;
using Rg.Plugins.Popup.Extensions;

namespace GMX
{
    public class VMDetallePoliza : VMGmx
    {
		INavigation nav;

        public ICommand NextCommand { get; private set; }
        public ICommand CaratulaCommand { get; set; }
        public ICommand GeneralesCommand { get; set; }
        public ICommand ControlCommand { get; set; }
        public ICommand ParticularesCommand { get; set; }
        public ICommand DetalleCommand { get; set; }
        public ICommand ReenviarCommand { get; set; }

        public VMDetallePoliza(resultado res, IUserDialogs diag, INavigation n) : base(diag)
        {
			//public VMDetallePoliza(IUserDialogs diag, INavigation n, resultado res) : base(diag)
			nav = n;
            Title = "Pólizas Emitidas";

            cargaDatos(res);

			NextCommand = new Command(async () =>
			{
                //await nav.PushAsync(new Documentos(vmcotizar));
					
			});

            CaratulaCommand = new Command(async () => 
            {
                
            });
			GeneralesCommand = new Command(async () =>
			{

			});
			ControlCommand = new Command(async () =>
			{

			});
			ParticularesCommand = new Command(async () =>
			{

			});
			DetalleCommand = new Command(async () =>
			{

			});
			ReenviarCommand = new Command(async () =>
			{

			});



        }

        public void cargaDatos(resultado res)
        {
            Emision = res.Emision;
            PrimaNeta = res.PrimaNeta;
            Derechos = res.Derechos;
            IVA = res.Iva;
            PrimaTotal = res.PrimaTotal;

        }

        DateTime emision;
        public DateTime Emision
        {
            get => emision;
            set 
            {
                if (emision != value)
                {
                    emision = value;
                    OnPropertyChanged("Emision");
                }
            }
        }

		double primaneta;
		public double PrimaNeta
		{
			get => primaneta;
			set
			{
				if (primaneta != value)
				{
					primaneta = value;
					OnPropertyChanged("PrimaNeta");
				}
			}
		}

		double derechos;
		public double Derechos
		{
            get => derechos;
			set
			{
				if (derechos != value)
				{
					derechos = value;
					OnPropertyChanged("Derechos");
				}
			}
		}

		double iva;
		public double IVA
		{
			get => iva;
			set
			{
				if (iva != value)
				{
					iva = value;
					OnPropertyChanged("IVA");
				}
			}
		}

		double primatotal;
		public double PrimaTotal
		{
			get => primatotal;
			set
			{
				if (primatotal != value)
				{
					primatotal = value;
					OnPropertyChanged("PrimaTotal");
				}
			}
		}


    }

}
