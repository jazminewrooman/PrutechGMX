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
using Rg.Plugins.Popup.Extensions;
using GMX.Services;

namespace GMX
{
    public class VMDatosProfesionales : VMGmx
    {
        INavigation nav;
        public ICommand VerCotizaCommand { get; private set; }
        public ICommand NextCommand { get; private set; }
        Dictionary<string, especialidades> lstesp;
        VMCotizar vmcotizar;

        public VMDatosProfesionales(IUserDialogs diag, INavigation n, VMCotizar vmcot) : base(diag)
        {
            nav = n;
            vmcotizar = vmcot;
            Title = "Datos profesionales";
            Especialidad = -1;
            VerCotizaCommand = new Command(async () =>
            {
                await n.PushPopupAsync(new VerCotiza(vmcot), true);
            });
            NextCommand = new Command(async () =>
            {
                if (!Validar())
                    await Diag.AlertAsync(Resources.FaltanOb, "Error", "Ok");
                else
                {
                    FormatText();
                    if (vmcotizar.IdTipo == "2") //renovacion
                        await nav.PushAsync(new AntecedentesPolizas(vmcotizar));
					if (vmcotizar.IdTipo == "1") //nueva
                        await nav.PushAsync(new MetodoPago(vmcotizar));
				}
            });
            CargaDatosProfesionales(vmcot.DatosProf);
        }

        private string descripcion;
        public string Descripcion
        {
            get => descripcion;
            set
            {
                if (descripcion != value)
                {
                    descripcion = value;
                    OnPropertyChanged("Descripcion");
                }
            }
        }
        private int iddescripcion;
        public int IdDescripcion
        {
            get => iddescripcion;
            set
            {
                if (iddescripcion != value)
                {
                    iddescripcion = value;
					CargaEspec();
					OnPropertyChanged("IdDescripcion");
                }
            }
        }
        private List<string> especialidades;
        public List<string> Especialidades
        {
            get => especialidades;
            set
            {
                if (especialidades != value)
                {
                    especialidades = value;
                    OnPropertyChanged("Especialidades");
                }
            }
        }
        private int especialidad;
        public int Especialidad
        {
            get => especialidad;
            set
            {
                if (especialidad != value)
                {
                    especialidad = value;
					OnPropertyChanged("Especialidad");
                }
            }
        }
        private string cedulaprof;
        public string CedulaProf
        {
            get => cedulaprof;
            set
            {
                if (cedulaprof != value)
                {
                    cedulaprof = value;
                    OnPropertyChanged("CedulaProf");
                }
            }
        }
        private string cedulaesp;
        public string CedulaEsp
        {
            get => cedulaesp;
            set
            {
                if (cedulaesp != value)
                {
                    cedulaesp = value;
                    OnPropertyChanged("CedulaEsp");
                }
            }
        }
        private string diplomados;
        public string Diplomados
        {
            get => diplomados;
            set
            {
                if (diplomados != value)
                {
                    diplomados = value;
                    OnPropertyChanged("Diplomados");
                }
            }
        }
        private bool Validar()
        {
            if (Especialidad < 0 || String.IsNullOrEmpty(CedulaProf))
                return false;
            else
            {
                DatosProfesionales dp = new DatosProfesionales()
                {
                    Descripcion = Descripcion,
                    IdDescripcion = IdDescripcion,
                    Especialidad = int.Parse(lstesp.ElementAt(Especialidad).Value.cod_sst_riesgo),
                    CedulaProf = CedulaProf,
                    CedulaEsp = CedulaEsp,
                    Diplomados = Diplomados,
                };
                vmcotizar.DatosProf = dp;
                return true;
            }
        }

		private FormattedString FormatText()
		{
			var fs = new FormattedString();
			fs.Spans.Add(new Span { Text = "Descripcion: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = Descripcion + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = "Especialidad: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = Especialidades[Especialidad].ToString() +"\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = "Número de cédula profesional: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = CedulaProf + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = "Número de cédula especilidad: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = CedulaEsp + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = "Diplomados y otros estudios: ", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });
            fs.Spans.Add(new Span { Text = Diplomados + "\\n", ForegroundColor = Color.Black, FontAttributes = FontAttributes.Bold });

			return fs;
		}

        private void CargaDatosProfesionales(DatosProfesionales dp)
        {
            if (dp != null)
            {
                Descripcion = dp.Descripcion;
                IdDescripcion = dp.IdDescripcion;
                //if (Especialidades != null && lstesp != null)
                //    Especialidad = Especialidades.IndexOf(lstesp.Where(x => x.Value.cod_sst_riesgo == dp.Especialidad.ToString()).FirstOrDefault().Value.txt_desc);
                Especialidad = dp.Especialidad;
                CedulaProf = dp.CedulaProf;
                CedulaEsp = dp.CedulaEsp;
                Diplomados = dp.Diplomados;
            }
            else
            {
                if (!vmcotizar.Cirugias)
                {
                    Descripcion = Resources.SinCirugia;
                    IdDescripcion = 22;
                }
                else
                {
                    Descripcion = Resources.ConCirugia;
                    IdDescripcion = 23;
                }
            }
        }

        private async void CargaEspec()
        {
            try
            {
                bindings b = new bindings();
                b.IniciaWS();
                var cod = new Dictionary<string, string>();
                cod.Add("profesionId", "8");
                cod.Add("descProfId", IdDescripcion.ToString());
                Ocupado = true;
                var crypdata = await b.getCatalog("GetEspecialidadesByProfesionAndDescProf", cod);
                var strdata = await b.decrypt(crypdata.Result);
                lstesp = JsonConvert.DeserializeObject<Dictionary<string, especialidades>>(strdata.Result);
                if (lstesp != null && lstesp.Count > 0)
                    Especialidades = lstesp.Select(x => x.Value.txt_desc).ToList();
                if (Especialidad < 0)
                    Especialidad = 0;
            }
            finally
            {
                Ocupado = false;
				if (Especialidades != null && lstesp != null && Especialidad > 0)
					Especialidad = Especialidades.IndexOf(lstesp.Where(x => x.Value.cod_sst_riesgo == Especialidad.ToString()).FirstOrDefault().Value.txt_desc);
			}
        }

    }
}
