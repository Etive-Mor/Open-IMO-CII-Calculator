namespace EtiveMor.OpenImoCiiCalculator.Core.Services.Impl
{
    public class ShipTransportWorkCalculatorService : IShipTransportWorkCalculatorService
    {

        /// <summary>
        /// Calculates a ship's transport work, which is the product of the ship's capacity and the distance sailed in a calendar year
        /// </summary>
        /// <param name="capacity">
        /// The ship's capacity, as calculated in accordance with MEPC 337(76)
        /// </param>
        /// <param name="distanceSailed">
        /// The distance sailed in nautical miles (nm) in a calendar year
        /// </param>
        /// <returns>
        /// A double representing the ship's transport work in a calendar year
        /// </returns>
        /// <remarks>
        /// A ship's capacity can be calculated with <seealso cref="ShipCapacityCalculatorService.GetShipCapacity(Models.Enums.ShipType, double, double)"/>
        /// </remarks>
        public double GetShipTransportWork(double capacity, double distanceSailed)
        {
            return capacity * distanceSailed;
        }
    }
}
