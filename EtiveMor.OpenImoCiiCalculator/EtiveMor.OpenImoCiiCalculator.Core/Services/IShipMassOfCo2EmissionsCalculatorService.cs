using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface IShipMassOfCo2EmissionsCalculatorService
    {
        double GetFuelCarbonContent(TypeOfFuel fuelType);
        double GetFuelLowerCalorificValue(TypeOfFuel fuelType);
        double GetFuelMassConversionFactor(TypeOfFuel fuelType);
        double GetMassOfCo2Emissions(TypeOfFuel fuelType, double fuelConsumptionMassInGrams);
    }
}