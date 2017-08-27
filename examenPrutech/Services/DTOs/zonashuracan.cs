using System;
namespace GMX.Services.DTOs
{
    public class zonashuracan
    {
        public string cod_postal_ini { get; set; }
        public string cod_postal_fin { get; set; }
        public string cod_pais { get; set; }
        public string cod_dpto { get; set; }
        public int zona_cresta { get; set; }
        public string txt_zona_cresta { get; set; }
        public string desc_zona_cresta { get; set; }
        public int zona_amis { get; set; }
        public string txt_zona_amis { get; set; }
        public string desc_zona_amis { get; set; }
        public string zona_hidro { get; set; }
        public string fec_desde { get; set; }
        public string pje_coaseguro { get; set; }
        public string ded_edificio { get; set; }
        public string ded_contenidos { get; set; }
        public string ded_per_consec { get; set; }
        public string ded_bienes { get; set; }
        public string coas_edificio { get; set; }
        public string coas_contenidos { get; set; }
        public string coas_per_consec { get; set; }
        public string coas_bienes { get; set; }
    }

    public class zonasterremoto
    {
        public string cod_postal_ini { get; set; }
        public string cod_postal_fin { get; set; }
        public string cod_pais { get; set; }
        public string cod_dpto { get; set; }
        public string Terremoto_numerica { get; set; }
        public string Clave_Asentamiento { get; set; }
        public string Asentamiento { get; set; }
        public int zona_cresta { get; set; }
        public string txt_zona_cresta { get; set; }
        public string desc_zona_cresta { get; set; }
        public int zona_amis { get; set; }
        public string txt_zona_amis { get; set; }
        public string desc_zona_amis { get; set; }
        public string nro_pisos_desde { get; set; }
        public string nro_pisos_hasta { get; set; }
        public string fec_desde { get; set; }
        public string pje_coaseguro { get; set; }
        public string ded_edificio { get; set; }
        public string ded_contenidos { get; set; }
        public string ded_per_consec { get; set; }
        public string ded_bienes { get; set; }
        public string coas_edificio { get; set; }
        public string coas_contenidos { get; set; }
        public string coas_per_consec { get; set; }
        public string coas_bienes { get; set; }
    }
}
