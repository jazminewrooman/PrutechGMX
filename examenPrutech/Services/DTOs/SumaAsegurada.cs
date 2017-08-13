using System;
using System.Collections.Generic;

namespace GMX.Services.DTOs
{
    public class SumaAsegXPlan
    {
        public double SumaAsegurada { get; set; }
		public double A { get; set; }
		public double B { get; set; }
		public double D { get; set; }
		public double E { get; set; }
		public DateTime addDate { get; set; }
		public DateTime modDate { get; set; }
		public int idSumAse_Categoria { get; set; }
        public double derechos { get; set; }
	}

	public class ListaSumasAseg
	{
		public IList<SumaAsegXPlan> Table { get; set; }
	}

	public class SumaAsegAngeles
	{
		public double SumaAsegurada { get; set; }
		public double PrimaUnica { get; set; }
		public DateTime addDate { get; set; }
		public DateTime modDate { get; set; }
		public int idSumAseg_Angeles { get; set; }
		public double PrimaTotal { get; set; }
		public double derechos { get; set; }
	}

	public class ListaSumaAngeles
	{
		public IList<SumaAsegAngeles> Table { get; set; }
	}
}
