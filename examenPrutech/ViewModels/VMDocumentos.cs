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
using GMX.Services;

namespace GMX
{
    public class VMDocumentos : VMGmx
    {
        INavigation nav;
        VMCotizar vmcotizar;

        public VMDocumentos(IUserDialogs diag, INavigation n, VMCotizar vmcot) : base(diag)
        {
            nav = n;
            vmcotizar = vmcot;
            Title = "Pólizas Emitidas";
        }
    }
}
