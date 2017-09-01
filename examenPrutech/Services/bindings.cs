using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;
using System.ServiceModel;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Net;
using GMXHelper;
using GMX.Views;

namespace GMX.Services
{
    public class bindings
    {
        private EndpointAddress EndPoint = new EndpointAddress(config.Config["APIIntegracion"]);
        private EmissionServiceClient ws;
        private GMXITServiceClient wsit;
        private GMXDocumentRepositoryClient wsdoc;

		/*public EmissionServiceClient Service
        {
            get { return (ws); }
        }*/

        public bindings(){
            //InitializeServiceClient();
        }

        public void IniciaWS(string api = "", string apidoc = "")
		{
			if (apidoc != "")
			{
				if (wsdoc == null || wsdoc.State == CommunicationState.Closed)
				{
					BasicHttpBinding binding = CreateBasicHttp();
					wsdoc = new GMXDocumentRepositoryClient(binding, new EndpointAddress(apidoc));
				}
			}
			if (api != "")
            {
                if (wsit == null || wsit.State == CommunicationState.Closed)
				{
					BasicHttpBinding binding = CreateBasicHttp();
                    wsit = new GMXITServiceClient(binding, new EndpointAddress(api));
				}
			}
            else
            {
                if (ws == null || ws.State == CommunicationState.Closed)
                {
                    BasicHttpBinding binding = CreateBasicHttp();
                    ws = new EmissionServiceClient(binding, EndPoint);
                }
            }
		}

		private void InitializeServiceClient()
		{
			BasicHttpBinding binding = CreateBasicHttp();

			ws = new EmissionServiceClient(binding, EndPoint);
		}

		private static BasicHttpBinding CreateBasicHttp()
		{
			BasicHttpBinding binding = new BasicHttpBinding()
			{
				Name = "basicHttpBinding",
				MaxBufferSize = 2147483647,
				MaxReceivedMessageSize = 2147483647,
			};
			TimeSpan timeout = new TimeSpan(0, 0, 60);
			binding.SendTimeout = timeout;
			binding.OpenTimeout = timeout;
			binding.ReceiveTimeout = timeout;
			return binding;
		}

		#region Docs
		public Task<ReturnDocumentCompletedEventArgs> ReturnDocument(string name, bool grales)
		{
            var tcs = CreateSource<ReturnDocumentCompletedEventArgs>(null);
            wsdoc.ReturnDocumentCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
            wsdoc.ReturnDocumentAsync(config.Config["producto"], config.Config["clave"], config.Config["llave"], name, grales);
			return tcs.Task;
		}

		#endregion

		#region Integracion
		public Task<createPolicyCompletedEventArgs> createPolicy(IntegrationServiceEntity.Emission emision)
		{
            var tcs = CreateSource<createPolicyCompletedEventArgs>(null);
            ws.createPolicyCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
            ws.createPolicyAsync(emision);
			return tcs.Task;
		}

        public Task<decryptCompletedEventArgs> decrypt(string request, string llave = "")
		{
			var tcs = CreateSource<decryptCompletedEventArgs>(null);
            ws.decryptCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
            if (String.IsNullOrEmpty(llave.Trim()))
                ws.decryptAsync(request, config.Config["llave"]);
            else
                ws.decryptAsync(request, llave);
			return tcs.Task;
		}

        public Task<getCatalogCompletedEventArgs> getCatalog(string cmd, Dictionary<string, string> param)
        {
            var values = new Dictionary<string, object>
               {
                { "producto",  config.Config["producto"] },
                { "clave",  config.Config["clave"] },
                { "llave", config.Config["llave"] },
               };
            values.Add("comando", cmd);
            values.Add("parametros", param);
            string json = JsonConvert.SerializeObject(values);
            var tcs = CreateSource<getCatalogCompletedEventArgs>(null);
            ws.getCatalogCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
            ws.getCatalogAsync(json);
            return tcs.Task;
        }
		#endregion

		#region GMX IT
        public Task<GenerateDocumentCompletedEventArgs> GenerateDocument(Section[] sections, DocumentPDF pdf)
		{
            var tcs = CreateSource<GenerateDocumentCompletedEventArgs>(null);
			wsit.GenerateDocumentCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
            wsit.GenerateDocumentAsync(sections, pdf);
			return tcs.Task;
		}

        public Task<DistribuirDocumentacionPolizaCompletedEventArgs> DistribuirDocumentacionPoliza(string mailaseg, FilePropertiesManager slip)
		{
            Destinatario[] asegurado = new Destinatario[] { new Destinatario() { Nombre = "Asegurado", Mail = mailaseg } };
            Destinatario[] agente = new Destinatario[] { new Destinatario() { Nombre = "Agente", Mail = App.usr.Email } };
            Destinatario[] suscrip = new Destinatario[] { new Destinatario() { Nombre = "Suscriptor", Mail = App.suscriptor.email } };
            Destinatario[] nicho = new Destinatario[] { new Destinatario() { Nombre = "Dynamic_CCO", Mail = config.Config["EmailNicho"] } };

			var tcs = CreateSource<DistribuirDocumentacionPolizaCompletedEventArgs>(null);
            wsit.DistribuirDocumentacionPolizaCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
            wsit.DistribuirDocumentacionPolizaAsync("cotizacion", false, "PVLMED", "", asegurado, agente, suscrip, nicho, null, slip, null, null, null, null, null, null);
			return tcs.Task;
		}
		#endregion

		private static TaskCompletionSource<T> CreateSource<T>(object state)
		{
			return new TaskCompletionSource<T>(state, TaskCreationOptions.None);
		}

		private static void TransferCompletion<T>(TaskCompletionSource<T> tcs, AsyncCompletedEventArgs e, Func<T> getResult, Action unregisterHandler)
		{
			//if (e.UserState == tcs)
			//{
				if (e.Cancelled) 
                    tcs.TrySetCanceled();
				else if (e.Error != null) 
                    tcs.TrySetException(e.Error);
				else 
                    tcs.TrySetResult(getResult());
				if (unregisterHandler != null) 
                    unregisterHandler();
			//}
		}

    }
}
