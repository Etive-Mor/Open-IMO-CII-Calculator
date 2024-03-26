namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    [TestClass]
    public class ShipTransportWorkCalculatorTests
    {

        [TestMethod]
        public void TestGetShipTransportWork()
        {
            var calculator = new ShipTransportWorkCalculator();
            var capacity = 250000;
            var distanceSailed = 1000;

            var transportWork = calculator.GetShipTransportWork(capacity, distanceSailed);

            Assert.AreEqual(250000000, transportWork);
        }
    }
}
