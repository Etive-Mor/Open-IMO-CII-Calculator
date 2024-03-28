using EtiveMor.OpenImoCiiCalculator.Core.Extensions;
using EtiveMor.OpenImoCiiCalculator.Core.Models;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Services;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;

namespace EtiveMor.OpenImoCiiCalculator.Core
{
    public class Calculator
    {
        IShipMassOfCo2EmissionsCalculatorService _shipMassOfCo2EmissionsService;
        IShipCapacityCalculatorService _shipCapacityService;
        IShipTransportWorkCalculatorService _shipTransportWorkService;
        ICarbonIntensityIndicatorCalculatorService _carbonIntensityIndicatorService;
        IRatingBoundariesService _ratingBoundariesService;
        public Calculator()
        {
            _shipMassOfCo2EmissionsService = new ShipMassOfCo2EmissionsCalculatorService();
            _shipCapacityService = new ShipCapacityCalculatorService();
            _shipTransportWorkService = new ShipTransportWorkCalculatorService();
            _carbonIntensityIndicatorService = new CarbonIntensityIndicatorCalculatorService();
            _ratingBoundariesService = new RatingBoundariesService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="grossTonnage">in long-tons</param>
        /// <param name="deadweightTonnage">in long-tons</param>
        /// <param name="distanceTravelled">distance travelled in nautical miles</param>
        /// <param name="fuelType"></param>
        /// <param name="fuelConsumption">quantity of fuel consumed in grams</param>
        /// <returns></returns>
        public CalculationResult CalculateAttainedCiiRating(
            ShipType shipType, 
            double grossTonnage, 
            double deadweightTonnage, 
            double distanceTravelled, 
            TypeOfFuel fuelType, 
            double fuelConsumption, 
            int targetYear)
        {
            var shipCo2Emissions = _shipMassOfCo2EmissionsService.GetMassOfCo2Emissions(fuelType, fuelConsumption);
            var shipCapacity = _shipCapacityService.GetShipCapacity(shipType, deadweightTonnage, grossTonnage);
            var transportWork = _shipTransportWorkService.GetShipTransportWork(shipCapacity, distanceTravelled);

            List<ResultYear> results = new List<ResultYear>();
            for (int year = 2019; year <= 2030; year++)
            {
                var attainedCiiInYear = _carbonIntensityIndicatorService.GetAttainedCarbonIntensity(shipCo2Emissions, transportWork);
                var requiredCiiInYear = _carbonIntensityIndicatorService.GetRequiredCarbonIntensity(shipType, shipCapacity, year);


                results.Add(new ResultYear
                {
                    IsMeasuredYear = targetYear == year,
                    Year = year,
                    AttainedCii = attainedCiiInYear,
                    RequiredCii = requiredCiiInYear,
                    Rating = GetImoCiiRatingInYear(attainedCiiInYear, requiredCiiInYear, year),
                    Boundaries = GetBoundaries(shipType, requiredCiiInYear)
                });
            }

            return new CalculationResult(results);
        }

        private ImoCiiRating GetImoCiiRatingInYear(double attainedCiiInYear, double requiredCiiInYear, int year) 
        {
            var gradeLowerBoundaries = GetBoundaries(ShipType.RoRoCruisePassengerShip, requiredCiiInYear);

            if (attainedCiiInYear < gradeLowerBoundaries[ImoCiiBoundary.Superior])
            {
                // lower than the "superior" boundary
                return ImoCiiRating.A;
            }
            else if (attainedCiiInYear < gradeLowerBoundaries[ImoCiiBoundary.Lower])
            {
                // lower than the "lower" boundary
                return ImoCiiRating.B;
            }
            else if (attainedCiiInYear < gradeLowerBoundaries[ImoCiiBoundary.Upper])
            {
                // lower than the "upper" boundary
                return ImoCiiRating.C;
            }
            else if (attainedCiiInYear < gradeLowerBoundaries[ImoCiiBoundary.Inferior])
            {
                // lower than the "inferior" boundary
                return ImoCiiRating.D;
            }
            else
            {
                // higher than the inferior boundary
                return ImoCiiRating.E;
            }
        }

        private Dictionary<ImoCiiBoundary, double> GetBoundaries(ShipType shipType, double requiredCiiInYear)
        {
            return new Dictionary<ImoCiiBoundary, double> {
                { ImoCiiBoundary.Superior,      0.72 *  requiredCiiInYear },
                { ImoCiiBoundary.Lower,         0.90 *  requiredCiiInYear},
                { ImoCiiBoundary.Upper,         1.12 *  requiredCiiInYear},
                { ImoCiiBoundary.Inferior,      1.41 *  requiredCiiInYear}
            };
        }
    }
   
}
