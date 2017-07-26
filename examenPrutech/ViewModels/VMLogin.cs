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
        }

        public ICommand IniciarSesionCommand { get; private set; }
        public ICommand RecoverPassCommand { get; private set; }

        private string usuario;
        public string Usuario
        {
            get { return usuario; }
            set
            {
                if (usuario != value)
                {
                    usuario = value;
                    NotifyPropertyChanged("Usuario");
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
                    NotifyPropertyChanged("Contrasena");
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
                    NotifyPropertyChanged("Error");
                    NotifyPropertyChanged("ErrorVisible");
                }
            }
        }

        public bool errorvisible;
        public bool ErrorVisible
        {
            get { return errorvisible; }
            set { errorvisible = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Eventos

        private async void Boton()
        {
            LoginUsers luser = new LoginUsers();
            try
            {
                Ocupado = true;
                GMX.wsUser.Security ws = new GMX.wsUser.Security();
                GMX.wsUser.bUsers user = ws.AuthenticateUser(Usuario, Contrasena, 1);
                GMX.wsUser.bUsers usrId = ws.GetUser(user.ExtId, 1);
                luser = ConvertUser(user);
                Ocupado = false;
                //var Welcome = new WelcomePage(luser);
                //Welcome.BindingContext = luser;

				var Welcome = new DatosGenerales();
                App.navigation.InsertPageBefore(Welcome, App.navigation.NavigationStack.FirstOrDefault());
                await App.navigation.PopToRootAsync();
                var MainP = new NavigationPage(Welcome)
				{
                    BarTextColor = Color.White,
                    BarBackgroundColor = Color.FromHex("#04b5b5"),
				};
                var md = new MasterDetailPage();
                md.Master = new menu(user);
                md.Detail = MainP;

                App.Current.MainPage = md;

				
            }
            catch
            {
                Ocupado = false;
                await Diag.AlertAsync("Usuario y/o contraseña no valido, favor de verificar", "Error", "OK");
            }
        }

		void Recover()
		{
            App.navigation.PushAsync(new RecoverPage());
			//App.navigation.PushAsync(new Views.DatosGenerales());
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
