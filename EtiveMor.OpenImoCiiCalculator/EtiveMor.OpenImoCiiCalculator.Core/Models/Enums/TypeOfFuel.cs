namespace EtiveMor.OpenImoCiiCalculator.Core.Models.Enums
{
    /// <summary>
    /// An enum describing the possible types of fuel used by ships 
    /// considered by the IMO's Carbon Intensity Indicator (CII) rating system
    /// </summary>
    public enum TypeOfFuel
    {
        UNKNOWN = 0,
        DIESEL_OR_GASOIL = 10,
        LIGHTFUELOIL = 20,
        HEAVYFUELOIL = 30,
        LIQUIFIEDPETROLEUM_PROPANE = 40,
        LIQUIFIEDPETROLEUM_BUTANE = 50,
        ETHANE = 60,
        LIQUIFIEDNATURALGAS = 70,
        METHANOL = 80,
        ETHANOL = 90,
        OTHER = 100
    }
}
