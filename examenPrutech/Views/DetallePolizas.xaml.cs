using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using GMX.Services.DTOs;
using Xamarin.Forms;

namespace GMX.Views
{
    public partial class DetallePolizas : ContentPage
    {
        public DetallePolizas() {}

        public DetallePolizas(polizaemitida res) 
        {
            InitializeComponent();

            Title = "Pólizas Emitidas";
            var vm = new VMDetallePoliza(res, UserDialogs.Instance, Navigation);
            BindingContext = vm;

            string polizagenerada = "";
            string[] numpol = res.Poliza.Split('_');
            if (numpol.Length > 0 && numpol.Length >= 3)
                polizagenerada = numpol[2];
            string numpoliza = $"{App.agent.cod_suc.PadLeft(3, '0')}-66-{polizagenerada.PadLeft(8, '0')}-0000-01";

            lblTiponegocio.Text = (res.Tipo_Negocio == 1 ? "Plan Tradicional" : "Plan Ángeles");
            lblNombre.Text = res.Nombre_Cliente;
            lblPoliza.Text = numpoliza;
            lblSuma.Text = $"{res.Suma_Asegurada.ToString("c")} M.N.";
            lblInicio.Text = res.Vigencia_Ini.ToString("dd/MM/yyyy");
            lblFin.Text = res.Vigencia_Fin.ToString("dd/MM/yyyy");
        }
    }
}
