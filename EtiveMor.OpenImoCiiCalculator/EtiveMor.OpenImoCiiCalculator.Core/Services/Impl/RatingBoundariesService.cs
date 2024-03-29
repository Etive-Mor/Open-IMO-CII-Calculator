using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Models.MeasurementModels;
using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services.Impl
{
    public class RatingBoundariesService : IRatingBoundariesService
    {
        /// <summary>
        /// Returns the ship grading boundaries ouelines in MEPC354(78) for a given 
        /// ship and required CII in a year.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="requiredCiiInYear"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public ShipDdVectorBoundaries GetBoundaries(Ship ship, double requiredCiiInYear, int year)
        {
            ValidateShipTonnageValid(ship);

            switch (ship.ShipType)
            {
                case ShipType.BulkCarrier:
                    {
                        if (ship.DeadweightTonnage <= 0) throw new NotSupportedException($"Deadweight tonnage must be greater than 0 for ship type {ship.ShipType}");

                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.DWT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.86 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.94 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.06 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.18 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.GasCarrier:
                    {
                        if (ship.DeadweightTonnage >= 65000)
                        {
                            return new ShipDdVectorBoundaries(
                               ship.ShipType,
                               new WeightClassification(65000, int.MaxValue),
                               CapacityUnit.DWT,
                               new Dictionary<ImoCiiBoundary, double>
                               {
                                    { ImoCiiBoundary.Superior, 0.81 * requiredCiiInYear },
                                    { ImoCiiBoundary.Lower, 0.91 * requiredCiiInYear },
                                    { ImoCiiBoundary.Upper, 1.12 * requiredCiiInYear },
                                    { ImoCiiBoundary.Inferior, 1.44 * requiredCiiInYear }
                               },
                            year);
                        }
                        else
                        {
                            return new ShipDdVectorBoundaries(
                                ship.ShipType,
                                new WeightClassification(0, 65000 - 1),
                                CapacityUnit.DWT,
                                new Dictionary<ImoCiiBoundary, double>
                                {
                                    { ImoCiiBoundary.Superior, 0.85 * requiredCiiInYear },
                                    { ImoCiiBoundary.Lower, 0.95 * requiredCiiInYear },
                                    { ImoCiiBoundary.Upper, 1.06 * requiredCiiInYear },
                                    { ImoCiiBoundary.Inferior, 1.25 * requiredCiiInYear }
                                },
                            year);
                        }
                    }
                case ShipType.Tanker:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.DWT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.82 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.93 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.08 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.28 * requiredCiiInYear }
                            }, year);
                    }
                case ShipType.ContainerShip:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.DWT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.83 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.94 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.07 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.19 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.GeneralCargoShip:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.DWT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.83 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.94 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.06 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.19 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.RefrigeratedCargoCarrier:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.DWT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.78 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.91 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.07 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.20 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.CombinationCarrier:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.DWT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.87 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.96 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.06 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.14 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.LngCarrier:
                    {
                        if (ship.DeadweightTonnage >= 100000)
                        {
                            return new ShipDdVectorBoundaries(
                                ship.ShipType,
                                new WeightClassification(100000, int.MaxValue),
                                CapacityUnit.DWT,
                                new Dictionary<ImoCiiBoundary, double>
                                {
                                { ImoCiiBoundary.Superior, 0.89 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.98 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.06 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.13 * requiredCiiInYear }
                            },
                            year);
                        }
                        else
                        {
                            return new ShipDdVectorBoundaries(
                                ship.ShipType,
                                new WeightClassification(0, 100000 - 1),
                                CapacityUnit.DWT,
                                new Dictionary<ImoCiiBoundary, double>
                                {
                                { ImoCiiBoundary.Superior, 0.78 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.92 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.10 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.37 * requiredCiiInYear }
                            },
                            year);
                        }
                    }
                case ShipType.RoRoCargoShipVehicleCarrier:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.GT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.86 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.94 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.06 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.16 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.RoRoCargoShip:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.GT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.76 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.89 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.08 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.27 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.RoRoPassengerShip:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.GT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.76 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.92 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.14 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.30 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.RoRoPassengerShip_HighSpeedSOLAS:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.GT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.76 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.92 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.14 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.30 * requiredCiiInYear }
                            },
                            year);
                    }
                case ShipType.CruisePassengerShip:
                    {
                        return new ShipDdVectorBoundaries(
                            ship.ShipType,
                            new WeightClassification(0, int.MaxValue),
                            CapacityUnit.GT,
                            new Dictionary<ImoCiiBoundary, double>
                            {
                                { ImoCiiBoundary.Superior, 0.87 * requiredCiiInYear },
                                { ImoCiiBoundary.Lower, 0.95 * requiredCiiInYear },
                                { ImoCiiBoundary.Upper, 1.06 * requiredCiiInYear },
                                { ImoCiiBoundary.Inferior, 1.16 * requiredCiiInYear }
                            },
                            year);
                    }

                case ShipType.UNKNOWN:
                    throw new NotSupportedException($"Ship type '{ship.ShipType}' not supported");
                default:
                    throw new NotSupportedException($"Ship type '{ship.ShipType}' not supported");
            }


            /// <summary>
            /// Checks that the ship tonnage is valid for the ship type.
            /// </summary>
            static void ValidateShipTonnageValid(Ship ship)
            {
                switch (ship.ShipType)
                {
                    case ShipType.BulkCarrier
                        or ShipType.GasCarrier
                        or ShipType.Tanker
                        or ShipType.ContainerShip
                        or ShipType.GeneralCargoShip
                        or ShipType.RefrigeratedCargoCarrier
                        or ShipType.CombinationCarrier
                        or ShipType.LngCarrier:
                        {
                            if (ship.DeadweightTonnage <= 0)
                            {
                                throw new NotSupportedException($"Deadweight tonnage must be greater than 0 for ship type {ship.ShipType}. Was provided {ship.DeadweightTonnage}");
                            }
                            break;
                        }
                    case ShipType.RoRoCargoShipVehicleCarrier
                        or ShipType.RoRoCargoShip
                        or ShipType.RoRoPassengerShip
                        or ShipType.RoRoPassengerShip_HighSpeedSOLAS
                        or ShipType.CruisePassengerShip:
                        {
                            if (ship.GrossTonnage <= 0)
                            {
                                throw new NotSupportedException($"Gross tonnage must be greater than 0 for ship type {ship.ShipType} Was provided {ship.GrossTonnage}");
                            }
                            break;
                        }
                    case ShipType.UNKNOWN:
                        throw new NotSupportedException($"Ship type '{ship.ShipType}' not supported");
                    default:
                        throw new NotSupportedException($"Ship type '{ship.ShipType}' not supported");
                }
            }
        }
    }




    

   
}