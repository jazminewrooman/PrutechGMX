using System;
using MvvmHelpers;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace GMX
{
    public class VMGmx : BaseViewModel
    {

        public IUserDialogs Diag;

        private bool mocupado;
        public bool Ocupado
        {
            get { return mocupado; }
            set
            {
                if (mocupado != value)
                {
                    if (value)
                    {
                        //Device.BeginInvokeOnMainThread(async () =>
                        //{
                        Diag.ShowLoading("Cargando...", MaskType.Black);
                            //Task.Delay(TimeSpan.FromMilliseconds(10000));
                        //});
                    }
                    else
                    {
                        //Device.BeginInvokeOnMainThread(() =>
                        //{
                            Diag.HideLoading();
                        //});
                    }
					mocupado = value;
                    OnPropertyChanged("Ocupado");
                }
            }
        }

        public VMGmx(IUserDialogs diag)
        {
            Diag = diag;
        }

    }
}
