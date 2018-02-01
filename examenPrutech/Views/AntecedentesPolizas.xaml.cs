using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;

namespace GMX.Views
{
	public partial class AntecedentesPolizas : ContentPage
	{
        public AntecedentesPolizas(VMCotizar vmcot, Modo modo)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

            }
            var vm = new VMAntecedentesPolizas(UserDialogs.Instance, Navigation, vmcot, modo);
            BindingContext = vm;
            Title = "Antecedentes pólizas";

            if (vmcot.Antecedentes != null)
            {
                if (vmcot.Antecedentes.poliza3 != null)
                    seg.SelectedSegment = 2;
                else if (vmcot.Antecedentes.poliza2 != null)
					seg.SelectedSegment = 1;
				else if (vmcot.Antecedentes.poliza1 != null)
					seg.SelectedSegment = 0;
			}
			seg.ValueChanged = (sender, e) =>
            {
                vm.MuestraGrid(seg.SelectedSegment + 1);
            };
        }
	}
}
