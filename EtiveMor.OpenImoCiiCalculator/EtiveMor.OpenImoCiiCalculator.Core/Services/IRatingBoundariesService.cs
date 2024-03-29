using EtiveMor.OpenImoCiiCalculator.Core.Models.MeasurementModels;
using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface IRatingBoundariesService
    {
        ShipDdVectorBoundaries GetBoundaries(Ship ship, double requiredCiiInYear, int year);

    }
}