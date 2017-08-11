using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class AntecedentesPolizas : ContentPage
	{
		public AntecedentesPolizas()
		{
			InitializeComponent();

			var vm = new VMAntecedentesPolizas(UserDialogs.Instance, Navigation);
			BindingContext = vm;
			Title = "Antecedentes pólizas";
		}
	}
}
