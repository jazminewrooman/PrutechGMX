using Xamarin.Forms;
using GMX.Views;
using GMX.Services;
using Plugin.Connectivity;
using System.Threading.Tasks;
using System.Threading;
using Acr.UserDialogs;
using System;
using GMX.Services.DTOs;

namespace GMX.Views
{
	public partial class App : Application
	{

		public static INavigation navigation;
        public static agente agent;

        public App()
        {
            InitializeComponent();

            var mainp = new NavigationPage(new LoginUser())
            //var mainp = new NavigationPage(new MetodoPago())
            {
                //BackgroundColor = Color.White,
                //BarTextColor = Color.Black,
                BarTextColor = Color.FromHex("#04b5b5"),
                BarBackgroundColor = Color.White,
            };
            MainPage = mainp;
            App.navigation = mainp.Navigation;

            CancellationTokenSource ts = new CancellationTokenSource();
            CancellationToken ct = ts.Token;
            CrossConnectivity.Current.ConnectivityChanged += async (sender, args) =>
            {
                if (!args.IsConnected)
                {
                    try
                    {
                        await UserDialogs.Instance.AlertAsync(GMX.Resources.NoInternet, "Aviso", "OK", ct);
                    }
                    catch (OperationCanceledException)
                    {
                        ts = new CancellationTokenSource();
                        ct = ts.Token;
                    }
                }
                else
                {
                    ts.Cancel();
                    ts = new CancellationTokenSource();
                    ct = ts.Token;
                }
            };
        }

		static public bool DoBack
        {
            get
            {
                MasterDetailPage mainPage = App.Current.MainPage as MasterDetailPage;
                if (mainPage != null)
                {
                    bool canDoBack = mainPage.Detail.Navigation.NavigationStack.Count > 1 || mainPage.IsPresented;
                    if (!canDoBack)
                        return false;
                    else
                        return true;
                }
                return true;
            }
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
