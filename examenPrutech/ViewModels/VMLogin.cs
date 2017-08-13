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

        #region Eventos

        private async void Boton()
        {
            LoginUsers luser = new LoginUsers();
            try
            {
                Ocupado = true;
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromMilliseconds(100));
                GMX.wsUser.Security ws = new GMX.wsUser.Security(config.Config["APIUsuarios"]);
                GMX.wsUser.bUsers user = ws.AuthenticateUser(Usuario, Contrasena, 1);
                GMX.wsUser.bUsers usrId = ws.GetUser(user.ExtId, 1);
                luser = ConvertUser(user);
                Ocupado = false;
                var Welcome = new Cotizar();
                App.navigation.InsertPageBefore(Welcome, App.navigation.NavigationStack.FirstOrDefault());
                await App.navigation.PopToRootAsync();
                var MainP = new NavigationPage(Welcome)
				{
                    //Icon = "slideout.png",
					BarTextColor = Color.FromHex("#04b5b5"),
					BarBackgroundColor = Color.White,
				};
                //var md = new GMX.Controls.MyMasterDetail();
                var md = new MasterDetailPage();
                md.Master = new menu(user);
                md.Detail = MainP;

                App.Current.MainPage = md;

				
            }
            catch(Exception ex)
            {
                Ocupado = false;
                await Diag.AlertAsync("Usuario y/o contraseña no valido, favor de verificar", "Error", "OK");
            }
        }

		void Recover()
		{
            App.navigation.PushAsync(new RecoverPage());
		}

        private void Registrar(){
            App.navigation.PushAsync(new WelcomePage());
        }

        #endregion

        #region Metodos

        public LoginUsers ConvertUser(GMX.wsUser.bUsers user)
        {
            LoginUsers usr = new LoginUsers();

            usr.CryptPwd = user.CryptPwd;
            usr.DisplayName = user.DisplayName;
            usr.Email = user.Email;
            usr.ExpDate = user.ExpDte;
            usr.ExtId = user.ExtId;
            usr.ParentId = user.ParentId;
            usr.Roles = user.Roles;
            usr.Source = user.Source;
            usr.UserId = user.UserId;
            usr.UserName = user.UserName;

            return usr;
        }

        #endregion
    }
}
