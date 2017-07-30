using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Foundation;
using UIKit;

namespace GMX.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			Xamarin.FormsMaps.Init();

            LoadApplication(new GMX.Views.App());

			MessagingCenter.Subscribe<Page, string>(this, "Call", (sender, arg) =>
			{
                Call(arg);
			});

			return base.FinishedLaunching(app, options);
		}

        private bool Call(string numero)
        {
			return UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + numero));
        }
	}
}
