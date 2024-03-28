using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels
{
    public class Ship
    {
        public Ship(
                double deadweightTonnage,
                double grossTonnage, 
                double a,
                double c,
                double capacity
            )
        {
            if (ShipType == ShipType.UNKNOWN)
            {
                throw new System.InvalidOperationException("ShipType must be set. Do not create a Ship directly, use an inheriting ship type");
            }
            DeadweightTonnage = deadweightTonnage;
            GrossTonnage = grossTonnage;

        }

        /// <summary>
        /// The type of the ship (e.g., BulkCarrier, RoRoCargoShip, CruisePassengerShip, etc.)        /// </summary>
        public ShipType ShipType { get; protected set; }

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



        /// <summary>
        /// The ship's dd vectors as outlined in MEPC.354(78)
        /// </summary>
        public required Dictionary<ImoCiiBoundary, double> BoundaryDdVectors2019 { get; set; }


        public double a { get; private set; }

        public double c { get; private set; }

        public double Capacity { get; protected set; }
        public CapacityUnit CapacityUnit { get; protected set; }
    }


    public enum CapacityUnit
    {
        ERR,
        DWT,
        DWT_CAP_HIGH,
        GT,
        GT_CAP_LOW
    }


    public class  BulkCarrier : Ship
    {
        public BulkCarrier(
            double deadweightTonnage,
            double grossTonnage
            ) : base(deadweightTonnage, grossTonnage)
        {
            ShipType = ShipType.BulkCarrier;
            BoundaryDdVectors2019 = new Dictionary<ImoCiiBoundary, double>
            {
                { ImoCiiBoundary.Superior, 0.76 },
                { ImoCiiBoundary.Lower, 0.92 },
                { ImoCiiBoundary.Upper, 1.14 },
                { ImoCiiBoundary.Inferior, 1.30 }
            };

        }
    }
}
