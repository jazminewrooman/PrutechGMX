using System;
using System.ComponentModel;
using Acr.UserDialogs;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;
using System.Linq;
using GMX.Views;
using GMX.Services;
using System.Text.RegularExpressions;

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
                        bindings b = new bindings();
                        b.IniciaWS();
                        b.Service.getCatalogCompleted += (sender, e) =>
                        {

                        };

                        b.Service.getCatalogAsync("{ 'producto':'PVLM3D', 'clave':'AdKGEpIxQTR1XvtEpQLDYA==', 'llave':'PJUZpGkDfHaEca6I1kdw0iuSYJP8cJIw3F6L4a94uQo=', 'comando': 'GetEstadosMexByCodpost', 'parametros':'08100' }");
                    }
					OnPropertyChanged("CP");
				}
			}
		}

		string estado;
		public string Estado
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
