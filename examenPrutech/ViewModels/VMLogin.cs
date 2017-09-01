﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Web.Services.Protocols;
using System.Web.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using System.Linq;
using GMX.Views;
using GMX.Services;
using GMX.Services.DTOs;

namespace GMX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class VMLogin : VMGmx
    {
        public VMLogin(IUserDialogs diag) : base(diag)
        {
            Title = "Inicio de sesión";
            IniciarSesionCommand = new Command(Boton);
            RecoverPassCommand = new Command(Recover);
            RegistrarCommand = new Command(Registrar);
        }

        public ICommand IniciarSesionCommand { get; private set; }
        public ICommand RecoverPassCommand { get; private set; }
        public ICommand RegistrarCommand { get; private set; }

        private string usuario;
        public string Usuario
        {
            get { return usuario; }
            set
            {
                if (usuario != value)
                {
                    usuario = value;
                    OnPropertyChanged("Usuario");
                }
            }
        }

        private string contrasena;
        public string Contrasena
        {
            get { return contrasena; }
            set
            {
                if (contrasena != value)
                {
                    contrasena = value;
                    OnPropertyChanged("Contrasena");
                }
            }
        }

        public string error;
        public string Error
        {
            get { return error; }
            set
            {
                if (error != value)
                {
                    error = value;
                    if (string.IsNullOrEmpty(error))
                        ErrorVisible = false;
                    else
                        ErrorVisible = true;
                    OnPropertyChanged("Error");
                    OnPropertyChanged("ErrorVisible");
                }
            }
        }

        public bool errorvisible;
        public bool ErrorVisible
        {
            get { return errorvisible; }
            set { errorvisible = value; }
        }

        private async void Boton()
        {
            LoginUsers luser = new LoginUsers();
            try
            {
                Ocupado = true;
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100));
                GMX.wsUser.Security ws = new GMX.wsUser.Security(config.Config["APIUsuarios"]);
                GMX.wsUser.bUsers user = ws.AuthenticateUser(Usuario, Contrasena, 4);//4 es app id de medicos
                App.usr = user;
                bindings b = new bindings();
                b.IniciaWS();
                var cod = new Dictionary<string, string>();
                cod.Add("agentId", user.ExtId.ToString());
                var crypdata = await b.getCatalog("GetAgentById", cod);
                var strdata = await b.decrypt(crypdata.Result);
                if (!String.IsNullOrEmpty(strdata.Result))
                {
                    Dictionary<int, agente> agent = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, agente>>(strdata.Result);
                    App.agent = agent.FirstOrDefault().Value;

                    wsbd.Service wsbd = new GMX.wsbd.Service();
                    string json = wsbd.get_catalogos("GetPvSuscriptorByPv", "@appId='4', @pv='1'");
                    suscriptor s = Newtonsoft.Json.JsonConvert.DeserializeObject<suscriptor>(json);
                    if (s.Table.FirstOrDefault() != null)
                        App.suscriptor = s.Table.FirstOrDefault();

                    //var Welcome = new Cotizar();
                    var Welcome = new MetodoPago(new VMCotizar(UserDialogs.Instance, App.navigation));
                    await App.navigation.PopToRootAsync();
                    var MainP = new NavigationPage(Welcome)
                    {
                        BarTextColor = Color.FromHex("#04b5b5"),
                        BarBackgroundColor = Color.White,
                    };
                    var md = new MasterDetailPage();
                    md.Master = new menu();
                    md.Detail = MainP;
                    App.Current.MainPage = md;
                    (Welcome.BindingContext as VMCotizar).ClickAuto = true;
                }
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                Ocupado = false; 
                await Diag.AlertAsync(Resources.UserNoExist, "Error", "OK");
            }
            finally
            {
                Ocupado = false;
            }
        }

		void Recover()
		{
            App.navigation.PushAsync(new RecoverPage());
		}

        private void Registrar()
        {
            App.navigation.PushAsync(new WelcomePage());
        }

    }
}
