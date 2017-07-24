using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace GMX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class menu : ContentPage
    {
        public ListView Menu { get; set; }
        StackLayout layout;

        public void Refrescamenu()
        {
            cargamenu();
        }

        public menu()
        {
            Title = "Menu";
            Icon = "slideout.png";
            cargamenu();
        }

        public void cargamenu()
        {
            Menu = new MenuListView();
            //Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            StackLayout menuLabel = new StackLayout
            {
                Padding = new Thickness(10, 0, 10, 0),
                //Spacing = 0,
                //HeightRequest = 40,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Horizontal,
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Label{ Text = "Bienvenido"},
                }
            };

            Content = menuLabel;
        }

    }

    public class MenuItem
    {
        public string Titulo { get; set; }
        public string Icono { get; set; }
        public Type TargetType { get; set; }
        public Color Color { get; set; }
        public Color TextColor { get; set; }
    }

    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = new MenuListData();
            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;
            var cell = new DataTemplate(typeof(menucell));
            ItemTemplate = cell;
        }
    }

    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            Color txt, back;

            this.Add(new MenuItem()
            {
                Titulo = "Mi perfil",
                //Icono = "profle.png",
                Color = Color.White,
                TextColor = Color.Black,
            });
        }


    }


}
    

