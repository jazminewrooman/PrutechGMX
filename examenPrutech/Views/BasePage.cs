#if __IOS__
using UIKit;
#endif

using System;
using Xamarin.Forms;

namespace GMX.Views
{
    public partial class BasePage : ContentPage
    {
        static double h;
        static double w;

        public BasePage()
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var content = base.Content as ScrollView;
            content.BackgroundColor = Color.Transparent;
			var layout = new AbsoluteLayout();
			if (content != null)
			{
#if __ANDROID__
                BackgroundImage = "medicosneg.png";
#endif
#if __IOS__
				w = UIScreen.MainScreen.Bounds.Width;
				h = UIScreen.MainScreen.Bounds.Height;
				Image img = new Image()
				{
					Source = "medicosneg.png",
                    Aspect = Aspect.AspectFit
				};
                layout.Children.Add(img, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All); 
                layout.Children.Add(content, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

                content.Content = layout;
#endif
			}


			/*Image fondo = new Image() { Source = "medicosneg.png", WidthRequest = base.Content.Width };
            BackgroundImage = fondo;*/

			/*var content = base.Content as ScrollView;
			if (content != null)
			{
                Image fondo = new Image() { Source = "medicosneg.png", WidthRequest = content.Width };
			}*/

		}
    }
}

