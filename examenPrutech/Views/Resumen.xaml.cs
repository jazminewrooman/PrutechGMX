using System;
using System.Collections.Generic;
using GMX;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace GMX.Views
{
    public partial class Resumen : ContentPage
    {
        public Resumen(VMCotizar vmc)
        {
            InitializeComponent();

            var vm = new VMResumen(UserDialogs.Instance, Navigation, vmc);
            BindingContext = vm;
            Title = "Pólizas emitidas";
            prueba();
        }

        private async Task prueba(){
			//GMX.wsbd.Service ws = new GMX.wsbd.Service(config.Config["APIBD"]);
			//string json = ws.get_catalogos("GetEmisionMedicoByIdAgenteAndEmision", "@UserId=407 , @Emision_Low='2017/06/01' , @Emision_Hgh='2017/06/30'");
		}
    }
}
