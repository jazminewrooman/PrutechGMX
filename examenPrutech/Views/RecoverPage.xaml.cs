using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GMX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecoverPage : ContentPage
	{
		public RecoverPage()
		{
			InitializeComponent();
            //Icon = "left.png";
			BindingContext = new VMRecover(UserDialogs.Instance, Navigation);
		}

		/*protected override void OnAppearing()
		{
			(App.Current.MainPage as NavigationPage).BarBackgroundColor = Color.White;
			(App.Current.MainPage as NavigationPage).BarTextColor = Color.Black;
			base.OnAppearing();
		}*/

	}
}