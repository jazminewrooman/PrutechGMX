using System;

using Xamarin.Forms;

namespace GMX.Views
{
    public class menucell : ViewCell
    {
        public menucell()
        {
            var icono = new Image
            {
                HeightRequest = 32,
                //WidthRequest = 30,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            icono.SetBinding(Image.SourceProperty, "Icono");

            var titulo = new Label()
            {
                VerticalOptions = LayoutOptions.Center,
                //FontFamily = "HelveticaNeue-Medium",
                FontSize = 16,
                //TextColor = Color.FromHex(App.PrimaryColor)
            };
            titulo.SetBinding(Label.TextProperty, "Titulo");

            var statusLayout = new StackLayout
            {
                Spacing = 10,
				Padding = new Thickness(10, 15, 10, 15),
                Orientation = StackOrientation.Horizontal,
                Children = { icono, titulo }
            };

            statusLayout.SetBinding(StackLayout.BackgroundColorProperty, "Color");
			titulo.SetBinding(Label.TextColorProperty, "TextColor");

			this.View = statusLayout;
        }
    }
}
