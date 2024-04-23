using EtiveMor.OpenImoCiiCalculator.Core;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Dto;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using Newtonsoft.Json;

namespace EtiveMor.OpenImoCiiCalculator.DemoConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("Generating a multi-fuel ship report...");
            Console.WriteLine("---------------------");

            MainMultiFuelCalculation(args);

            Console.WriteLine("---------------------");
            Console.WriteLine("Completed the multi-fuel ship report...");
            Console.WriteLine("---------------------");


            Console.WriteLine("---------------------");
            Console.WriteLine("Generating a single-fuel ship report...");
            Console.WriteLine("---------------------");

            MainOneFuelCalculation(args);

            Console.WriteLine("---------------------");
            Console.WriteLine("Completed the single-fuel ship report...");
            Console.WriteLine("---------------------");
        }



        /// <summary>
        /// Runs the single fuel calculation with sample data
        /// </summary>
        /// <param name="args"></param>
        static void Old_MainOneFuelCalculation(string[] args)
        {
            Console.WriteLine("Generating a ship result now...");

            var calculator = new ShipCarbonIntensityCalculator();

            double fuelConsumptionInMegaTons = 19_000;

            var result = calculator.CalculateAttainedCiiRating(
               ShipType.RoRoPassengerShip,
                grossTonnage: 25_000,
                deadweightTonnage: 0,
                distanceTravelled: 150_000,
                TypeOfFuel.DIESEL_OR_GASOIL,
                fuelConsumption: fuelConsumptionInMegaTons * 1_000_000,
                2019);


            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine(json);
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }



        /// <summary>
        /// Runs the single fuel calculation with sample data
        /// </summary>
        /// <param name="args"></param>
        static void MainOneFuelCalculation(string[] args)
        {
            Console.WriteLine("Generating a ship result now...");

            var calculator = new ShipCarbonIntensityCalculator();

            double fuelConsumptionInMegaTons = 19_000;

            var result = calculator.CalculateAttainedCiiRating(
               ShipType.RoRoPassengerShip,
                grossTonnage: 25_000,
                deadweightTonnage: 0,
                distanceTravelled: 150_000,
                TypeOfFuel.DIESEL_OR_GASOIL,
                fuelConsumption: fuelConsumptionInMegaTons * 1_000_000,
                2019);


            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine(json);
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }



        /// <summary>
        /// Runs the multi-fuel calculation with sample data
        /// </summary>
        /// <param name="args"></param>
        static void MainMultiFuelCalculation(string[] args)
        {
            var calculator = new ShipCarbonIntensityCalculator();

            double fuelConsumptionDieselInMegaTons = 12_500;
            double fuelConsumptionLightFuelInMegaTons = 10_000; // 35_000;


            var result = calculator.CalculateAttainedCiiRating(
                ShipType.RoRoPassengerShip,
                grossTonnage: 25_000,
                deadweightTonnage: 0,
                distanceTravelled: 150_000,
                new List<FuelTypeConsumption> { 
                    new FuelTypeConsumption
                    {
                        FuelConsumption = fuelConsumptionDieselInMegaTons * 1_000_000,
                        FuelType = TypeOfFuel.DIESEL_OR_GASOIL
                    },
                    new FuelTypeConsumption
                    {
                        FuelConsumption = fuelConsumptionLightFuelInMegaTons * 1_000_000,
                        FuelType = TypeOfFuel.LIGHTFUELOIL
                    }
                },
                2019);

            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            Console.WriteLine(json);
            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
        }
    }
}