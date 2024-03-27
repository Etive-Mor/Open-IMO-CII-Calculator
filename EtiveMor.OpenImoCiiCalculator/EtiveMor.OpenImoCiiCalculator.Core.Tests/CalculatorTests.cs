using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
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
                fuelConsumption: 1.9e+10
                );

            Assert.AreNotEqual(ImoCiiRating.ERR, result);
        }

    }
}
