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
        private string mmsgocupado;
        public string MsgOcupado
        {
            get => (!String.IsNullOrEmpty(mmsgocupado) ? $"{mmsgocupado}..." : "");
            set
            {
                if (mmsgocupado != value)
                {
                    mmsgocupado = value;
                    OnPropertyChanged("MsgOcupado");
                }
            }
        }

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
                        Device.BeginInvokeOnMainThread( () =>
                        {
                            Diag.ShowLoading($"Estamos trabajando.{Environment.NewLine}Permítenos procesar tu información.{Environment.NewLine}{MsgOcupado}", MaskType.Black);
                            //Task.Delay(TimeSpan.FromMilliseconds(10000));
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Diag.HideLoading();
                        });
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
