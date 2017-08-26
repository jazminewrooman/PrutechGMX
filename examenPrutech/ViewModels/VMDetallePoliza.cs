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
		VMCotizar vmcotizar;
		INavigation nav;

        public ICommand NextCommand { get; private set; }

        public VMDetallePoliza(IUserDialogs diag, INavigation n, VMCotizar vmc, int id) : base(diag)
        {
            nav = n;
            vmcotizar = vmc;
            Title = "Pólizas Emitidas";

			NextCommand = new Command(async () =>
			{
                //await nav.PushAsync(new Documentos(vmcotizar));
					
			});


        }

        string emision;
        public string Emision
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

		string primaneta;
		public string PrimaNeta
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

		string derechos;
		public string Derechos
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

		string iva;
		public string IVA
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

		string primatotal;
		public string PrimaTotal
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
