using System;
namespace GMX
{
	public class Enums
	{
	}

	public enum TipoDatos
	{
		Generales = 0,
		Fiscales = 1
	};

	public enum TipoPersona
	{
		Fisica = 0,
		Moral = 1
	};


    public enum TipoPago{
        tarjeta = 1,
        banco = 2
    }

    public enum TipoResumen
    {
        Generales = 1,
        Fiscales = 2,
        Profesionales = 3,
        Bancarios = 4
    }
}
