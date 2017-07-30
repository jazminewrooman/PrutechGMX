using System;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;

namespace GMX.Droid
{
    [Activity(Label = "GMX", Icon = "@drawable/logogmx", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            Xamarin.FormsMaps.Init(this, bundle);
            UserDialogs.Init(this);

            LoadApplication(new GMX.Views.App());

            MessagingCenter.Subscribe<Page, string>(this, "Call", (sender, arg) =>
			{
			    Call(arg);
			});
        }

		private bool Call(string numero)
		{
			var context = Forms.Context;
			if (context == null)
				return false;

			var intent = new Intent(Intent.ActionDial);

            intent.SetData(Android.Net.Uri.Parse("tel:" + numero));

            if (intent.ResolveActivity(PackageManager) != null)
			{
				context.StartActivity(intent);
				return true;
			}
			return false;
		}

	}
}
