using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GMX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WelcomePage : ContentPage
	{
		public WelcomePage()
		{
			InitializeComponent();

            btnCall.Clicked += (s, e) =>
            {
                MessagingCenter.Send<Page, string>(this, "Call", "5554804100");
            };

            btnEmail.Clicked += (sender, e) => {
                Device.OpenUri(new Uri("mailto:soporte@gmx.com.mx"));
            };
		}
	}
}