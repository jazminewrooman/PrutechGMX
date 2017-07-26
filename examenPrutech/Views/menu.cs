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
        private GMX.wsUser.bUsers muser;

        public void Refrescamenu()
        {
            cargamenu(muser);
        }

        public menu(GMX.wsUser.bUsers user)
        {
            Title = "Menu";
            Icon = "slideout.png";
            muser = user;
            cargamenu(muser);
        }

        public void cargamenu(GMX.wsUser.bUsers user)
        {
            Menu = new MenuListView();
            //Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            StackLayout menuLabel = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 0),
                //Spacing = 0,
                //HeightRequest = 40,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical,
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Image(){ Source = "dres", Aspect = Aspect.AspectFill, HeightRequest = 120 },
                    new StackLayout(){ BackgroundColor = Color.White, Padding = 20,
                        Children = { new Label(){ FontSize = 12, FontAttributes = FontAttributes.Bold, Text = muser.DisplayName} },
                    }
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
    

