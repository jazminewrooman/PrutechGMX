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
using CreditCardValidator;
using Rg.Plugins.Popup.Extensions;

namespace GMX
{
    public class VMDetallePoliza : VMGmx
    {
		VMCotizar vmcotizar;
		INavigation nav;


        public VMDetallePoliza(IUserDialogs diag, INavigation n, VMCotizar vmc) : base(diag)
        {
            nav = n;
            vmcotizar = vmc;


        }
    }
}
