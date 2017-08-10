using System;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Linq;

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

		void NavigateTo(MenuItem menu)
		{
			Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);
            var det = new NavigationPage(displayPage);
            App.navigation = det.Navigation;
            (App.Current.MainPage as MasterDetailPage).Detail = det;
		}

        public void cargamenu(GMX.wsUser.bUsers user)
        {
            Menu = new MenuListView();
            Menu.ItemSelected += (sender, e) =>
            {
                if ((e.SelectedItem as MenuItem).TargetType == null)
                {
                    //Navigation.InsertPageBefore(new LoginUser(), Navigation.NavigationStack.FirstOrDefault());
                    //await Navigation.PopToRootAsync();
                    var det = new NavigationPage(new LoginUser());
                    App.navigation = det.Navigation;
                    App.Current.MainPage = det;
                }
                else
                    NavigateTo(e.SelectedItem as MenuItem);
            };

            StackLayout menuLabel = new StackLayout
            {
                BackgroundColor = (Color)App.Current.Resources["ligthgray"],
                Padding = new Thickness(0, 0, 0, 0),
                //Spacing = 0,
                //HeightRequest = 40,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical,
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Image(){ Source = "dres", Aspect = Aspect.AspectFill, HeightRequest = 200 },
                    new StackLayout(){ BackgroundColor = Color.White, Padding = 20,
                        Children = { 
                            new Label(){ FontSize = 14, FontAttributes = FontAttributes.None, Text = "Bienvenido"},
                            new Label(){ FontSize = 16, FontAttributes = FontAttributes.Bold, Text = muser.DisplayName} 
                        },
                    },
                    Menu
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
            SeparatorColor = Color.Transparent;
            HasUnevenRows = true;
            //SeparatorVisibility = SeparatorVisibility.None;
            var cell = new DataTemplate(typeof(menucell));
            ItemTemplate = cell;
        }
    }

    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MenuItem()
            {
                Titulo = "Cotizar",
                Icono = "nocash.png",
                Color = Color.White,
                TextColor = Color.Black,
            });
			this.Add(new MenuItem()
			{
				Titulo = "Polizas emitidas",
				Icono = "document2.png",
				Color = Color.White,
				TextColor = Color.Black,
                TargetType = typeof(Resumen)
			});
			this.Add(new MenuItem()
			{
				Titulo = "Cerrar sesion",
				Icono = "cross.png",
				Color = Color.White,
				TextColor = Color.Black,
                TargetType = null
			});
        }


    }


}
    

