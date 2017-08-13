using System;
using System.ComponentModel;
using Acr.UserDialogs;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Linq;
using GMX.Views;
using GMX.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using GMX.Services.DTOs;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;

namespace GMX
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public class VMDatosGenerales : VMGmx
	{
		INavigation nav;
		private bool datosdeservicio = false;
        private Dictionary<string, estado> lstedos;
        private Dictionary<string, municipio> lstmun;
        private Dictionary<string, ciudad> lstciud;
        private Dictionary<string, colonia> lstcols;
        public ICommand VerCotizaCommand { get; private set; }

        public VMDatosGenerales(IUserDialogs diag, INavigation n, VMCotizar vmcot) : base(diag)
		{
            nav = n;
            Title = "Datos Generales";
			VerCotizaCommand = new Command(async () =>
			{
                await n.PushPopupAsync(new VerCotiza(vmcot), true);
			});
		}

		string rfc;
		public string RFC
		{
			get { return rfc; }
			set
			{
				if (rfc != value)
				{
                    rfc = value;
                    OnPropertyChanged("RFC");
				}
			}
		}

		bool rfcvalido;
		public bool RFCValido
		{
			get { return rfcvalido; }
			set
			{
				if (rfcvalido != value)
				{
					rfcvalido = value;
                    if (value)
					    CargaRFC();
					OnPropertyChanged("RFCValido");
				}
			}
		}

		string nombre;
		public string Nombre
		{
			get { return nombre; }
			set
			{
				if (nombre != value)
				{
					nombre = value;
					OnPropertyChanged("Nombre");
				}
			}
		}

		string apaterno;
		public string APaterno
		{
			get { return apaterno; }
			set
			{
				if (apaterno != value)
				{
					apaterno = value;
					OnPropertyChanged("APaterno");
				}
			}
		}

		string amaterno;
		public string AMaterno
		{
			get { return amaterno; }
			set
			{
				if (amaterno != value)
				{
					amaterno = value;
					OnPropertyChanged("AMaterno");
				}
			}
		}

		string direccion;
		public string Direccion
		{
			get { return direccion; }
			set
			{
				if (direccion != value)
				{
					direccion = value;
					OnPropertyChanged("Direccion");
				}
			}
		}


		string telefono;
		public string Telefono
		{
			get { return telefono; }
			set
			{
				if (telefono != value)
				{
					telefono = value;
					OnPropertyChanged("Telefono");
				}
			}
		}

		string cp;
		public string CP
        {
            get { return cp; }
            set
            {
                if (cp != value)
                {
                    cp = value;
                    if (cp.Trim().Length == 5)
                    {
                        if (!datosdeservicio)
                            CargaCatalogos(value);
                    }
                    OnPropertyChanged("CP");
                }
            }
        }

        private async Task CargaRFC(){
			bindings b = new bindings();
			b.IniciaWS();
			var cod = new Dictionary<string, string>();
            cod.Add("clientRFC", RFC);
			Ocupado = true;
			try
			{
				var crypdata = await b.getCatalog("GetClientByRFC", cod);
				var strdata = await b.decrypt(crypdata.Result);
                KeyValuePair<string, cliente> cli = JsonConvert.DeserializeObject<Dictionary<string, cliente>>(strdata.Result).FirstOrDefault();
                if (cli.Value != null)
                {
                    datosdeservicio = true;
                    cliente c = cli.Value;
                    Nombre = c.txt_nombre;
                    APaterno = c.txt_apellido1;
                    AMaterno = c.txt_apellido2;
                    Direccion = $"{c.calle} {c.num_ext} {c.num_int}";
                    Telefono = c.txt_telefono;
                    CP = c.nro_cod_postal;
                    await CargaCatalogos(c.nro_cod_postal);
                    Estado = Estados.IndexOf(lstedos.Where(x => x.Value.cod_dpto == c.cod_estado).FirstOrDefault().Value.txt_desc);
                    Municipio = Municipios.IndexOf(lstmun.Where(x => x.Value.cod_municipio == c.cod_municipio).FirstOrDefault().Value.txt_desc);
                    await CargaColonias(CP, c.cod_estado, c.cod_municipio, c.cod_ciudad);
                    Ciudad = Ciudades.IndexOf(lstciud.Where(x => x.Value.cod_ciudad == c.cod_ciudad).FirstOrDefault().Value.txt_desc);
                    Colonia = Colonias.IndexOf(lstcols.Where(x => x.Value.cod_colonia == c.cod_colonia).FirstOrDefault().Value.txt_desc);
                }
                else
                    datosdeservicio = false;
			}
			finally
			{
				Ocupado = false;
				await Task.Delay(TimeSpan.FromMilliseconds(100));
			}
        }

        private async Task CargaColonias(string cp, string idedo, string idmuncp, string idciudad)
        {
            bindings b = new bindings();
            b.IniciaWS();
            var cod = new Dictionary<string, string>();
            cod.Add("codpost", cp);
            cod.Add("estadoId", idedo);
            cod.Add("muncpId", idmuncp);
            cod.Add("ciudadId", idciudad);
            Ocupado = true;
            try
            {
                var crypdata = await b.getCatalog("GetColoniasMexByDetailedInfo", cod);
                var strdata = await b.decrypt(crypdata.Result);
                lstcols = JsonConvert.DeserializeObject<Dictionary<string, colonia>>(strdata.Result);
                if (lstcols != null && lstcols.Count > 0)
                    Colonias = lstcols.Select(x => x.Value.txt_desc).ToList();
                else
                    Colonias = null;
            }
            finally
            {
                Ocupado = false;
                await Task.Delay(TimeSpan.FromMilliseconds(100));
                if (lstcols != null && lstcols.Count > 0)
                {
                    if (!datosdeservicio)
                        Colonia = 0;
                }
                else
                    Colonia = -1;
            }
        }

        private async Task CargaCatalogos(string cp)
        {
            bindings b = new bindings();
            b.IniciaWS();
            var cod = new Dictionary<string, string>();
            cod.Add("codpost", cp);
            Ocupado = true;
            try
            {
                var crypdata = await b.getCatalog("GetEstadosMexByCodpost", cod);
                var strdata = await b.decrypt(crypdata.Result);
                lstedos = JsonConvert.DeserializeObject<Dictionary<string, estado>>(strdata.Result);
                if (lstedos != null && lstedos.Count > 0)
                {
                    Estados = lstedos.Select(x => x.Value.txt_desc).ToList();
                    cod.Add("estadoId", lstedos["1"].cod_dpto);
                    crypdata = await b.getCatalog("GetMuncpsMexByEstadoAndCodpost", cod);
                    strdata = await b.decrypt(crypdata.Result);
                    lstmun = JsonConvert.DeserializeObject<Dictionary<string, municipio>>(strdata.Result);
                    Municipios = lstmun.Select(x => x.Value.txt_desc).ToList();
                    crypdata = await b.getCatalog("GetCiudadesMexByEstadoAndCodpost", cod);
                    strdata = await b.decrypt(crypdata.Result);
                    lstciud = JsonConvert.DeserializeObject<Dictionary<string, ciudad>>(strdata.Result);
                    Ciudades = lstciud.Select(x => x.Value.txt_desc).ToList();
                }
                else
                    Estados = null;
            }
            finally
            {
                Ocupado = false;
                if (Estados == null || Estados.Count == 0)
                {
					Municipios = null;
					Ciudades = null;
					Estado = -1;
                    Municipio = -1;
                    Ciudad = -1;
                    await Diag.AlertAsync("No existe el Codigo Postal, favor de verificar", "Error", "OK");
                }
                else
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(100));
                    if (!datosdeservicio)
                    {
                        Estado = 0;
                        Municipio = 0;
                        Ciudad = 0;
                    }
                }
            }
        }

		int estado = -1;
		public int Estado
		{
			get { return estado; }
			set
			{
				if (estado != value)
				{
					estado = value;
					OnPropertyChanged("Estado");
				}
			}
		}

		List<string> estados;
		public List<string> Estados
		{
			get { return estados; }
			set
			{
				if (estados != value)
				{
					estados = value;
					OnPropertyChanged("Estados");
				}
			}
		}

		int municipio = -1;
		public int Municipio
		{
			get { return municipio; }
			set
			{
				if (municipio != value)
				{
					municipio = value;
					OnPropertyChanged("Municipio");
				}

			}
		}

		List<string> municipios;
		public List<string> Municipios
		{
			get { return municipios; }
			set
			{
				if (municipios != value)
				{
					municipios = value;
					OnPropertyChanged("Municipios");
				}

			}
		}

		int ciudad = -1;
		public int Ciudad
		{
			get { return ciudad; }
			set
			{
				if (ciudad != value)
				{
					ciudad = value;
                    if (estado > -1 && municipio > -1 && ciudad > -1)
                    {
                        if (!datosdeservicio)
                            CargaColonias(cp, lstedos.ElementAt(estado).Value.cod_dpto, lstmun.ElementAt(municipio).Value.cod_municipio, lstciud.ElementAt(ciudad).Value.cod_ciudad);
                    }
					OnPropertyChanged("Ciudad");
				}
			}
		}

		List<string> ciudades;
		public List<string> Ciudades
		{
			get { return ciudades; }
			set
			{
				if (ciudades != value)
				{
					ciudades = value;
					OnPropertyChanged("Ciudades");
				}
			}
		}

		int colonia = -1;
		public int Colonia
		{
			get { return colonia; }
			set
			{
				if (colonia != value)
				{
					colonia = value;
					OnPropertyChanged("Colonia");
				}
			}
		}

		List<string> colonias;
		public List<string> Colonias
		{
			get { return colonias; }
			set
			{
				if (colonias != value)
				{
					colonias = value;
					OnPropertyChanged("Colonias");
				}
			}
		}

		string correo;
		public string Correo
		{
			get { return correo; }
			set
			{
				if (correo != value)
				{
					correo = value;
					OnPropertyChanged("Correo");
				}
			}
		}

		public void CargaDatosGenerales(DatosGralesModel dgmodel, TipoDatos td)
		{
			RFC = dgmodel.RFC;
			Nombre = dgmodel.Nombre;
			APaterno = dgmodel.APaterno;
			AMaterno = dgmodel.AMaterno;
			Direccion = dgmodel.Direccion;
			Telefono = dgmodel.Telefono;
			CP = dgmodel.CP;
			Estado = dgmodel.Estado;
			Municipio = dgmodel.Municipio;
			Ciudad = dgmodel.Ciudad;
			Colonia = dgmodel.Colonia;
			Correo = dgmodel.Correo;

			if (td.Equals(TipoDatos.Fiscales))
				Title = "DATOS FISCALES";
			else
				Title = "DATOS GENERALES";
		}
	}
}
