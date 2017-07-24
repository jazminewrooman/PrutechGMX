using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace GMX.Views
{
	public partial class LoginUser : ContentPage
	{
		public VMLogin vm = new VMLogin(UserDialogs.Instance);
		public LoginUser()
		{
			InitializeComponent();

			Title = "Inicio de sesión";

			this.BindingContext = vm;
		}
	}
}