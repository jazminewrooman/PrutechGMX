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
		public VMDatosGenerales vm = new VMDatosGenerales(UserDialogs.Instance);

		public DatosGenerales(DatosGralesModel dgmodel, TipoDatos td)
		{
			InitializeComponent();

			this.BindingContext = vm;

            seg.ValueChanged += (sender, e) => {
                
            };

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
