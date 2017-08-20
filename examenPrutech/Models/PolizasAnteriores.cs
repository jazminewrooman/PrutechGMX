using System;
using System.Collections.Generic;

namespace GMX.Models
{
    public class PolizasAnteriores
    {
        public PolizasAnteriores()
        {
        }

        public NumPoliza poliza1 { get; set; }
        public NumPoliza poliza2 { get; set; }
        public NumPoliza poliza3 { get; set; }
        public int dia { get; set; }
        public int mes { get; set; }
        public int anno { get; set; }
    }

    public class NumPoliza
    {
        public string oficina { get; set; }
        public string producto { get; set; }
        public string poliza { get; set; }
        public string endoso { get; set; }
        public string renovacion { get; set; }
    }
}
