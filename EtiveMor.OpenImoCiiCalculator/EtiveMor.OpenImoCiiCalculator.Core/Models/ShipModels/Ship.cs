using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels
{

    public class BulkCarrier : Ship
    {
        public BulkCarrier(
            double deadweightTonnage,
            double grossTonnage
            ) : base(deadweightTonnage, grossTonnage)
        {
            ShipType = ShipType.BulkCarrier;
            BoundaryDdVectors2019 = new Dictionary<ImoCiiBoundary, double>
            {
                { ImoCiiBoundary.Superior, 0.86 },
                { ImoCiiBoundary.Lower, 0.94 },
                { ImoCiiBoundary.Upper, 1.06 },
                { ImoCiiBoundary.Inferior, 1.18 }
            };
            a = 4745;
            c = 0.622;

            Capacity = GetShipCapacity(ShipType, deadweightTonnage, grossTonnage);
        }
    }


    public class Ship
    {
        public Ship(
                double deadweightTonnage,
                double grossTonnage
            )
        {
            if (ShipType == ShipType.UNKNOWN)
            {
                throw new System.InvalidOperationException("ShipType must be set. Do not create a Ship directly, use an inheriting ship type");
            }
            DeadweightTonnage = deadweightTonnage;
            GrossTonnage = grossTonnage;


            Capacity = 0;



            if (BoundaryDdVectors2019 == null)
            {
                throw new Exception("BoundaryDdVectors2019 must be set. Do not create a Ship directly, use an inheriting ship type");
            }
        }



        /// <summary>
        /// The type of the ship (e.g., BulkCarrier, RoRoCargoShip, CruisePassengerShip, etc.)       
        /// </summary>
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
        public Dictionary<ImoCiiBoundary, double> BoundaryDdVectors2019 { get; set; }


        public double a { get; protected set; }

        public double c { get; protected set; }

        public double Capacity { get; protected set; }
        public CapacityUnit CapacityUnit { get; protected set; }



        /// <summary>
        /// Calculates the ship's capacity according to the MEPC.353(78)guidelines
        /// </summary>
        /// <param name="shipType">
        /// The type of the ship (e.g., BulkCarrier, RoRoCargoShip, CruisePassengerShip, etc.)
        /// </param>
        /// <param name="deadweightTonnage">
        /// The deadweight tonnage (DWT) of the ship, which represents the sum of the weights 
        /// of cargo, fuel, fresh water, ballast water, provisions, passengers, and crew.
        /// </param>
        /// <param name="grossTonnage">
        /// The gross tonnage (GT) of the ship, which is a measure of the ship's overall internal 
        /// volume. GT is not a weight measurement but is used to determine various other 
        /// shipping-related values, such as crew size, safety requirements, and registration 
        /// fees.
        /// </param>
        /// <returns>
        /// The ship's type capacity according to MEPC.353(78)
        /// </returns>
        public double GetShipCapacity(ShipType shipType, double deadweightTonnage, double grossTonnage)
        {
            ValidateTonnageParamsSet(shipType, deadweightTonnage, grossTonnage);


            switch (shipType)
            {
                case ShipType.BulkCarrier:
                    return deadweightTonnage >= 279000 ? 279000 : deadweightTonnage;
                case ShipType.GasCarrier:
                    return deadweightTonnage;
                case ShipType.Tanker:
                    return deadweightTonnage;
                case ShipType.ContainerShip:
                    return deadweightTonnage;
                case ShipType.GeneralCargoShip:
                    return deadweightTonnage;
                case ShipType.RefrigeratedCargoCarrier:
                    return deadweightTonnage;
                case ShipType.CombinationCarrier:
                    return deadweightTonnage;
                case ShipType.LngCarrier:
                    return deadweightTonnage < 65000 ? 65000 : deadweightTonnage;
                case ShipType.RoRoCargoShipVehicleCarrier:
                    return deadweightTonnage >= 57700 ? 57700 : grossTonnage;
                case ShipType.RoRoCargoShip:
                    return grossTonnage;
                case ShipType.RoRoPassengerShip:
                    return grossTonnage;
                case ShipType.RoRoPassengerShip_HighSpeedSOLAS:
                    return grossTonnage;
                case ShipType.RoRoCruisePassengerShip:
                    return grossTonnage;
                default:
                    throw new ArgumentException($"Unsupported {nameof(shipType)}: {shipType}");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="deadweightTonnage">
        /// The ship's deadweight tonnage
        /// Required to be above 0 for ship types:
        ///     - <see cref="ShipType.BulkCarrier"/>
        ///     - <see cref="ShipType.GasCarrier"/>
        ///     - <see cref="ShipType.Tanker"/>
        ///     - <see cref="ShipType.ContainerShip"/>
        ///     - <see cref="ShipType.GeneralCargoShip"/>
        ///     - <see cref="ShipType.RefrigeratedCargoCarrier"/>
        ///     - <see cref="ShipType.CombinationCarrier"/>
        ///     - <see cref="ShipType.LngCarrier"/>
        ///     - <see cref="ShipType.RoRoCargoShip"/>
        /// </param>
        /// <param name="grossTonnage">
        /// The ship's grossTonnage. 
        /// 
        /// Required to be above 0 for ship types: 
        ///     - <see cref="ShipType.RoRoCargoShipVehicleCarrier"/>
        ///     - <see cref="ShipType.RoRoPassengerShip"/>
        ///     - <see cref="ShipType.RoRoCruisePassengerShip"/>
        /// </param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the weight value is equal or lower than 0 if it is required to be above 0</exception>
        private void ValidateTonnageParamsSet(ShipType shipType, double deadweightTonnage, double grossTonnage)
        {
            _ = shipType switch
            {
                ShipType.BulkCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType)
                    ? deadweightTonnage
                    : throw new InvalidOperationException(),

                ShipType.GasCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType)
                    ? deadweightTonnage
                    : throw new InvalidOperationException(),

                ShipType.Tanker => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType)
                    ? deadweightTonnage
                    : throw new InvalidOperationException(),

                ShipType.ContainerShip => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType)
                    ? deadweightTonnage
                    : throw new InvalidOperationException(),

                ShipType.GeneralCargoShip => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType)
                    ? deadweightTonnage
                    : throw new InvalidOperationException(),

                ShipType.RefrigeratedCargoCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType)
                    ? deadweightTonnage
                    : throw new InvalidOperationException(),

                ShipType.CombinationCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType)
                    ? deadweightTonnage
                    : throw new InvalidOperationException(),

                ShipType.LngCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType)
                    ? deadweightTonnage
                    : throw new InvalidOperationException(),

                ShipType.RoRoCargoShipVehicleCarrier => ValidateTonnage(grossTonnage, nameof(grossTonnage), shipType)
                    ? grossTonnage
                    : throw new InvalidOperationException(),

                ShipType.RoRoCargoShip => ValidateTonnage(grossTonnage, nameof(grossTonnage), shipType)
                    ? grossTonnage
                    : throw new InvalidOperationException(),

                ShipType.RoRoPassengerShip => ValidateTonnage(grossTonnage, nameof(grossTonnage), shipType)
                    ? grossTonnage
                    : throw new InvalidOperationException(),

                ShipType.RoRoPassengerShip_HighSpeedSOLAS => ValidateTonnage(grossTonnage, nameof(grossTonnage), shipType)
                    ? grossTonnage
                    : throw new InvalidOperationException(),

                ShipType.RoRoCruisePassengerShip => ValidateTonnage(grossTonnage, nameof(grossTonnage), shipType)
                    ? grossTonnage
                    : throw new InvalidOperationException(),

                _ => throw new ArgumentOutOfRangeException(nameof(shipType), shipType, $"Unsupported {nameof(shipType)}: {shipType}")



            };
        }

        /// <summary>
        /// Validates that the tonnage param is greater than 0 
        /// 
        /// </summary>
        /// <param name="tonnage">The tonnage value (accepts either gross tonnage or deadweight)</param>
        /// <param name="tonnageName">The ship's tonnage name (accepts either "gross" or "deadweight"</param>
        /// <param name="shipType">The ship type <seealso cref="ShipType"/></param>
        /// <returns>
        /// true if the tonnage is greater than 0
        /// throws an exception if the tonnage is less than or equal to 0
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is equal or lower than 0</exception>
        private bool ValidateTonnage(double tonnage, string tonnageName, ShipType shipType)
        {
            if (tonnage <= 0)
            {
                throw new ArgumentOutOfRangeException(tonnageName, tonnage, $"{tonnageName} must be greater than 0 if {nameof(shipType)} is set to {shipType} ");
            }
            return true;
        }
    }


    public enum CapacityUnit
    {
        ERR,
        DWT,
        DWT_CAP_HIGH,
        GT,
        GT_CAP_LOW
    }


    
}
