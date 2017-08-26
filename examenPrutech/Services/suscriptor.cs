using System;
using System.Collections.Generic;

namespace GMX.Services.DTOs
{
    public class susc
    {
        public int Pv { get; set; }
        public string clave { get; set; }
        public string email { get; set; }
    }

    public class suscriptor
    {
        public IList<susc> Table { get; set; }
    }
}
