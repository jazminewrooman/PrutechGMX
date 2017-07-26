using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace GMX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class VMRecover : VMGmx
    {
        public ICommand RecoverPassCommand { get; private set; }
        private INavigation nav;

        public VMRecover(IUserDialogs diag, INavigation n) : base(diag)
        {
            Title = "Recupera contraseña";
            RecoverPassCommand = new Command(Recover);
            nav = n;
        }

        private string correo;
        public string Correo
        {
            get { return correo; }
            set
            {
                if (correo != value)
                {
                    correo = value;
                    NotifyPropertyChanged("Correo");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        private async void Recover(){
			LoginUsers luser = new LoginUsers();
			bool exito;
			try
			{
                Ocupado = true;
				GMX.wsUser.Security ws = new GMX.wsUser.Security();
                exito = ws.SendPwdRecoveryMail(correo, 4);
                Ocupado = false;
                if (!exito)
					await UserDialogs.Instance.AlertAsync("Este correo no se encuentra registrado", "Aviso", "OK");
                await nav.PopAsync(true);
			}
			catch
			{
				
			}
		}

    }
}
