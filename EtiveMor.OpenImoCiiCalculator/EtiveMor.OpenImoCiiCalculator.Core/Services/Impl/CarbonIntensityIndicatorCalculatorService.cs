using EtiveMor.OpenImoCiiCalculator.Core.Extensions;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services.Impl
{
    public class CarbonIntensityIndicatorCalculatorService : ICarbonIntensityIndicatorCalculatorService
    {
        /// <summary>
        /// Gets a ship's attained carbon intensity, which is the ratio of the cumulative mass
        /// of CO2 emissions in a calendar year to the ship's transport work in a calendar year
        /// </summary>
        /// <param name="massOfCo2Emissions">
        /// The cumulative mass of CO2 emissions in a calendar year in grams (g)
        /// <seealso cref="ShipMassOfCo2EmissionsCalculatorService.GetMassOfCo2Emissions(Models.Enums.TypeOfFuel, double)"/>
        /// </param>
        /// <param name="transportWork">
        /// The ship's transport work in a calendar year
        /// <seealso cref="ShipTransportWorkCalculatorService.GetShipTransportWork(double, double)"/>"/>
        /// </param>
        /// <returns>
        /// A ship's attained Carbon Intensity (CII)
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if massOfCo2Emissions or transportWork is less than or equal to zero
        /// </exception>
        public double GetAttainedCarbonIntensity(double massOfCo2Emissions, double transportWork)
        {
            if (massOfCo2Emissions <= 0)
            {
                throw new ArgumentOutOfRangeException("Mass of CO2 emissions must be a positive value",
                                       nameof(massOfCo2Emissions));
            }
            if (transportWork <= 0)
            {
                throw new ArgumentOutOfRangeException("Transport work must be a positive value",
                                                          nameof(transportWork));
            }

            return massOfCo2Emissions / transportWork;
        }


        public Dictionary<int, double> GetRequiredCarbonIntensity(ShipType shipType, double capacity)
        {
            var dict = new Dictionary<int, double>();

            for (int year = 2019; year <= 2030; year++)
            {
                // Your code here
                dict.Add(year, GetRequiredCarbonIntensity(shipType, capacity, year));
            }

            return dict;
        }


        /// <summary>
        /// Gets a ship's required CII in accordance with MEPC.323(74)
        /// </summary>
        /// <param name="shipType">
        /// The type of ship <seealso cref="ShipType"/>
        /// </param>
        /// <param name="capacity">
        /// The ship's capacity according to MEPC 337(76).
        /// 
        /// Note that the capacity must have been pre-calculated, 
        /// using <seealso cref="ShipCapacityCalculatorService.GetShipCapacity(ShipType, double, double)"/>
        /// </param>
        /// <returns>
        /// The required CII for a ship of the given type and capacity
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if capacity is equal or lower than 0</exception>
        public double GetRequiredCarbonIntensity(ShipType shipType, double capacity, int year)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be a positive value", nameof(capacity));
            }

            double a = GetValue(ValType.a, shipType, capacity);
            double c = GetValue(ValType.c, shipType, capacity);

            double ciiReference = a * Math.Pow(capacity, -c);

            return ciiReference.ApplyAnnualReductionFactor(year);
        }


        /// <summary>
        /// Gets either the `a` or `c` value for a given ship type and capacity
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="shipType">the type of ship being queried</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// the `a` or `c` value for the given ship type and capacity
        /// </returns>
        /// <exception cref="ArgumentException">Thrown if <see cref="ValType"/> is not a or c</exception>
        /// <exception cref="NotSupportedException">
        /// Thrown if the ship type is not supported
        /// </exception>
        private double GetValue(ValType valType, ShipType shipType, double capacity)
        {
            if (valType != ValType.a && valType != ValType.c)
            {
                throw new ArgumentException($"Invalid value type '{valType}'", nameof(valType));
            }

            
            return shipType switch
            {
                ShipType.BulkCarrier => GetBulkCarrierValue(valType, capacity),
                ShipType.GasCarrier => GetGasCarrierValue(valType, capacity),
                ShipType.Tanker => GetTankerValue(valType, capacity), 
                ShipType.ContainerShip => GetContainerShipValue(valType, capacity),
                ShipType.GeneralCargoShip => GetGeneralCargoShipValue(valType, capacity),
                ShipType.RefrigeratedCargoCarrier => GetRefrigeratedCargoCarrierValue(valType, capacity), 
                ShipType.CombinationCarrier => GetCombinationCarrierValue(valType, capacity), 
                ShipType.LngCarrier => GetLngCarrierShipValue(valType, capacity),
                ShipType.RoRoCargoShipVehicleCarrier => GetRoRoCargoShipVehicleCarrierValue(valType, capacity), 
                ShipType.RoRoCargoShip => GetRoRoCargoShipValue(valType, capacity), 
                ShipType.RoRoPassengerShip => GetRoRoPassengerShipValue(valType, capacity),
                ShipType.RoRoPassengerShip_HighSpeedSOLAS => GetRoRoPassengerShip_HighSpeedSOLASValue(valType, capacity), 
                ShipType.CruisePassengerShip => GetRoRoCruisePassengerShipValue(valType, capacity),
                ShipType.UNKNOWN => throw new NotSupportedException($"Unsupported {nameof(shipType)} '{shipType}'"),
                _ => throw new NotSupportedException($"Unsupported {nameof(shipType)} '{shipType}'")
            };
        }


        /// <summary>
        /// Gets the appropriate `a` or `c` value for a LNG Carrier, according to 
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a LNG Carrier
        /// </returns>
        private double GetLngCarrierShipValue(ValType valType, double capacity)
        {
            if (capacity >= 100000)
            {
                return valType == ValType.a ? 9.827 : 0.000;
            }
            if (capacity >= 65000)
            {
                return valType == ValType.a ? 14479E10 : 2.673;
            }
            return valType == ValType.a ? 14779E10 : 2.673;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a General Cargo ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a General Cargo ship
        /// </returns>
        private double GetGeneralCargoShipValue(ValType valType, double capacity)
        {
            if (capacity >= 20000)
            {
                return valType == ValType.a ? 31948 : 0.792;
            }
            return valType == ValType.a ? 588 : 0.3885;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a Bulk Carrier ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a Bulk Carrier ship
        /// </returns>
        private double GetBulkCarrierValue(ValType valType, double capacity)
        {
            if (capacity >= 279000)
            {
                return valType == ValType.a ? 4745 : 0.622;
            }
            return valType == ValType.a ? 4745 : 0.622;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a Gas Carrier ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a Gas Carrier ship
        /// </returns>
        private double GetGasCarrierValue(ValType valType, double capacity)
        {
            if (capacity >= 65000)
            {
                return valType == ValType.a ? 14405E7 : 2.071;
            }
            return valType == ValType.a ? 8104 : 0.639;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a Tanker ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a Tanker ship
        /// </returns>
        private double GetTankerValue(ValType valType, double capacity)
        {
            return valType == ValType.a ? 5247 : 0.610;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a ContainerShip ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a ContainerShip ship
        /// </returns>
        private double GetContainerShipValue(ValType valType, double capacity)
        {
            return valType == ValType.a ? 1984 : 0.489;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a RefrigeratedCargoCarrier ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a RefrigeratedCargoCarrier ship
        /// </returns>
        private double GetRefrigeratedCargoCarrierValue(ValType valType, double capacity)
        {
            return valType == ValType.a ? 4600 : 0.557;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a CombinationCarrier ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a CombinationCarrier ship
        /// </returns>
        private double GetCombinationCarrierValue(ValType valType, double capacity)
        {
            return valType == ValType.a ? 5119 : 622;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a RoRoCargoShipVehicleCarrier ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a RoRoCargoShipVehicleCarrier ship
        /// </returns>
        private double GetRoRoCargoShipVehicleCarrierValue(ValType valType, double capacity)
        {
            if (capacity >= 57700)
            {
                return valType == ValType.a ? 3627 : 0.590;
            }
            if (capacity >= 30000)
            {
                return valType == ValType.a ? 5739 : 0.590;
            }
            return valType == ValType.a ? 330 : 329;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a RoRoCargoShip ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a RoRoCargoShip ship
        /// </returns>
        private double GetRoRoCargoShipValue(ValType valType, double capacity)
        {
            return valType == ValType.a ? 1967 : 0.485;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a RoRoPassengerShip ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a RoRoPassengerShip ship
        /// </returns>
        private double GetRoRoPassengerShipValue(ValType valType, double capacity)
        {
            return valType == ValType.a ? 2023 : 0.460;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a RoRoPassengerShip ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a RoRoPassengerShip ship
        /// </returns>
        private double GetRoRoPassengerShip_HighSpeedSOLASValue(ValType valType, double capacity)
        {
            return valType == ValType.a ? 4196 : 0.460;
        }

        /// <summary>
        /// Gets the appropriate `a` or `c` value for a RoRoCruisePassengerShip ship, according to
        /// Table 1: MEPC.353(78)
        /// </summary>
        /// <param name="valType">The <see cref="ValType"/> to return, must be either a or c</param>
        /// <param name="capacity">the capacity of the ship being queried</param>
        /// <returns>
        /// The `a` or `c` value for a RoRoCruisePassengerShip ship
        /// </returns>
        private double GetRoRoCruisePassengerShipValue(ValType valType, double capacity)
        {
            return valType == ValType.a ? 930 : 0.383;
        }

        private enum ValType
        {
            a, 
            c,
            ERR
        }
    }
}
