using System;
using CreditCardValidator;

namespace GMX.Models
{
    public class DatosBancarios
    {
        public string Nombre { get; set; }
        public CardIssuer TipoTarj { get; set; }
        public string FormaPago { get; set; }
        public string NumTarjeta { get; set; }
        public string Mes { get; set; }
        public string Anio { get; set; }
        public string CodigoSeg { get; set; }
    }
}
