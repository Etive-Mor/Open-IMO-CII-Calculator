using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Models.MeasurementModels
{
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
}
