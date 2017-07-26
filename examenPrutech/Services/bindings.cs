using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Plugin.Connectivity;
using System.ServiceModel;
using Acr.UserDialogs;
using Newtonsoft.Json;
using System.Net;

namespace GMX.Services
{
    public class bindings
    {
		public EndpointAddress EndPoint = new EndpointAddress("http://desa.gmx.com.mx/IntegrationService/EmissionService.svc");
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
			//------    Credenciales y SSL  --------------------------
			//BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport)
			//------    Credenciales y SSL  --------------------------
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
    }
}
