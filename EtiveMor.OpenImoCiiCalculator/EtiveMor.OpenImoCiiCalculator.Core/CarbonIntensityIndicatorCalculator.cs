namespace EtiveMor.OpenImoCiiCalculator.Core
{
    public class CarbonIntensityIndicatorCalculator
    {
        /// <summary>
        /// Gets a ship's attained carbon intensity, which is the ratio of the cumulative mass
        /// of CO2 emissions in a calendar year to the ship's transport work in a calendar year
        /// </summary>
        /// <param name="massOfCo2Emissions">
        /// The cumulative mass of CO2 emissions in a calendar year in grams (g)
        /// <seealso cref="ShipMassOfCo2EmissionsCalculator.GetMassOfCo2Emissions(Models.Enums.TypeOfFuel, double)"/>
        /// </param>
        /// <param name="transportWork">
        /// The ship's transport work in a calendar year
        /// <seealso cref="ShipTransportWorkCalculator.GetShipTransportWork(double, double)"/>"/>
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
    }
}
