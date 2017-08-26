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
    public class VMResultadoBusqueda : VMGmx
    {
		VMCotizar vmcotizar;
		INavigation nav;

        public ICommand Item_Tapped { get; private set; }

        public VMResultadoBusqueda(IUserDialogs diag, INavigation n, VMCotizar vmc) : base(diag)
        {
            nav = n;
            vmcotizar = vmc;



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

        ObservableCollection<resultado> listadatos;
        public ObservableCollection<resultado> ListaDatos
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
        public string nom { get; set; }
        public string tel { get; set; }
        public int id { get; set; }
        public bool sel { set; get; }
    }
}
