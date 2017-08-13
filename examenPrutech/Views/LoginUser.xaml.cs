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
			this.BindingContext = vm;
		}

        protected override void OnAppearing()
        {
			//(App.Current.MainPage as NavigationPage).BarBackgroundColor = Color.White;
			//(App.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
			//(App.Current.MainPage as NavigationPage).BarBackgroundColor = Color.FromHex("#04b5b5");
			//(App.Current.MainPage as NavigationPage).BarTextColor = Color.White;
			base.OnAppearing();
        }
	}
}