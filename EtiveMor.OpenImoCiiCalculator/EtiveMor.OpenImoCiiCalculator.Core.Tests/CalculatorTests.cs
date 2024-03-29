using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestCalculator()
        {
            var _calc = new Calculator();

            var result = _calc.CalculateAttainedCiiRating(
                ShipType.RoRoPassengerShip, 
                grossTonnage: 25000, 
                deadweightTonnage: 0, 
                distanceTravelled: 150000, 
                TypeOfFuel.DIESEL_OR_GASOIL,
                fuelConsumption: 1.9e+10,
                2019
                );

            System.Diagnostics.Debug.WriteLine("Basic result is:");

            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            System.Diagnostics.Debug.WriteLine(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Results.Count(), 12);

            Assert.IsTrue(result.Results.Count(result => result.IsMeasuredYear) == 1);
            Assert.IsTrue(result.Results.Count(result => result.IsEstimatedYear) == 11);
        }



        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2019, ImoCiiRating.B, 19.184190519387734, 16.243733333333335, 0.8467249799733408)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2020, ImoCiiRating.B, 18.992348614193855, 16.243733333333335, 0.8552777575488293)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2021, ImoCiiRating.B, 18.80050670899998,  16.243733333333335, 0.8640050816054499)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2022, ImoCiiRating.B, 18.6086648038061, 16.243733333333335, 0.8729123504879803)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2023, ImoCiiRating.B, 18.224980993418345, 16.243733333333335, 0.8912894526035168)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2024, ImoCiiRating.B, 17.84129718303059, 16.243733333333335, 0.9104569677132699)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2025, ImoCiiRating.C, 17.45761337264284, 16.243733333333335, 0.9304670109597152)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2026, ImoCiiRating.C, 17.073929562255085, 16.243733333333335, 0.9513763819925177)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2027, ImoCiiRating.C, 16.690245751867327, 16.243733333333335, 0.9732471034176333)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2028, ImoCiiRating.C, 16.306561941479572, 16.243733333333335, 0.996147035262754)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2029, ImoCiiRating.C, 15.922878131091819, 16.243733333333335, 1.0201505782811335)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10, 2030, ImoCiiRating.C, 15.539194320704066, 16.243733333333335, 1.0453394814485688)]
        [TestMethod]
        public void TestRoRoPassengerShipReturnsExpectedValues(
            ShipType shipType, 
            double deadweightTonnage,
            double grossTonnage,
            TypeOfFuel typeOfFuel, 
            double fuelConsumption, 
            int year,
            ImoCiiRating expectedRating, 
            double expectedRequiredCii, 
            double expectedAttainedCii,
            double expectedArRatio)
        {
            var _calc = new Calculator();

            var result = _calc.CalculateAttainedCiiRating(
                shipType,
                grossTonnage: grossTonnage,
                deadweightTonnage: deadweightTonnage,
                distanceTravelled: 150000,
                fuelType: typeOfFuel,
                fuelConsumption: fuelConsumption,
                year
                );

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Results.Count(), 12);

            Assert.IsTrue(result.Results.Count(result => result.IsMeasuredYear) == 1);
            Assert.IsTrue(result.Results.Count(result => result.IsEstimatedYear) == 11);

            Assert.AreEqual(result.Results.First(c => c.Year == year).Year, year);
            Assert.AreEqual(result.Results.First(c => c.Year == year).VectorBoundariesForYear.ShipType, shipType);
            Assert.AreEqual(result.Results.First(c => c.Year == year).RequiredCii, expectedRequiredCii);
            Assert.AreEqual(result.Results.First(c => c.Year == year).AttainedRequiredRatio, expectedArRatio);
            Assert.AreEqual(result.Results.First(c => c.Year == year).AttainedCii, expectedAttainedCii);
            Assert.AreEqual(result.Results.First(c => c.Year == year).Rating, expectedRating);
            Assert.AreNotEqual(result.Results.First(c => c.Year == year).IsMeasuredYear, result.Results.First(c => c.Year == year).IsEstimatedYear);
        }
    }
}
