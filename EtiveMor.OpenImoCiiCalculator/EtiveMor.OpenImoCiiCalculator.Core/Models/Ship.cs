using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Models
{
    public class Ship
    {
        /// <summary>
        /// The type of the ship (e.g., BulkCarrier, RoRoCargoShip, CruisePassengerShip, etc.)        /// </summary>
        public ShipType ShipType { get; set; }

        /// <summary>
        /// The deadweight tonnage (DWT) of the ship, which represents the sum of the weights 
        /// of cargo, fuel, fresh water, ballast water, provisions, passengers, and crew.
        /// </summary>
        public double DeadweightTonnage { get; set; }

        /// <summary>
        /// The gross tonnage (GT) of the ship, which is a measure of the ship's overall internal 
        /// volume. GT is not a weight measurement but is used to determine various other 
        /// shipping-related values, such as crew size, safety requirements, and registration 
        /// fees.
        /// </summary>
        public double GrossTonnage { get; set; }
    }
}
