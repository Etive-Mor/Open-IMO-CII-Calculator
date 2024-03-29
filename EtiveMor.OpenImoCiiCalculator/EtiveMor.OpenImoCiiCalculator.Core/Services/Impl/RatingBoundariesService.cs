using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services.Impl
{
    public class RatingBoundariesService : IRatingBoundariesService
    {
        /// <summary>
        /// Returns the boundaries for the given ship and required CII in the given year.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="requiredCiiInYear"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public ShipDdVectorBoundaries GetBoundaries(Ship ship, double requiredCiiInYear)
        {
            switch (ship.ShipType)
            {
                case ShipType.BulkCarrier:
                    {
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
                            });
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
                               });
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
                                });
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
                            });
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
                            });
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
                            });
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
                            });
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
                            });
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
                            });
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
                            });
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
                            });
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
                            });
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
                            });
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
                            });
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
                            });
                    }

                case ShipType.UNKNOWN:
                    throw new NotSupportedException($"Ship type '{ship.ShipType}' not supported");
                default:
                    throw new NotSupportedException($"Ship type '{ship.ShipType}' not supported");
            }
        }
    }




    public class ShipDdVectorBoundaries
    {
        public ShipDdVectorBoundaries(
            ShipType shipType,
            WeightClassification weightClassification,
            CapacityUnit capacityUnit,
            Dictionary<ImoCiiBoundary, double> boundaryDdVectors2019)
        {
            ShipType = shipType;
            WeightClassification = weightClassification;
            CapacityUnit = capacityUnit;
            BoundaryDdVectors2019 = boundaryDdVectors2019;
        }

        public ShipType ShipType { get; set; }
        public WeightClassification WeightClassification { get; set; }
        public CapacityUnit CapacityUnit { get; set; }

        public Dictionary<ImoCiiBoundary, double> BoundaryDdVectors2019 { get; set; }
    }

    public class WeightClassification
    {
        public WeightClassification(int upperLimit, int lowerLimit)
        {
            UpperLimit = upperLimit;
            LowerLimit = lowerLimit;
        }

        public int UpperLimit { get; private set; }
        public int LowerLimit { get; private set; }
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