using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;

namespace GMX.Views
{
    public partial class VerConfirma : PopupPage
    {
        //public bool seleccion { get; set; }
        TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        public VerConfirma()
        {

        }

        public Task<bool> Regresa()
        {
            return tcs.Task;
        }

        public VerConfirma(VMCotizar vm)
        {
            InitializeComponent();


            BindingContext = vm;

            btnOk.Clicked += (s, e) =>
            {
                //seleccion = true;
                tcs.TrySetResult(true);
                Navigation.PopPopupAsync(true);
            };
            btnCerrar.Clicked += (s, e) =>
            {
                //seleccion = false;
                tcs.TrySetResult(false);
                Navigation.PopPopupAsync(true);
            };
        }
    }
}
