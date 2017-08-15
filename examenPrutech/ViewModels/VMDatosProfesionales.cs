using System;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Collections.Generic;
using GMX.Views;
using System.Collections.ObjectModel;
using GMX.Services.DTOs;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;

namespace GMX
{
	public class VMDatosProfesionales : VMGmx
	{
		INavigation nav;
		public ICommand VerCotizaCommand { get; private set; }
		public ICommand NextCommand { get; private set; }
		
        public VMDatosProfesionales(IUserDialogs diag, INavigation n, VMCotizar vmcot) : base(diag)
        {
            nav = n;
            VerCotizaCommand = new Command(async () =>
			{
			    await n.PushPopupAsync(new VerCotiza(vmcot), true);
			});
            NextCommand = new Command(async () =>
            {
                await nav.PushAsync(new AntecedentesPolizas());
            });
        }
	}
}
