namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface ICarbonIntensityIndicatorCalculatorService
    {
        double GetAttainedCarbonIntensity(double massOfCo2Emissions, double transportWork);
        void GetReferenceCarbonIntensity();
    }
}