using System;
using System.Collections.Generic;

namespace GMX.Services.DTOs
{
    public class SumaAsegXPlan
    {
        public SumaAsegXPlan()
        {
        }

        public double SumaAsegurada { get; set; }
		public double A { get; set; }
		public double B { get; set; }
		public double D { get; set; }
		public double E { get; set; }
		public DateTime addDate { get; set; }
		public DateTime modDate { get; set; }
		public int idSumAse_Categoria { get; set; }
	}

	public class ListaSumasAseg
	{
		public IList<SumaAsegXPlan> Table { get; set; }
	}
}
