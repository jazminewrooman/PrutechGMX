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

namespace GMX.Services
{
    public class bindings
    {
        private EndpointAddress EndPoint = new EndpointAddress(config.Config["APIIntegracion"]);
        private EmissionServiceClient ws;

		public EmissionServiceClient Service
        {
            get{ return (ws); }
        }

        public bindings(){
            InitializeServiceClient();
        }

        public void IniciaWS()
		{
			if (ws.State == CommunicationState.Closed)
			{
				BasicHttpBinding binding = CreateBasicHttp();
				ws = new EmissionServiceClient(binding, EndPoint);
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
			TimeSpan timeout = new TimeSpan(0, 3, 0);
			binding.SendTimeout = timeout;
			binding.OpenTimeout = timeout;
			binding.ReceiveTimeout = timeout;
			return binding;
		}

        public Task<decryptCompletedEventArgs> decrypt(string request)
		{
			var tcs = CreateSource<decryptCompletedEventArgs>(null);
            ws.decryptCompleted += (sender, e) => TransferCompletion(tcs, e, () => e, null);
            ws.decryptAsync(request, config.Config["llave"]);
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
