using System;
using System.Collections.Generic;
using System.Text;

namespace GMX
{

	public class DatosGralesModel
	{
		public string RFC { get; set; }
		public string Nombre { get; set; }
		public string APaterno { get; set; }
		public string AMaterno { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
		public string CP { get; set; }
		public int Estado { get; set; }
		public int Municipio { get; set; }
		public int Ciudad { get; set; }
		public int Colonia { get; set; }
		public string Correo { get; set; }
        public TipoPersona Persona { get; set; }
	}

}
