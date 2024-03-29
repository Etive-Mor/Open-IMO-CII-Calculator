using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface IRatingBoundariesService
    {
        ShipDdVectorBoundaries GetBoundaries(Ship ship, double requiredCiiInYear);

    }
}