using Xamarin.Forms;
using GMX.Views;

namespace GMX.Views
{
	public partial class App : Application
	{

		public static INavigation navigation;

		public App()
		{
			InitializeComponent();

			var MainP = new NavigationPage(new LoginUser())
			{
				BarTextColor = Color.DarkOrchid,
				BarBackgroundColor = Color.White,
				//Title = "Listas",
			};

			MainPage = MainP;

			App.navigation = MainPage.Navigation;

			//MainPage = new GMXApp.LoginUser();
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
