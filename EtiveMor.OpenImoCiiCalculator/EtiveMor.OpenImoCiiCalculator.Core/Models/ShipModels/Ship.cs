using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels
{


    public class Ship
    {
        public Ship(
                ShipType shipType,
                double deadweightTonnage,
                double grossTonnage
            )
        {
            ShipType = shipType;
            DeadweightTonnage = deadweightTonnage;
            GrossTonnage = grossTonnage;
        }



        /// <summary>
        /// The type of the ship (e.g., BulkCarrier, RoRoCargoShip, CruisePassengerShip, etc.)       
        /// </summary>
        public ShipType ShipType { get; private set; }

        /// <summary>
        /// The deadweight tonnage (DWT) of the ship, which represents the sum of the weights 
        /// of cargo, fuel, fresh water, ballast water, provisions, passengers, and crew.
        /// </summary>
        public double DeadweightTonnage { get; private set; }

        /// <summary>
        /// The gross tonnage (GT) of the ship, which is a measure of the ship's overall internal 
        /// volume. GT is not a weight measurement but is used to determine various other 
        /// shipping-related values, such as crew size, safety requirements, and registration 
        /// fees.
        /// </summary>
        public double GrossTonnage { get; private set; }



        /// <summary>
        /// The ship's dd vectors as outlined in MEPC.354(78)
        ///// </summary>
        //public Dictionary<ImoCiiBoundary, double> BoundaryDdVectors2019 { get; set; }


        //public double a { get; protected set; }

        //public double c { get; protected set; }

        //public double Capacity { get; protected set; }
        //public CapacityUnit CapacityUnit { get; protected set; }
    }





    
}
