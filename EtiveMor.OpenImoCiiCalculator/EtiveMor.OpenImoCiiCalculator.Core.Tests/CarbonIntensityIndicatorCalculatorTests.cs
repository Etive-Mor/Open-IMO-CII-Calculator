using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;

namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    [TestClass]
    public class CarbonIntensityIndicatorCalculatorTests
    {

        /// <summary>
        /// This test checks that the GetAttainedCarbonIntensity method returns the correct ratio 
        /// of massOfCo2Emissions to transportWork.
        /// </summary>
        /// <param name="massOfCo2Emissions"></param>
        /// <param name="transportWork"></param>
        /// <param name="expectedRatio"></param>
        [TestMethod]
        [DataRow(1000, 5000, 0.2)]
        [DataRow(5000, 10000, 0.5)]
        public void GetAttainedCarbonIntensity_Success_ReturnsCorrectRatio(double massOfCo2Emissions, double transportWork, double expectedRatio)
        {
            var calculator = new CarbonIntensityIndicatorCalculatorService();
            double result = calculator.GetAttainedCarbonIntensity(massOfCo2Emissions, transportWork);
            Assert.AreEqual(expectedRatio, result, 0.001); // Using a tolerance of 0.001 for floating point comparison
        }

        /// <summary>
        /// This test checks that the GetAttainedCarbonIntensity method throws an ArgumentOutOfRangeException when
        /// he massOfCo2Emissions or transportWork is less than or equal to zero.
        /// </summary>
        /// <param name="massOfCo2Emissions"></param>
        /// <param name="transportWork"></param>
        [TestMethod]
        [DataRow(0, 1000)]
        [DataRow(-100, 5000)]
        [DataRow(1000, 0)]
        [DataRow(5000, -200)]
        public void GetAttainedCarbonIntensity_Failure_ThrowsArgumentOutOfRangeException(double massOfCo2Emissions, double transportWork)
        {
            var calculator = new CarbonIntensityIndicatorCalculatorService();

            ArgumentOutOfRangeException ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => calculator.GetAttainedCarbonIntensity(massOfCo2Emissions, transportWork));
            Assert.IsTrue(!string.IsNullOrEmpty(ex.Message));
        }
    }
}
