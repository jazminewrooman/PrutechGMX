using System;

namespace GMX.Services.DTOs
{
    public class resultadobanco
    {
        public string Error { get; set; }
        public string Operacion { get; set; }
        public string Autorizacion { get; set; }
        public string ReferenciaPago { get; set; }
    }

	public class resultadopoliza
	{
		public string Movimiento { get; set; }
		public string Mensajes { get; set; }
		public string PolizaGenerada { get; set; }
		public string Sucursal { get; set; }
		public string Ramo { get; set; }
		public string NumPoliza { get; set; }
		public string Endoso { get; set; }
		public string Renovacion { get; set; }
		public string Referencia { get; set; }
		public string Serie { get; set; }
		public string Comprobante { get; set; }
		public string CodigoBanco { get; set; }
	}

	public class resultadopolizaerror
	{
		public string Movimiento { get; set; }
		public string Mensajes { get; set; }
		public string Error { get; set; }
	}

}
