using System;
using System.Collections.Generic;

namespace GMX.Services.DTOs
{
	public class datospolizaemitida
	{
		public IList<polizaemitida> Table { get; set; }
	}

	public class polizaemitida
    {
        public int Id { get; set; }
        public int Id_Agente { get; set; }
        public string txt_Agente { get; set; }
        public string Rfc_Cliente { get; set; }
        public string Nombre_Cliente { get; set; }
        public string Calle_Cliente { get; set; }
        public string No_Ext_Cliente { get; set; }
        public string No_Int_Cliente { get; set; }
        public string Telefono_Cliente { get; set; }
        public string Cp_Cliente { get; set; }
        public string Colonia_Cliente { get; set; }
        public string Estado_Cliente { get; set; }
        public string Municipio_Cliente { get; set; }
        public string Email_Cliente { get; set; }
        public string Poliza { get; set; }
        public string Referencia { get; set; }
        public string Estatus_Transaccion { get; set; }
        public string Operacion { get; set; }
        public string Autorizacion { get; set; }
        public string Tarjeta { get; set; }
        public string Respuesta_Xml { get; set; }
        public bool Genero_Recibo { get; set; }
        public string Especialidad { get; set; }
        public string SubEspecialidad { get; set; }
        public string NoCedulaPro { get; set; }
        public string NoCedulaEsp { get; set; }
        public string Otros { get; set; }
        public double Suma_Asegurada { get; set; }
        public bool Arrendatario { get; set; }
        public DateTime Vigencia_Ini { get; set; }
        public DateTime Vigencia_Fin { get; set; }
        public DateTime Emision { get; set; }
        public double PrimaNeta { get; set; }
        public double Derechos { get; set; }
        public double Iva { get; set; }
        public double PrimaTotal { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Tipo_Negocio { get; set; }
        public string No_Empleado { get; set; }
        public string Centro_Work { get; set; }
        public string Puesto { get; set; }
        public string RazonSocial { get; set; }
        public string RfcFis { get; set; }
        public string DomicilioFis { get; set; }
        public string oficina { get; set; }
        public string Especialidad2 { get; set; }
        public string NoCedulaEsp2 { get; set; }
        public string PolizasAnt { get; set; }
        public string PolAnt1 { get; set; }
        public string PolAnt2 { get; set; }
        public string NoSocioEspanol { get; set; }
        public string NoCedulaEspecialidad2_ABC { get; set; }
        public string Especialidad2_ABC { get; set; }
        public object PolizaAntTecSalud { get; set; }
        public object InicioVigenciaTecSalud { get; set; }
        public string NoCredencializacionTecSalud { get; set; }
        public DateTime fecRetroactiva { get; set; }
        public string Clave { get; set; }
        public string MatriculaImss { get; set; }
        public string CentroTrabajoImss { get; set; }
        public string TipoContratoImss { get; set; }
    }
}
