using EtiveMor.OpenImoCiiCalculator.Core;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;
using Newtonsoft.Json;

namespace EtiveMor.OpenImoCiiCalculator.DemoConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
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
    }
}