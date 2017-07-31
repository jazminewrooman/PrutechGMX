using System;
using System.ComponentModel;
using Acr.UserDialogs;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;
using System.Linq;
using GMX.Views;
using GMX.Services;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using GMX.Services.DTOs;

namespace GMX
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public class VMDatosGenerales : VMGmx
	{
        private Dictionary<string, estado> lstedos;
        private Dictionary<string, municipio> lstmun;
        private Dictionary<string, ciudad> lstciud;
        private Dictionary<string, colonia> lstcols;

		public VMDatosGenerales(IUserDialogs diag) : base(diag)
		{
            Title = "DATOS GENERALES";

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
                    //OnPropertyChanged("RFC");
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
                    //if (!value)
                    //    Diag.AlertAsync("El RFC es incorrecto, verifique por favor", "Error", "OK");
					//OnPropertyChanged("RFCValido");
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
                        CargaCatalogos(value);
                    }
                    OnPropertyChanged("CP");
                }
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
                    Colonia = 0;
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
                    Estado = 0;
                    Municipio = 0;
                    Ciudad = 0;
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
                        CargaColonias(cp, lstedos.ElementAt(estado).Value.cod_dpto, lstmun.ElementAt(municipio).Value.cod_municipio, lstciud.ElementAt(ciudad).Value.cod_ciudad);
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
	}
}
