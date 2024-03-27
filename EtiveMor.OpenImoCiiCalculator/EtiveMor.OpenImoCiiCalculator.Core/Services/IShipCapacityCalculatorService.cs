using EtiveMor.OpenImoCiiCalculator.Core.Models;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface IShipCapacityCalculatorService
    {
        double GetShipCapacity(Ship ship);
        double GetShipCapacity(ShipType shipType, double deadweightTonnage, double grossTonnage);
    }
}