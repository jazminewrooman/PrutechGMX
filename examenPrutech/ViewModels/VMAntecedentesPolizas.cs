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
using GMX.Models;
using Rg.Plugins.Popup.Extensions;

namespace GMX
{
    public class VMAntecedentesPolizas : VMGmx
    {
        public PolizasAnteriores polizas { get; set; }
        INavigation nav;
        VMCotizar vmcotizar;
        public ICommand NextCommand { get; private set; }
        public ICommand VerCotizaCommand { get; private set; }

        public VMAntecedentesPolizas(IUserDialogs diag, INavigation n, VMCotizar vmcot) : base(diag)
        {
            nav = n;
            vmcotizar = vmcot;
			VerCotizaCommand = new Command(async () =>
			{
				await nav.PushPopupAsync(new VerCotiza(vmcot), true);
			});
			NextCommand = new Command(async () =>
            {
                if (!Validar())
                    await Diag.AlertAsync(Resources.FaltanOb, "Error", "Ok");
                else
                {
                    vmcotizar.Antecedentes = polizas;
                    await nav.PushAsync(new MetodoPago(vmcotizar));
                }
            });
            List<string> temp = new List<string>();
            for (int i = 2000; i < DateTime.Now.Year; i++)
                temp.Add(i.ToString());
            LstAnos = temp;
            if (vmcotizar.Antecedentes != null)
                CargaDatos();
            else
            {
				poliza1 = new NumPoliza()
				{
                    oficina = App.agent.cod_suc.PadLeft(2, '0'),
					producto = "66",
					endoso = "0000"
				};
				polizas = new PolizasAnteriores()
                {
                    poliza1 = poliza1,
                    dia = vmcotizar.IniVig.Day,
                    mes = vmcotizar.IniVig.Month
                };
                OnPropertyChanged("polizas");
            }
        }

        private void CargaDatos(){
            NumPoliza tmp;
            if (vmcotizar.Antecedentes != null)
            {
				polizas = new PolizasAnteriores()
				{
					dia = vmcotizar.Antecedentes.dia,
					mes = vmcotizar.Antecedentes.mes,
                    anno = vmcotizar.Antecedentes.anno
				};
                Anno = polizas.anno.ToString();
                if (vmcotizar.Antecedentes.poliza1 != null)
                {
                    tmp = vmcotizar.Antecedentes.poliza1;
					poliza1 = new NumPoliza()
					{
						oficina = tmp.oficina,
						producto = tmp.producto,
						endoso = tmp.endoso,
                        poliza = tmp.poliza,
                        renovacion = tmp.renovacion
					};
                    polizas.poliza1 = poliza1;
                }
				if (vmcotizar.Antecedentes.poliza2 != null)
				{
					tmp = vmcotizar.Antecedentes.poliza2;
					poliza2 = new NumPoliza()
					{
						oficina = tmp.oficina,
						producto = tmp.producto,
						endoso = tmp.endoso,
						poliza = tmp.poliza,
						renovacion = tmp.renovacion
					};
                    polizas.poliza2 = poliza2;
                    OnPropertyChanged("Muestra2");
				}
                if (vmcotizar.Antecedentes.poliza3 != null)
				{
					tmp = vmcotizar.Antecedentes.poliza3;
					poliza3 = new NumPoliza()
					{
						oficina = tmp.oficina,
						producto = tmp.producto,
						endoso = tmp.endoso,
						poliza = tmp.poliza,
						renovacion = tmp.renovacion
					};
                    polizas.poliza3 = poliza3;
                    OnPropertyChanged("Muestra3");
				}
                OnPropertyChanged("polizas");
            }
        }

        private bool Validar()
        {
            bool ret = false;
            if (String.IsNullOrEmpty(Anno) || String.IsNullOrEmpty(polizas.poliza1.poliza) || String.IsNullOrEmpty(polizas.poliza1.renovacion) || polizas.poliza1.poliza.Length < 8 || polizas.poliza1.renovacion.Length < 2)
                ret = false;
            else
            {
                polizas.poliza1.fullpoliza = $"{polizas.poliza1.oficina}-{polizas.poliza1.producto}-{polizas.poliza1.poliza}-{polizas.poliza1.endoso}-{polizas.poliza1.renovacion}";
                ret = true;
                polizas.anno = int.Parse(Anno);
                if (Muestra2)
                {
                    if (String.IsNullOrEmpty(poliza2.poliza) || String.IsNullOrEmpty(poliza2.renovacion) || poliza2.poliza.Length < 8 || poliza2.renovacion.Length < 2)
                        ret = false;
                    else
                    {
                        polizas.poliza2 = poliza2;
                        polizas.poliza2.fullpoliza = $"{polizas.poliza2.oficina}-{polizas.poliza2.producto}-{polizas.poliza2.poliza}-{polizas.poliza2.endoso}-{polizas.poliza2.renovacion}";
                        //return true;
                    }
                }
                if (Muestra3)
                {
                    if (String.IsNullOrEmpty(poliza3.poliza) || String.IsNullOrEmpty(poliza3.renovacion) || poliza3.poliza.Length < 8 || poliza3.renovacion.Length < 2)
                        ret = false;
                    else
                    {
                        polizas.poliza3 = poliza3;
                        polizas.poliza3.fullpoliza = $"{polizas.poliza3.oficina}-{polizas.poliza3.producto}-{polizas.poliza3.poliza}-{polizas.poliza3.endoso}-{polizas.poliza3.renovacion}";
                        //return true;
                    }
                }
            }
            return (ret);
        }

        public bool Muestra2 { get; set; }
        public bool Muestra3 { get; set; }
		private List<string> lstanos;
		public List<string> LstAnos
		{
			get => lstanos;
			set
			{
				if (lstanos != value)
				{
					lstanos = value;
					OnPropertyChanged("LstAnos");
				}
			}
		}
		private string anno;
        public string Anno
		{
			get => anno;
			set
			{
				if (anno != value)
				{
					anno = value;
					OnPropertyChanged("Anno");
				}
			}
		}
		private NumPoliza p1;
        public NumPoliza poliza1
        {
            get => p1;
            set
            {
                if (p1 != value)
                {
                    p1 = value;
                    OnPropertyChanged("poliza1");
                }
            }
        }
        private NumPoliza p2;
		public NumPoliza poliza2
        {
            get => p2;
            set
            {
                if (p2 != value)
                {
                    p2 = value;
                    OnPropertyChanged("poliza2");
                }
            }
        }
		private NumPoliza p3;
		public NumPoliza poliza3
		{
			get => p3;
			set
			{
				if (p3 != value)
				{
					p3 = value;
					OnPropertyChanged("poliza3");
				}
			}
		}
		public void MuestraGrid(int num)
        {
			if (num == 1)
			{
                Muestra2 = false;
				Muestra3 = false;
				OnPropertyChanged("Muestra2");
				OnPropertyChanged("Muestra3");
                poliza2 = null;
				poliza3 = null;
			}
            if (num == 2)
            {
                Muestra2 = true;
                Muestra3 = false;
                OnPropertyChanged("Muestra2");
                OnPropertyChanged("Muestra3");
                if (poliza2 == null)
                {
                    poliza2 = new NumPoliza()
                    {
                        oficina = App.agent.cod_suc.PadLeft(2, '0'),
                        producto = "66",
                        endoso = "0000"
                    };
                }
                poliza3 = null;
            }
			if (num == 3)
			{
				Muestra2 = true;
				Muestra3 = true;
				OnPropertyChanged("Muestra2");
				OnPropertyChanged("Muestra3");
                if (poliza2 == null)
                {
                    poliza2 = new NumPoliza()
                    {
                        oficina = App.agent.cod_suc.PadLeft(2, '0'),
                        producto = "66",
                        endoso = "0000"
                    };
                }
                if (poliza3 == null)
                {
                    poliza3 = new NumPoliza()
                    {
                        oficina = App.agent.cod_suc.PadLeft(2, '0'),
                        producto = "66",
                        endoso = "0000"
                    };
                }
			}
		}
    }
}
