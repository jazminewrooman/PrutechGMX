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

}
