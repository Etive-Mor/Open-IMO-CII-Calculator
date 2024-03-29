using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface IRatingBoundariesService
    {
        DdVectorDataTableRow GetBoundaries(Ship ship, double requiredCiiInYear);

    }
}