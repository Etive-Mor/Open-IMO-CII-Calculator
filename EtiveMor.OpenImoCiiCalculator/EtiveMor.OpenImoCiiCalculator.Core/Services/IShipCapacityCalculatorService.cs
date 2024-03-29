using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface IShipCapacityCalculatorService
    {
        double GetShipCapacity(Ship ship);
        // double GetShipCapacity(ShipType shipType, double deadweightTonnage, double grossTonnage);
    }
}