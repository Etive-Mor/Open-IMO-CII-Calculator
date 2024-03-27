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
                ShipType.BulkCarrier, 
                1000, 
                1000, 
                1000, 
                TypeOfFuel.HEAVYFUELOIL, 
                1000);

            Assert.AreNotEqual(ImoCiiRating.ERR, result);
        }

    }
}
