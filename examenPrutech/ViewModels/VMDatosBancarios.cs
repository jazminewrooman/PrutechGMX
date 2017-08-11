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

namespace GMX
{
	public class VMDatosBancarios : VMGmx
	{
		INavigation nav;

		public VMDatosBancarios(IUserDialogs diag, INavigation n) : base(diag)
		{
		}
	}
}
