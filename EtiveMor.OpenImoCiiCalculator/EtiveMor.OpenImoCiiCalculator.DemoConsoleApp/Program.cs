using EtiveMor.OpenImoCiiCalculator.Core;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
        static void MainOneFuelCalculation(string[] args)
        {
            Console.WriteLine("Generating a ship result now...");

            var calculator = new ShipCarbonIntensityCalculator();

            var result = calculator.CalculateAttainedCiiRating(
               ShipType.RoRoPassengerShip,
                grossTonnage: 25000,
                deadweightTonnage: 0,
                distanceTravelled: 150000,
                TypeOfFuel.DIESEL_OR_GASOIL,
                fuelConsumption: 1.9e+10,
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

            var result = calculator.CalculateAttainedCiiRating(
                ShipType.RoRoPassengerShip,
                grossTonnage: 25000,
                deadweightTonnage: 0,
                distanceTravelled: 150000,
                new List<FuelTypeConsumption> { 
                    new FuelTypeConsumption
                    {
                        FuelConsumption = 1.9e+10,
                        FuelType = TypeOfFuel.DIESEL_OR_GASOIL
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