namespace EtiveMor.OpenImoCiiCalculator.Core.Models.Dto
{
    /// <summary>
    /// A request object to hold metadata about the annual consumption of a ship
    /// </summary>
    public class AnnaulConsumption
    {
        /// <summary>
        /// The year this consumption measures
        /// </summary>
        public int TargetYear { get; set; }

        /// <summary>
        /// The total fuel consumption in the given year
        /// </summary>
        public IEnumerable<FuelTypeConsumption> FuelConsumption { get; set; }
    }

}
