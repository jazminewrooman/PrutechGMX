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
using GMX.SegmentedControl.Android;
using GMX.Views;

namespace GMX.Droid
{
    [Activity(Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    //public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        public override void OnBackPressed()
        {
            if (App.DoBack)
                base.OnBackPressed();
            else
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage("¿Desea salir de la aplicación?");
                builder.SetPositiveButton("SI", (s, e) =>
                { /* do something on OK click */
                    base.OnBackPressed();
                });
                builder.SetNegativeButton("NO", (s, e) =>
                { /* do something on Cancel click */
                });
                builder.Create().Show();
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            SegmentedControlRenderer.Init(); 
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
