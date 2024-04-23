using EtiveMor.OpenImoCiiCalculator.Core.Models.Dto;
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

        /// <summary>
        /// Tests a known and pre-calculated set of inputs for a RoRoPassengerShip
        /// </summary>
        [TestMethod]
        public void TestCalculatorSingleFuel()
        {
            var _calc = new ShipCarbonIntensityCalculator();

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

        /// <summary>
        /// Tests a known and pre-calculated set of inputs for a RoRoPassengerShip in the multi-fuel scenario
        /// </summary>
        [TestMethod]
        public void TestCalculatorMultiFuel()
        {
            var _calc = new ShipCarbonIntensityCalculator();

            var result = _calc.CalculateAttainedCiiRating(
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


        [TestMethod]
        public void TestCalculatorMultiFuelSameResultAsSingleFuel()
        {
            var _calc = new ShipCarbonIntensityCalculator();

            var resultMultiFuel = _calc.CalculateAttainedCiiRating(
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
                2019
                );

            var resultSingleFuel = _calc.CalculateAttainedCiiRating(
                   ShipType.RoRoPassengerShip,
                   grossTonnage: 25000,
                   deadweightTonnage: 0,
                   distanceTravelled: 150000,
                   TypeOfFuel.DIESEL_OR_GASOIL,
                   fuelConsumption: 1.9e+10,
                   2019
                   );

            var singleFuelAsJsonObj = JsonConvert.SerializeObject(resultSingleFuel);
            var multiFuelAsJsonObj = JsonConvert.SerializeObject(resultMultiFuel);

            Assert.AreEqual(singleFuelAsJsonObj, multiFuelAsJsonObj);
        }


        /// <summary>
        /// This tests that cumulative fuel consumption is the same as single fuel consumption when 
        /// using the multi-fuel mode of CalculatAttainedCiiRating
        /// </summary>
        [TestMethod]
        public void TestCalculatorMultiFuelSameResultWithZeroFuelConsumption()
        {
            var _calc = new ShipCarbonIntensityCalculator();

            double fuelConsumptionInMegaTons = 19_000;


            var resultMultiFuel = _calc.CalculateAttainedCiiRating(
                ShipType.RoRoPassengerShip,
                grossTonnage: 25000,
                deadweightTonnage: 0,
                distanceTravelled: 150000,
                new List<FuelTypeConsumption> {
                    new FuelTypeConsumption
                    {
                        FuelConsumption = fuelConsumptionInMegaTons * 1_000_000,
                        FuelType = TypeOfFuel.DIESEL_OR_GASOIL
                    }
                },
                2019
                );

            var resultWithZeroFuelConsumption = _calc.CalculateAttainedCiiRating(
                   ShipType.RoRoPassengerShip,
                   grossTonnage: 25000,
                   deadweightTonnage: 0,
                   distanceTravelled: 150000,
new List<FuelTypeConsumption> {
                    new FuelTypeConsumption
                    {
                        FuelConsumption = fuelConsumptionInMegaTons * 1_000_000,
                        FuelType = TypeOfFuel.DIESEL_OR_GASOIL
                    },
                    new FuelTypeConsumption
                    {
                        FuelConsumption = 0,
                        FuelType = TypeOfFuel.DIESEL_OR_GASOIL
                    }
                },
                   2019
                   );


            var resultWithMixedFuelConsumption = _calc.CalculateAttainedCiiRating(
       ShipType.RoRoPassengerShip,
       grossTonnage: 25000,
       deadweightTonnage: 0,
       distanceTravelled: 150000,
new List<FuelTypeConsumption> {
                    new FuelTypeConsumption
                    {
                        FuelConsumption = (fuelConsumptionInMegaTons  / 2) * 1_000_000,
                        FuelType = TypeOfFuel.DIESEL_OR_GASOIL
                    },
                    new FuelTypeConsumption
                    {
                        FuelConsumption = (fuelConsumptionInMegaTons  / 2) * 1_000_000,
                        FuelType = TypeOfFuel.DIESEL_OR_GASOIL
                    }
    },
       2019
       );

            var resultWithZeroFuelConsumptionAsJsonObj = JsonConvert.SerializeObject(resultWithZeroFuelConsumption);
            var multiFuelAsJsonObj = JsonConvert.SerializeObject(resultMultiFuel);
            var multiFuelWithMixedConsumption = JsonConvert.SerializeObject(resultWithMixedFuelConsumption);

            Assert.AreEqual(resultWithZeroFuelConsumptionAsJsonObj, multiFuelAsJsonObj);
            Assert.AreEqual(resultWithZeroFuelConsumptionAsJsonObj, multiFuelWithMixedConsumption);
            Assert.AreEqual(multiFuelAsJsonObj, multiFuelWithMixedConsumption);
        }



        /// <summary>
        /// Tests a known and pre-calculated set of inputs for a RoRoPassengerShip
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="deadweightTonnage"></param>
        /// <param name="grossTonnage"></param>
        /// <param name="typeOfFuel"></param>
        /// <param name="fuelConsumptionInMtms"></param>
        /// <param name="year"></param>
        /// <param name="expectedRating"></param>
        /// <param name="expectedRequiredCii"></param>
        /// <param name="expectedAttainedCii"></param>
        /// <param name="expectedArRatio"></param>
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2019, ImoCiiRating.B, 19.184190519387734, 16.243733333333335, 0.8467249799733408)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2020, ImoCiiRating.B, 18.992348614193855, 16.243733333333335, 0.8552777575488293)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2021, ImoCiiRating.B, 18.80050670899998, 16.243733333333335, 0.8640050816054499)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2022, ImoCiiRating.B, 18.6086648038061, 16.243733333333335, 0.8729123504879803)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2023, ImoCiiRating.B, 18.224980993418345, 16.243733333333335, 0.8912894526035168)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2024, ImoCiiRating.B, 17.84129718303059, 16.243733333333335, 0.9104569677132699)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2025, ImoCiiRating.C, 17.45761337264284, 16.243733333333335, 0.9304670109597152)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2026, ImoCiiRating.C, 17.073929562255085, 16.243733333333335, 0.9513763819925177)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2027, ImoCiiRating.C, 16.690245751867327, 16.243733333333335, 0.9732471034176333)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2028, ImoCiiRating.C, 16.306561941479572, 16.243733333333335, 0.996147035262754)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2029, ImoCiiRating.C, 15.922878131091819, 16.243733333333335, 1.0201505782811335)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 19_000, 2030, ImoCiiRating.C, 15.539194320704066, 16.243733333333335, 1.0453394814485688)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2019, ImoCiiRating.B, 19.184190519387734, 15.965066666666667, 0.8321991303480963)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2020, ImoCiiRating.B, 18.992348614193855, 15.965066666666667, 0.8406051821697944)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2021, ImoCiiRating.B, 18.80050670899998, 15.965066666666667, 0.849182786069486)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2022, ImoCiiRating.B, 18.6086648038061, 15.965066666666667, 0.8579372477815427)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2023, ImoCiiRating.B, 18.224980993418345, 15.965066666666667, 0.8759990845769436)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2024, ImoCiiRating.B, 17.84129718303059, 15.965066666666667, 0.8948377745678456)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2025, ImoCiiRating.B, 17.45761337264284, 15.965066666666667, 0.9145045388440618)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2026, ImoCiiRating.C, 17.073929562255085, 15.965066666666667, 0.9350552026383104)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2027, ImoCiiRating.C, 16.690245751867327, 15.965066666666667, 0.9565507245380419)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2028, ImoCiiRating.C, 16.306561941479572, 15.965066666666667, 0.9790578004095253)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2029, ImoCiiRating.C, 15.922878131091819, 15.965066666666667, 1.0026495546362606)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.LIGHTFUELOIL, 19_000, 2030, ImoCiiRating.C, 15.539194320704066, 15.965066666666667, 1.0274063337630819)]
        [TestMethod]
        public void TestRoRoPassengerShipReturnsExpectedValues(
            ShipType shipType,
            double deadweightTonnage,
            double grossTonnage,
            TypeOfFuel typeOfFuel,
            double fuelConsumptionInMt,
            int year,
            ImoCiiRating expectedRating,
            double expectedRequiredCii,
            double expectedAttainedCii,
            double expectedArRatio)
        {
            var _calc = new ShipCarbonIntensityCalculator();

            var fuelConsumptionInGrams = fuelConsumptionInMt * 1_000_000;

            var result = _calc.CalculateAttainedCiiRating(
                shipType,
                grossTonnage: grossTonnage,
                deadweightTonnage: deadweightTonnage,
                distanceTravelled: 150000,
                fuelType: typeOfFuel,
                fuelConsumption: fuelConsumptionInGrams,
                year
                );

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Results.Count(), 12);

            Assert.IsTrue(result.Results.Count(result => result.IsMeasuredYear) == 1);
            Assert.IsTrue(result.Results.Count(result => result.IsEstimatedYear) == 11);

            Assert.AreEqual(year, result.Results.First(c => c.Year == year).Year);
            Assert.AreEqual(shipType, result.Results.First(c => c.Year == year).VectorBoundariesForYear.ShipType);
            Assert.AreEqual(expectedRequiredCii, result.Results.First(c => c.Year == year).RequiredCii);
            Assert.AreEqual(expectedArRatio, result.Results.First(c => c.Year == year).AttainedRequiredRatio);
            Assert.AreEqual(expectedAttainedCii, result.Results.First(c => c.Year == year).AttainedCii);
            Assert.AreEqual(expectedRating, result.Results.First(c => c.Year == year).Rating);
            Assert.AreNotEqual(result.Results.First(c => c.Year == year).IsMeasuredYear, result.Results.First(c => c.Year == year).IsEstimatedYear);
        }







        /// <summary>
        /// Tests a known and pre-calculated set of inputs for a RoRoPassengerShip
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="deadweightTonnage"></param>
        /// <param name="grossTonnage"></param>
        /// <param name="typeOfFuelOne"></param>
        /// <param name="fuelConsumptionInMtOne"></param>
        /// <param name="typeOfFuelTwo"></param>
        /// <param name="fuelConsumptionInMtTwo"></param>
        /// <param name="year"></param>
        /// <param name="expectedRating"></param>
        /// <param name="expectedRequiredCii"></param>
        /// <param name="expectedAttainedCii"></param>
        /// <param name="expectedArRatio"></param>
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2019, ImoCiiRating.C, 19.184190519387734, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2020, ImoCiiRating.C, 18.992348614193855, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2021, ImoCiiRating.C, 18.80050670899998, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2022, ImoCiiRating.C, 18.6086648038061, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2023, ImoCiiRating.C, 18.224980993418345, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2024, ImoCiiRating.C, 17.84129718303059, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2025, ImoCiiRating.C, 17.45761337264284, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2026, ImoCiiRating.C, 17.073929562255085, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2027, ImoCiiRating.D, 16.690245751867327, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2028, ImoCiiRating.D, 16.306561941479572, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2029, ImoCiiRating.D, 15.922878131091819, 19.089333333333332)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 25000, TypeOfFuel.DIESEL_OR_GASOIL, 12_500, TypeOfFuel.LIGHTFUELOIL, 10_000, 2030, ImoCiiRating.D, 15.539194320704066, 19.089333333333332)]
        [TestMethod]
        public void TestMultiFuelRoRoPassengerShipReturnsExpectedValues(
            ShipType shipType,
            double deadweightTonnage,
            double grossTonnage,
            TypeOfFuel typeOfFuelOne,
            double fuelConsumptionInMtOne,
            TypeOfFuel typeOfFuelTwo,
            double fuelConsumptionInMtTwo,
            int year,
            ImoCiiRating expectedRating,
            double expectedRequiredCii,
            double expectedAttainedCii)
        {
            double expectedArRatio = expectedAttainedCii / expectedRequiredCii;

            var _calc = new ShipCarbonIntensityCalculator();

            var result = _calc.CalculateAttainedCiiRating(
                shipType,
                grossTonnage: grossTonnage,
                deadweightTonnage: deadweightTonnage,
                distanceTravelled: 150_000,
                new List<FuelTypeConsumption> {
                    new FuelTypeConsumption
                    {
                        FuelConsumption = fuelConsumptionInMtOne * 1_000_000,
                        FuelType = typeOfFuelOne
                    },
                    new FuelTypeConsumption
                    {
                        FuelConsumption = fuelConsumptionInMtTwo * 1_000_000,
                        FuelType = typeOfFuelTwo
                    }
                },
                year
                );


            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            System.Diagnostics.Debug.WriteLine(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Results.Count(), 12);

            Assert.IsTrue(result.Results.Count(result => result.IsMeasuredYear) == 1);
            Assert.IsTrue(result.Results.Count(result => result.IsEstimatedYear) == 11);

            Assert.AreEqual(result.Results.First(c => c.Year == year).Year, year, $"{nameof(year)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).VectorBoundariesForYear.ShipType, shipType, $"{nameof(shipType)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).RequiredCii, expectedRequiredCii, $"{nameof(expectedRequiredCii)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).AttainedCii, expectedAttainedCii, $"{nameof(expectedAttainedCii)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).AttainedRequiredRatio, expectedArRatio, $"{nameof(expectedArRatio)} value was incorrect");

            Assert.AreEqual(result.Results.First(c => c.Year == year).Rating, expectedRating, $"{nameof(expectedRating)} value was incorrect");
            Assert.AreNotEqual(result.Results.First(c => c.Year == year).IsMeasuredYear, result.Results.First(c => c.Year == year).IsEstimatedYear);
        }






        /// <summary>
        /// Tests a known and pre-calculated set of inputs for a RoRoPassengerShip in the multi-fuel scenario
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="deadweightTonnage"></param>
        /// <param name="grossTonnage"></param>
        /// <param name="typeOfFuel"></param>
        /// <param name="fuelConsumptionInGrams"></param>
        /// <param name="year"></param>
        /// <param name="expectedRating"></param>
        /// <param name="expectedRequiredCii"></param>
        /// <param name="expectedAttainedCii"></param>
        /// <param name="expectedArRatio"></param>
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2019, ImoCiiRating.C, 8.724037653822592, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2020, ImoCiiRating.C, 8.636797277284366, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2021, ImoCiiRating.C, 8.54955690074614, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2022, ImoCiiRating.C, 8.462316524207914, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2023, ImoCiiRating.C, 8.287835771131462, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2024, ImoCiiRating.C, 8.11335501805501, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2025, ImoCiiRating.D, 7.938874264978558, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2026, ImoCiiRating.D, 7.764393511902107, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2027, ImoCiiRating.D, 7.589912758825655, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2028, ImoCiiRating.D, 7.415432005749203, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2029, ImoCiiRating.E, 7.240951252672751, 8.549333333333333)]
        [DataRow(ShipType.BulkCarrier, 25000, 0, TypeOfFuel.DIESEL_OR_GASOIL, 1e+10, 2030, ImoCiiRating.E, 7.0664704995963, 8.549333333333333)]
        [TestMethod]
        public void TestBulkCarrierReturnsExpectedValues(
            ShipType shipType,
            double deadweightTonnage,
            double grossTonnage,
            TypeOfFuel typeOfFuel,
            double fuelConsumptionInGrams,
            int year,
            ImoCiiRating expectedRating,
            double expectedRequiredCii,
            double expectedAttainedCii)
        {
            double expectedArRatio = expectedAttainedCii / expectedRequiredCii;

            var _calc = new ShipCarbonIntensityCalculator();

            var result = _calc.CalculateAttainedCiiRating(
                shipType,
                grossTonnage: grossTonnage,
                deadweightTonnage: deadweightTonnage,
                distanceTravelled: 150000,
                new List<FuelTypeConsumption> {
                    new FuelTypeConsumption
                    {
                        FuelConsumption = fuelConsumptionInGrams,
                        FuelType = typeOfFuel
                    }
                },
                year
                );


            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            System.Diagnostics.Debug.WriteLine(json);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Results.Count(), 12);

            Assert.IsTrue(result.Results.Count(result => result.IsMeasuredYear) == 1);
            Assert.IsTrue(result.Results.Count(result => result.IsEstimatedYear) == 11);

            Assert.AreEqual(result.Results.First(c => c.Year == year).Year, year, $"{nameof(year)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).VectorBoundariesForYear.ShipType, shipType, $"{nameof(shipType)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).RequiredCii, expectedRequiredCii, $"{nameof(expectedRequiredCii)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).AttainedRequiredRatio, expectedArRatio, $"{nameof(expectedArRatio)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).AttainedCii, expectedAttainedCii, $"{nameof(expectedAttainedCii)} value was incorrect");
            Assert.AreEqual(result.Results.First(c => c.Year == year).Rating, expectedRating, $"{nameof(expectedRating)} value was incorrect");
            Assert.AreNotEqual(result.Results.First(c => c.Year == year).IsMeasuredYear, result.Results.First(c => c.Year == year).IsEstimatedYear);
        }
    }
}
