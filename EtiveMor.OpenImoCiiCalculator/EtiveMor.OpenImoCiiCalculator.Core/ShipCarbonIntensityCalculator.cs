using EtiveMor.OpenImoCiiCalculator.Core.Models;
using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Models.MeasurementModels;
using EtiveMor.OpenImoCiiCalculator.Core.Services;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.FileIO;

namespace EtiveMor.OpenImoCiiCalculator.Core
{
    public class ShipCarbonIntensityCalculator
    {
        IShipMassOfCo2EmissionsCalculatorService _shipMassOfCo2EmissionsService;
        IShipCapacityCalculatorService _shipCapacityService;
        IShipTransportWorkCalculatorService _shipTransportWorkService;
        ICarbonIntensityIndicatorCalculatorService _carbonIntensityIndicatorService;
        IRatingBoundariesService _ratingBoundariesService;


        public ShipCarbonIntensityCalculator()
        {
            _shipMassOfCo2EmissionsService = new ShipMassOfCo2EmissionsCalculatorService();
            _shipCapacityService = new ShipCapacityCalculatorService();
            _shipTransportWorkService = new ShipTransportWorkCalculatorService();
            _carbonIntensityIndicatorService = new CarbonIntensityIndicatorCalculatorService();
            _ratingBoundariesService = new RatingBoundariesService();
        }

        /// <summary>
        /// Calculate the attained CII rating for a ship for a given year
        /// 
        /// This method accepts multiple fuel types.
        /// </summary>
        /// <param name="shipType">The type of ship</param>
        /// <param name="grossTonnage">in long-tons</param>
        /// <param name="deadweightTonnage">in long-tons</param>
        /// <param name="distanceTravelled">distance travelled in nautical miles</param>
        /// <param name="fuelType">The type of fuel</param>
        /// <param name="fuelConsumption">quantity of fuel consumed in grams</param>
        /// <returns>
        /// A <see cref="CalculationResult"/> containing details of the ship's carbon intensity rating
        /// </returns>
        public CalculationResult CalculateAttainedCiiRating(
            ShipType shipType,
            double grossTonnage,
            double deadweightTonnage,
            double distanceTravelled,
            IEnumerable<FuelTypeConsumption> fuelTypeConsumptions,  
            int targetYear)
        {
            if (fuelTypeConsumptions == null || fuelTypeConsumptions.Count() == 0)
            {
                throw new ArgumentException("FuelTypeConsumptions must be provided");
            }
            double shipCo2Emissions = 0;
            foreach (var consumption in fuelTypeConsumptions)
            {
                shipCo2Emissions += _shipMassOfCo2EmissionsService.GetMassOfCo2Emissions(consumption.FuelType, consumption.FuelConsumption);
            }
            var shipCapacity = _shipCapacityService.GetShipCapacity(shipType, deadweightTonnage, grossTonnage);
            var transportWork = _shipTransportWorkService.GetShipTransportWork(shipCapacity, distanceTravelled);

            List<ResultYear> results = new List<ResultYear>();
            for (int year = 2019; year <= 2030; year++)
            {
                var attainedCiiInYear = _carbonIntensityIndicatorService.GetAttainedCarbonIntensity(shipCo2Emissions, transportWork);
                var requiredCiiInYear = _carbonIntensityIndicatorService.GetRequiredCarbonIntensity(shipType, shipCapacity, year);

                var vectors = _ratingBoundariesService.GetBoundaries(new Ship(shipType, deadweightTonnage, grossTonnage), requiredCiiInYear, year);
                var rating = GetImoCiiRatingFromVectors(vectors, attainedCiiInYear, year);

                results.Add(new ResultYear
                {
                    IsMeasuredYear = targetYear == year,
                    Year = year,
                    AttainedCii = attainedCiiInYear,
                    RequiredCii = requiredCiiInYear,
                    Rating = rating,
                    VectorBoundariesForYear = vectors,
                    CalculatedCo2eEmissions = shipCo2Emissions,
                    CalculatedShipCapacity = shipCapacity,
                    CalculatedTransportWork = transportWork
                });
            }

            return new CalculationResult(results);
        }

        /// <summary>
        /// Calculate the attained CII rating for a ship for a given year
        /// 
        /// This method accepts exactly one fuel type. For multiple fuel
        /// types, use the <seealso cref="CalculateAttainedCiiRating(ShipType, double, double, double, IEnumerable{FuelTypeConsumption}, int)"/> method
        /// </summary>
        /// <param name="shipType">The type of ship</param>
        /// <param name="grossTonnage">in long-tons</param>
        /// <param name="deadweightTonnage">in long-tons</param>
        /// <param name="distanceTravelled">distance travelled in nautical miles</param>
        /// <param name="fuelType">The type of fuel</param>
        /// <param name="fuelConsumption">quantity of fuel consumed in grams</param>
        /// <returns>
        /// A <see cref="CalculationResult"/> containing details of the ship's carbon intensity rating
        /// </returns>
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

                var vectors = _ratingBoundariesService.GetBoundaries(new Ship(shipType, deadweightTonnage, grossTonnage), requiredCiiInYear, year);
                var rating = GetImoCiiRatingFromVectors(vectors, attainedCiiInYear, year);

                results.Add(new ResultYear
                {
                    IsMeasuredYear = targetYear == year,
                    Year = year,
                    AttainedCii = attainedCiiInYear,
                    RequiredCii = requiredCiiInYear,
                    Rating = rating,
                    VectorBoundariesForYear = vectors,
                    CalculatedCo2eEmissions = shipCo2Emissions,
                    CalculatedShipCapacity = shipCapacity,
                    CalculatedTransportWork = transportWork
                });
            }

            return new CalculationResult(results);
        }


        private ImoCiiRating GetImoCiiRatingFromVectors(ShipDdVectorBoundaries boundaries, double attainedCiiInYear, int year)
        {
            if (attainedCiiInYear < boundaries.BoundaryDdVectors[ImoCiiBoundary.Superior])
            {
                // lower than the "superior" boundary
                return ImoCiiRating.A;
            }
            else if (attainedCiiInYear < boundaries.BoundaryDdVectors[ImoCiiBoundary.Lower])
            {
                // lower than the "lower" boundary
                return ImoCiiRating.B;
            }
            else if (attainedCiiInYear < boundaries.BoundaryDdVectors[ImoCiiBoundary.Upper])
            {
                // lower than the "upper" boundary
                return ImoCiiRating.C;
            }
            else if (attainedCiiInYear < boundaries.BoundaryDdVectors[ImoCiiBoundary.Inferior])
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

    }

    public class AnnaulConsumption
    {
        public int TargetYear { get; set; }
        public IEnumerable<FuelTypeConsumption> FuelConsumption { get; set; }
    }

    public class FuelTypeConsumption
    {
        public TypeOfFuel FuelType { get; set; }
        public double FuelConsumption { get; set; }
    }
   
}
