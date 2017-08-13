using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GMX.SegmentedControl;

namespace GMX.Views
{
[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatosGenerales : ContentPage
	{
        public VMCotizar vmcot;
        public VMDatosGenerales vm;

        public DatosGenerales(DatosGralesModel dgmodel, TipoDatos td, VMCotizar v)
		{
			InitializeComponent();
			vmcot = v;
            vm = new VMDatosGenerales(UserDialogs.Instance, Navigation, vmcot);
			this.BindingContext = vm;

            //seg.ValueChanged += async (sender, e) => {
            //    await DisplayAlert("a", seg.SelectedSegment.ToString(), "ok");
            //};

			if (dgmodel != null)
			{
				//Si el modelo trae datos, la vista servirá para editar datos existentes
				vm.CargaDatosGenerales(dgmodel, td);
			}
			else 
			{
				//Si el modelo se encuentra vacio quiere decir que la vista servirá para captura de datos
				if (td.Equals(TipoDatos.Fiscales))
					Title = "DATOS FISCALES";
				else
					Title = "DATOS GENERALES";

			}


		}

	}
}
