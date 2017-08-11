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
	public class VMMetodoPago : VMGmx
	{
		INavigation nav;

		public ICommand NextCommand { get; private set; }

		public VMMetodoPago(IUserDialogs diag, INavigation n) : base(diag)
		{
			TxtTerminos = Resources.Terminos;
			TxtManifiesto = Resources.Manifiesto;

			NextCommand = new Command(async () =>
			{
				if (!Validar())
					await Diag.AlertAsync(Resources.FaltanOb, "Error", "Ok");
				else
					await nav.PushAsync(new DatosBancarios());
			});

		}

		private bool Validar()
		{
			if ((!Manifiesto && !Terminos) || (Manifiesto && !Terminos) || (!Manifiesto && Terminos))
				return false;
			else
				return true;
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
