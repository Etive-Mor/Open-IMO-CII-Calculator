using EtiveMor.OpenImoCiiCalculator.Core.Extensions;

namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    [TestClass]
    public class ReductionFactorTests
    {

        [TestMethod]
        public void GetAnnualReductionFactor_2019_ReturnsZero()
        {
            // Arrange
            int year = 2019;
            double expected = 0.00;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2020_ReturnsOnePercent()
        {
            // Arrange
            int year = 2020;
            double expected = 0.01;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2021_ReturnsTwoPercent()
        {
            // Arrange
            int year = 2021;
            double expected = 0.02;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2022_ReturnsThreePercent()
        {
            // Arrange
            int year = 2022;
            double expected = 0.03;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2023_ReturnsFivePercent()
        {
            // Arrange
            int year = 2023;
            double expected = 0.05;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2024_ReturnsSevenPercent()
        {
            // Arrange
            int year = 2024;
            double expected = 0.07;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2025_ReturnsNinePercent()
        {
            // Arrange
            int year = 2025;
            double expected = 0.09;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2026_ReturnsElevenPercent()
        {
            // Arrange
            int year = 2026;
            double expected = 0.11;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2027_ReturnsThirteenPercent()
        {
            // Arrange
            int year = 2027;
            double expected = 0.13;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2028_ReturnsFifteenPercent()
        {
            // Arrange
            int year = 2028;
            double expected = 0.15;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2029_ReturnsSeventeenPercent()
        {
            // Arrange
            int year = 2029;
            double expected = 0.17;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAnnualReductionFactor_2030_ReturnsNineteenPercent()
        {
            // Arrange
            int year = 2030;
            double expected = 0.19;

            // Act
            double result = year.GetAnnualReductionFactor();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void GetAnnualReductionFactor_UnsupportedYear_ThrowsException()
        {
            // Arrange
            int year = 2018;

            // Act & Assert
            year.GetAnnualReductionFactor();
        }


        [TestMethod]
        public void ApplyAnnualReductionFactor_2018_ThrowsException()
        {
            // Arrange
            double value = 100.0;
            int year = 2018;

            // Act & Assert
            Assert.ThrowsException<NotSupportedException>(() => value.ApplyAnnualReductionFactor(year));
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2019_ReturnsOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2019;
            double expected = 100.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2020_Returns99PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2020;
            double expected = 99.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2021_Returns98PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2021;
            double expected = 98.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2022_Returns97PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2022;
            double expected = 97.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2023_Returns95PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2023;
            double expected = 95.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2024_Returns93PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2024;
            double expected = 93.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2025_Returns91PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2025;
            double expected = 91.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2026_Returns89PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2026;
            double expected = 89.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2027_Returns87PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2027;
            double expected = 87.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2028_Returns85PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2028;
            double expected = 85.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2029_Returns83PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2029;
            double expected = 83.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }

        [TestMethod]
        public void ApplyAnnualReductionFactor_2030_Returns81PercentOfOriginalValue()
        {
            // Arrange
            double value = 100.0;
            int year = 2030;
            double expected = 81.0;

            // Act
            double result = value.ApplyAnnualReductionFactor(year);

            // Assert
            Assert.AreEqual(expected, result, 0.01);
        }
    }
}
