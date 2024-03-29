namespace EtiveMor.OpenImoCiiCalculator.Core.Services
{
    internal interface IShipTransportWorkCalculatorService
    {
        double GetShipTransportWork(double capacity, double distanceSailed);
    }
}