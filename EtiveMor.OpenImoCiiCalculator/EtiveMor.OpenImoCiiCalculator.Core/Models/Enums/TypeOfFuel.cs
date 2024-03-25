namespace EtiveMor.OpenImoCiiCalculator.Core.Models.Enums
{
    /// <summary>
    /// An enum describing the possible types of fuel used by ships 
    /// considered by the IMO's Carbon Intensity Indicator (CII) rating system
    /// </summary>
    public enum TypeOfFuel
    {
        UNKNOWN = 0,
        DIESEL_OR_GASOIL = 1,
        LIGHTFUELOIL = 2,
        HEAVYFUELOIL = 3,
        LIQUIFIEDPETROLEUMGAS = 4,
        LIQUIFIEDNATURALGAS = 5,
        OTHER = 6
    }
}
