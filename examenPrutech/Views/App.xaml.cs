using Xamarin.Forms;
using GMX.Views;
using GMX.Services;

namespace GMX.Views
{
	public partial class App : Application
	{

		public static INavigation navigation;

		public App()
		{
			InitializeComponent();

            //var mainp = new NavigationPage(new LoginUser())
            var mainp = new NavigationPage(new Cotizar())
            {
				//BackgroundColor = Color.White,
				//BarTextColor = Color.Black,
				BarTextColor = Color.White,
				BarBackgroundColor = Color.FromHex("#04b5b5"),
			};
            MainPage = mainp;
			App.navigation = mainp.Navigation;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
