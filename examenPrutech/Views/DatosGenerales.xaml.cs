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

        public DatosGenerales()
        {
            InitializeComponent();
        }

        public DatosGenerales(DatosGralesModel dg, TipoDatos td, VMCotizar v, Modo modo)
        {
            InitializeComponent();
            vmcot = v;
            vm = new VMDatosGenerales(UserDialogs.Instance, Navigation, vmcot, td, modo);
            this.BindingContext = vm;

            edtDirecc.TextChanged += (sender, e) => edtDirecc.UpdateLayout();

            seg.ValueChanged += (sender, e) =>
            {
                if (seg.SelectedSegment == 0)
                {
                    vm.Persona = TipoPersona.Fisica;
                    lblNombre.IsVisible = true;
                    txtNombre.IsVisible = true;
                    lblAMaterno.IsVisible = true;
                    txtAMaterno.IsVisible = true;
                    lblAPaterno.Text = "Apellido paterno";
                }
                if (seg.SelectedSegment == 1)
                {
                    vm.Persona = TipoPersona.Moral;
                    lblNombre.IsVisible = false;
                    txtNombre.IsVisible = false;
                    lblAMaterno.IsVisible = false;
                    txtAMaterno.IsVisible = false;
                    lblAPaterno.Text = "Razón social";
                }
            };

            if (dg != null)
            {
                //Si el modelo trae datos, la vista servirá para editar datos existentes
                vm.CargaDatosGenerales(dg, td);
                seg.SelectedSegment = (int)dg.Persona;
            }

            //Si el modelo se encuentra vacio quiere decir que la vista servirá para captura de datos
            if (td == TipoDatos.Fiscales)
            {
                //Title = "Datos Fiscales";
                seg.IsVisible = true;
                btnFiscales.IsVisible = false;
                lblEmail.IsVisible = false;
                txtEmail.IsVisible = false;
            }
            else
            {
                //Title = "Datos Generales";
                seg.IsVisible = false;
                if (modo == Modo.Captura)
                    btnFiscales.IsVisible = true;
                if (modo == Modo.Edicion)
                    btnFiscales.IsVisible = false;
                lblEmail.IsVisible = true;
                txtEmail.IsVisible = true;
            }
        }
    }
}
