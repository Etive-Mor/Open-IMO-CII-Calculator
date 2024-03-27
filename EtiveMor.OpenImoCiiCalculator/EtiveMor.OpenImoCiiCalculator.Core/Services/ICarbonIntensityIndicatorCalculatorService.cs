using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface ICarbonIntensityIndicatorCalculatorService
    {
        double GetAttainedCarbonIntensity(double massOfCo2Emissions, double transportWork);
        double GetRequiredCarbonIntensity(ShipType shipType, double capacity, int year);
        Dictionary<int, double> GetRequiredCarbonIntensity(ShipType shipType, double capacity);
    }
}