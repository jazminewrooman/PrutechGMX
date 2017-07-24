using System;
using MvvmHelpers;
using Acr.UserDialogs;
         
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
                        Diag.ShowLoading("Cargando...");
                    else
                        Diag.HideLoading();
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
