using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GMX.Services.DTOs
{
    public class estado
    {
        public string cod_dpto { get; set; }
        public string txt_desc { get; set; }
    }

    public class municipio
    {
        public string cod_municipio { get; set; }
        public string txt_desc { get; set; }
    }

    public class ciudad
    {
        public string cod_ciudad { get; set; }
        public string txt_desc { get; set; }
    }

    public class colonia
    {
        public string cod_pais { get; set; }
        public string cod_dpto { get; set; }
        public string cod_municipio { get; set; }
        public string cod_ciudad { get; set; }
        public string cod_colonia { get; set; }
        public string txt_desc { get; set; }
        public string cod_postal { get; set; }
        public string valida { get; set; }
    }
}
