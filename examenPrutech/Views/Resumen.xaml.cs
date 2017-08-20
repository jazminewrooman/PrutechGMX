using System;
using System.Collections.Generic;
using GMX;
using Xamarin.Forms;
using GMX.Services;
using System.Threading.Tasks;

namespace GMX.Views
{
    public partial class Resumen : ContentPage
    {
        public Resumen()
        {
            InitializeComponent();
            prueba();
        }

        private async Task prueba(){
			//GMX.wsbd.Service ws = new GMX.wsbd.Service(config.Config["APIBD"]);
			//string json = ws.get_catalogos("GetEmisionMedicoByIdAgenteAndEmision", "@UserId=407 , @Emision_Low='2017/06/01' , @Emision_Hgh='2017/06/30'");

			//GMX.wspago.PaymentCenter ws = new GMX.wspago.PaymentCenter();
			//GMX.wspago.KeyValue[] kv = ws.GetMerchantIds();

			bindings b = new bindings();
			b.IniciaWS();
			var cod = new Dictionary<string, string>();

            //cod.Add("profesionId", "8");
            //cod.Add("descProfId", "22");
            //var crypdata = await b.getCatalog("GetEspecialidadesByProfesionAndDescProf", cod);

            cod.Add("agentId", "100");
            var crypdata = await b.getCatalog("GetAgentById", cod);

			var strdata = await b.decrypt(crypdata.Result);
		}
    }
}
