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
	public class VMResumenDatos : VMGmx
	{
		INavigation nav;
		public ICommand ShopCommand { get; private set; }

		public VMResumenDatos(IUserDialogs diag, INavigation n) : base(diag)
		{
			ObservableCollection<opciones> lst = new ObservableCollection<opciones>();
			lst.Add(new opciones() { idopc = "1", opc = "Datos Generales" });
			lst.Add(new opciones() { idopc = "2", opc = "Datos Fiscales" });
			lst.Add(new opciones() { idopc = "3", opc = "Datos Profesionales" });
			lst.Add(new opciones() { idopc = "4", opc = "Datos Bancarios" });
			LstResumenDatos = lst;

			ShopCommand = new Command(async () =>
			{
				DatosGralesModel dgmodel = null;
				//var Welcome = new DatosGenerales(dgmodel, TipoDatos.Generales);
				//await nav.PushAsync(new DatosGenerales(dgmodel, TipoDatos.Generales));
			});
		}

		#region Declaraciones

		ObservableCollection<opciones> lstresumendatos;
		public ObservableCollection<opciones> LstResumenDatos
		{
			get { return lstresumendatos; }
			set
			{
				if (lstresumendatos != value)
				{
					lstresumendatos = value;
					OnPropertyChanged("LstResumenDatos");
				}

			}
		}

		#endregion

		#region Eventos

		public async void SeleccionaLista(int id)
		{
			DatosGralesModel dgmodel;
			switch (id)
			{
				case 1:
					//Para Datos Generales
					dgmodel = null;
					//var Welcome1 = new DatosGenerales(dgmodel, TipoDatos.Generales);
					//App.navigation.InsertPageBefore(Welcome1, App.navigation.NavigationStack.FirstOrDefault());
					//await App.navigation.PopToRootAsync();
					//var MainP1 = new NavigationPage(Welcome1)
					//{
					//	BarTextColor = Color.White,
					//	BarBackgroundColor = Color.FromHex("#04b5b5"),
					//};
					//var md1 = new MasterDetailPage();
					//md1.Detail = MainP1;
					//App.Current.MainPage = md1;
					break;
				case 2:
					//Para Datos Fiscales
					dgmodel = null;
					//var Welcome2 = new DatosGenerales(dgmodel, TipoDatos.Fiscales);
					//App.navigation.InsertPageBefore(Welcome2, App.navigation.NavigationStack.FirstOrDefault());
					//await App.navigation.PopToRootAsync();
					//var MainP2 = new NavigationPage(Welcome2)
					//{
					//	BarTextColor = Color.White,
					//	BarBackgroundColor = Color.FromHex("#04b5b5"),
					//};
					//var md2 = new MasterDetailPage();
					////md.Master = new menu(user);
					//md2.Detail = MainP2;
					//App.Current.MainPage = md2;
					break;
				case 3:
					//Para Datos Profesionales
					var Welcome3 = new DatosBancarios();
					App.navigation.InsertPageBefore(Welcome3, App.navigation.NavigationStack.FirstOrDefault());
					await App.navigation.PopToRootAsync();
					var MainP3 = new NavigationPage(Welcome3)
					{
						BarTextColor = Color.White,
						BarBackgroundColor = Color.FromHex("#04b5b5"),
					};
					var md3 = new MasterDetailPage();
					//md.Master = new menu(user);
					md3.Detail = MainP3;
					App.Current.MainPage = md3;
					break;
				case 4:
					//Para Datos Bancarios
					var Welcome4 = new DatosBancarios();
					App.navigation.InsertPageBefore(Welcome4, App.navigation.NavigationStack.FirstOrDefault());
					await App.navigation.PopToRootAsync();
					var MainP4 = new NavigationPage(Welcome4)
					{
						BarTextColor = Color.White,
						BarBackgroundColor = Color.FromHex("#04b5b5"),
					};
					var md4 = new MasterDetailPage();
					//md.Master = new menu(user);
					md4.Detail = MainP4;
					App.Current.MainPage = md4;
					break;
			}
		}

  #endregion

	}
}
