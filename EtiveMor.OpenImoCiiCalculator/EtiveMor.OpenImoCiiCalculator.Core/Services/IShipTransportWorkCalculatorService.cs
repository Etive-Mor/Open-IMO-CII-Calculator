namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    public interface IShipTransportWorkCalculatorService
    {
        double GetShipTransportWork(double capacity, double distanceSailed);
    }
}