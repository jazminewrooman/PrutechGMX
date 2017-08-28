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
    public class VMResumenDatos : VMGmx
    {
        VMCotizar vmcotizar;
        INavigation nav;
        public ICommand EmisionCommand { get; private set; }
        public ICommand SelectList { get; private set; }

        public VMResumenDatos(IUserDialogs diag, INavigation n, VMCotizar vmc) : base(diag)
        {
            nav = n;
            vmcotizar = vmc;
            ObservableCollection<resum> lst = new ObservableCollection<resum>();
            lst.Add(new resum { id=1, opc = "Datos Generales" });
            lst.Add(new resum { id=2, opc = "Datos Fiscales" });
            lst.Add(new resum { id=3, opc ="Datos Profesionales"});
            lst.Add(new resum { id=4, opc = "Datos Bancarios"});

			ListaDatos = lst;

			EmisionCommand = new Command(async () =>
			{
                var result = await diag.PromptAsync("Se mandara la emision de la poliza con prima de " + vmcotizar.PrimaNeta.ToString("c") + ". ¿Desea continuar?", "Aviso", "Ok", "Cancelar");
                if (result.Ok)
                    await vmcotizar.MandarEmision();
			});

            SelectList = new Command( (e) =>
            {
                /*if (e.SelectedItem == null)
                    return;
                OnOpcionSeleccionada(new SelectedOptionEventArgs() {sel = (e.SelectedItem as resum)});
                SeleccionaLista(e.SelectedItem);*/
            });
		}

        public class SelectedOptionEventArgs : EventArgs
        {
            public resum sel { set; get; }
        }

		#region Declaraciones

		ObservableCollection<resum> listadatos;
		public ObservableCollection<resum> ListaDatos
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

		public event EventHandler<SelectedOptionEventArgs> OpcionSeleccionada;
		protected virtual void OnOpcionSeleccionada(SelectedOptionEventArgs e)
		{
			var handler = OpcionSeleccionada;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		private resum _ItemSelected;
		public resum objItemSelected
		{
			get
			{
				return _ItemSelected;
			}
			set
			{
				if (_ItemSelected != value)
				{
					//_ItemSelected = value;
                    SeleccionaLista(value.id); 
                    _ItemSelected = null;
					OnPropertyChanged("objItemSelected");
				}
			}
		}

		#endregion

		#region Eventos

		public async void SeleccionaLista(int id)
		{
			switch (id)
			{
				case 1:
					//Para Datos Generales
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosGenerales, TipoResumen.Generales, nav), true);
					break;
				case 2:
					//Para Datos Fiscales
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosFiskles, TipoResumen.Fiscales, nav), true);
					break;
				case 3:
					//Para Datos Profesionales
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosProfesionales, TipoResumen.Profesionales, nav), true);
					break;
				case 4:
					//Para Datos Bancarios
                    await nav.PushPopupAsync(new VerResumen(vmcotizar, vmcotizar.DatosBancarios, TipoResumen.Bancarios, nav), true);
					break;
			}
		}

  #endregion

	}

    public class resum 
    {
        public string opc { get; set; }
        public int id { get; set; }
        public bool sel { get; set; }
    }
}
