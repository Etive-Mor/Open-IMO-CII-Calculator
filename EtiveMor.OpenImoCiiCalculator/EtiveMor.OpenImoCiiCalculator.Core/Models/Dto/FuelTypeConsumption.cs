using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Models.Dto
{
    /// <summary>
    /// A request object to hold metadata about the fuel consumption of a ship
    /// </summary>
    public class FuelTypeConsumption
    {
        /// <summary>
        /// The type of fuel consumed by the ship
        /// </summary>
        public TypeOfFuel FuelType { get; set; }

        /// <summary>
        /// The amount of fuel consumed by the ship in the given year (in grams)
        /// </summary>
        public double FuelConsumption { get; set; }
    }

}
