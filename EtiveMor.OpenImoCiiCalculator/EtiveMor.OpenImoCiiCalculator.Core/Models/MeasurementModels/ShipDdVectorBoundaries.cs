using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Models.MeasurementModels
{
    public class ShipDdVectorBoundaries
    {
        /// <summary>
        /// IMO MEPC.354(78) ddvectors for a given year for the specified ship type
        /// </summary>
        /// <param name="shipType">
        /// The type of ship to generate ddvector boundaries for
        /// </param>
        /// <param name="weightClassification">
        /// The weight classification of the ship to generate ddvector boundaries for.
        /// 
        /// If these ddvectors have a min/max weight boundary in MEPC.354(78), this object describes the 
        /// lower and upper bound of that classification. For example, Gas Carriers below
        /// 65000 DWT have different ddvectors to those at or above 65000 DWT
        /// 
        /// If the ddvectors do not have a weight classification, this object will contain
        /// a range between 0 and int.MaxValue
        /// </param>
        /// <param name="capacityUnit">
        /// Indicates the capacity unit these ddvectors are calculated against. For example
        /// GT for passenger/ro-ro ships and DWT for cargo carriers
        /// </param>
        /// <param name="boundaryDdVectors">
        /// The ddvectors for the specified ship type, weight classification and capacity unit in the given year
        /// </param>
        /// <param name="year">
        /// The year these ddvectors apply to. Note that the ddvectors are only valid for the specified
        /// calendar year
        /// </param>
        public ShipDdVectorBoundaries(
            ShipType shipType,
            WeightClassification weightClassification,
            CapacityUnit capacityUnit,
            Dictionary<ImoCiiBoundary, double> boundaryDdVectors, 
            int year)
        {
            ShipType = shipType;
            WeightClassification = weightClassification;
            CapacityUnit = capacityUnit;
            BoundaryDdVectors = boundaryDdVectors;
            Year = year;
        }

        /// <summary>
        /// The year these DDVector boundaries apply to 
        /// </summary>
        public int Year { get; private set; }

        /// <summary>
        /// The shipType these vector boundaries apply to
        /// </summary>
        public ShipType ShipType { get; set; }

        /// <summary>
        /// The weight classification these vector boundaries apply to
        /// </summary>
        public WeightClassification WeightClassification { get; set; }
        
        /// <summary>
        /// The capacity unit these vector boundaries apply to
        /// </summary>
        public CapacityUnit CapacityUnit { get; set; }

        public Dictionary<ImoCiiBoundary, double> BoundaryDdVectors { get; set; }
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
