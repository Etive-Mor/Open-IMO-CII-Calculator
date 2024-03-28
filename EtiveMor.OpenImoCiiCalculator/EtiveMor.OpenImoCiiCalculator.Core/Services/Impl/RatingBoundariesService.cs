using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services.Impl
{
    public interface IRatingBoundariesService
    {
        Dictionary<ImoCiiBoundary, double> GetBoundaries(ShipType shipType, double requiredCiiInYear);

    }
    public class RatingBoundariesService : IRatingBoundariesService
    {
        public Dictionary<ImoCiiBoundary, double> GetBoundaries(ShipType shipType, double requiredCiiInYear)
        {
            switch (shipType)
            {
                case ShipType.RoRoPassengerShip:
                    {
                        return new Dictionary<ImoCiiBoundary, double> {
                            { ImoCiiBoundary.Superior,      0.76 *  requiredCiiInYear },
                            { ImoCiiBoundary.Lower,         0.92 *  requiredCiiInYear},
                            { ImoCiiBoundary.Upper,         1.14 *  requiredCiiInYear},
                            { ImoCiiBoundary.Inferior,      1.30 *  requiredCiiInYear}
                        };
                    }
                default:
                    throw new NotSupportedException($"Ship type '{shipType}' not supported");
            }
        }
    }
}
