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
				//string res;
				if (rfc != value)
				{
                    rfc = (value.Length > 13 ? value.Substring(0, 13) : value);
                    //OnPropertyChanged("RFC");
				}
				//if (!String.IsNullOrEmpty(rfc))
				//{ 
				//	string pattern = @"[A-Z,Ñ,&]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[A-Z|\d]{3}";
				//	string input = @rfc;

				//	Match m = Regex.Match(input, pattern);
				//	bool x = m.Success;
				//	if (m.Success)
				//		res = "OK";
				//}
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

        private async Task CargaCatalogos(string cp)
        {
            bindings b = new bindings();
            b.IniciaWS();

            var cod = new Dictionary<string, string>();
            cod.Add("codpost", cp);
            Ocupado = true;

            var crypdata = await b.getCatalog("GetEstadosMexByCodpost", cod);
            var strdata = await b.decrypt(crypdata.Result);
            Dictionary<string, estado> lstedos = JsonConvert.DeserializeObject<Dictionary<string,estado>>(strdata.Result);
            Estados = lstedos.Select(x => x.Value.txt_desc).ToList();
            if (Estados.Count > 0)
                Estado = 0;

            cod.Add("estadoId", lstedos["1"].cod_dpto);
			crypdata = await b.getCatalog("GetMuncpsMexByEstadoAndCodpost", cod);
            strdata = await b.decrypt(crypdata.Result);
            Dictionary<string, municipio> lstmun = JsonConvert.DeserializeObject<Dictionary<string, municipio>>(strdata.Result);
            Municipios = lstmun.Select(x => x.Value.txt_desc).ToList();

			crypdata = await b.getCatalog("GetCiudadesMexByEstadoAndCodpost", cod);
            strdata = await b.decrypt(crypdata.Result);
            Dictionary<string, ciudad> lstciud = JsonConvert.DeserializeObject<Dictionary<string, ciudad>>(strdata.Result);
            Ciudades = lstciud.Select(x => x.Value.txt_desc).ToList();

			Ocupado = false;
        }

		int estado;
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

		string municipio;
		public string Municipio
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

		string ciudad;
		public string Ciudad
		{
			get { return ciudad; }
			set
			{
				if (ciudad != value)
				{
					ciudad = value;
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

		string colonia;
		public string Colonia
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
