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
        public Calculator()
        {
            _shipMassOfCo2EmissionsService = new ShipMassOfCo2EmissionsCalculatorService();
            _shipCapacityService = new ShipCapacityCalculatorService();
            _shipTransportWorkService = new ShipTransportWorkCalculatorService();
            _carbonIntensityIndicatorService = new CarbonIntensityIndicatorCalculatorService();
        }


        public ImoCiiRating CalculateAttainedCiiRating(ShipType shipType, double grossTonnage, double deadweightTonnage, double distanceTravelled, TypeOfFuel fuelType, double fuelConsumption)
        {
            var shipCo2Emissions = _shipMassOfCo2EmissionsService.GetMassOfCo2Emissions(fuelType, fuelConsumption);
            var shipCapacity = _shipCapacityService.GetShipCapacity(shipType, grossTonnage, deadweightTonnage);
            var transportWork = _shipTransportWorkService.GetShipTransportWork(shipCapacity, distanceTravelled);
            var attainedCii = _carbonIntensityIndicatorService.GetAttainedCarbonIntensity(shipCo2Emissions, transportWork);




            return ImoCiiRating.ERR;
        }


        /// <summary>
        /// CII = (annualFuelConsumption * co2eqEmissionsFactor) / (distanceSailed * capacity)
        /// </summary>
        /// <param name="annualFuelConsumption"></param>
        /// <param name="co2eqEmissionsFactor"></param>
        /// <param name="distanceSailed"></param>
        /// <param name="capacity"></param>
        /// <param name="deadweightTonnage"></param>
        /// <param name="shipType"></param>
        /// <returns></returns>
        public ImoCiiRating CalculateImoCiiRating(double annualFuelConsumption, double co2eqEmissionsFactor, double distanceSailed, double capacity, double deadweightTonnage, double grossTonnage, ShipType shipType)
        {
            double massOfCo2Emissions = annualFuelConsumption * co2eqEmissionsFactor;
            double transportWork = annualFuelConsumption * co2eqEmissionsFactor;
            var cii = massOfCo2Emissions / transportWork;

            return ImoCiiRating.ERR;
        }



        /// <summary>
        /// Calculates the mass of CO2 emissions from a ship, given the mass of CO2eq emissions, and the transport work undertaken by the ship in a full calendar year.
        /// 
        /// 
        /// </summary>
        /// <param name="massOfCo2Emissions"></param>
        /// <param name="transportWork"></param>
        /// <returns></returns>
        public ImoCiiRating CalculateImoCiiRating(decimal massOfCo2Emissions, decimal transportWork)
        {
            double cii = (double)(massOfCo2Emissions / transportWork);

            if (cii < 0.0001)
            {
                return ImoCiiRating.A;
            }
            else if (cii >= 0.0001 && cii < 0.0002)
            {
                return ImoCiiRating.B;
            }
            else if (cii >= 0.0002 && cii < 0.0003)
            {
                return ImoCiiRating.C;
            }
            else if (cii >= 0.0003 && cii < 0.0004)
            {
                return ImoCiiRating.D;
            }
            else if (cii >= 0.0004)
            {
                return ImoCiiRating.E;
            }
            else
            {
                return ImoCiiRating.ERR;
            }
        }

        /// <summary>
        /// Calculates the transport work undertaken by a ship, given its deadweight tonnage and distance sailed in a calendar year.
        /// </summary>
        /// <param name="deadweightTonnage">
        /// The Capacity of the ship in metric Tons
        ///     
        ///     For cargo ships submit the Deadweight Tonnage
        ///     For cruise ships submit the Gross Tonnage
        /// </param>
        /// <param name="distanceSailedCalendarYear">
        /// The distance travelled by the ship across one full calendar year
        /// </param>
        /// <returns></returns>
        public decimal CalculateTransportWork(decimal deadweightTonnage, decimal distanceSailedCalendarYear, ShipType shipType)
        {
            return deadweightTonnage * distanceSailedCalendarYear;
        }


        
    }
}
