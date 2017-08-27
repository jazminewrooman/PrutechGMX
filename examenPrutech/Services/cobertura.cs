using System;
using SQLite;

namespace GMX.Services
{

    public class cobertura
    {
        public int IdPlan { get; set; }
        public int RCArrendatario { get; set; }
        public decimal snAcumSumTot { get; set; }
        public decimal snAcumPrimaTot { get; set; }
        public int ramoTecnico { get; set; }
        public int codItem { get; set; }
        public int codIndCob { get; set; }
        public int codTarifa { get; set; }
        public decimal pjeTasa { get; set; }
        public int codTipoTasa { get; set; }
        public int codObjeto { get; set; }
        public decimal impSumaAsegurable { get; set; }
        public decimal impRespMax { get; set; }
        public decimal impRespMaxAcc { get; set; }
        public decimal impParteDe { get; set; }
        public decimal impAgregAnual { get; set; }
        public int codDeduc { get; set; }
        public decimal impDeduc { get; set; }
        public int codUnidadDeduc { get; set; }
        public int codDeducAplica { get; set; }
        public string txtDetalle { get; set; }
        public decimal impPrima { get; set; }
        public decimal impPrimaTasa { get; set; }
        public decimal impPrimaRecDesc { get; set; }
        public decimal impPrimaAjuste { get; set; }
        public decimal pjeInflacion { get; set; }
        public decimal pjeInflacionPrima { get; set; }
        public decimal impSumaInflacion { get; set; }
        public decimal impPrimaInflacion { get; set; }
        public decimal impSumaAsegurada { get; set; }
    }
}
