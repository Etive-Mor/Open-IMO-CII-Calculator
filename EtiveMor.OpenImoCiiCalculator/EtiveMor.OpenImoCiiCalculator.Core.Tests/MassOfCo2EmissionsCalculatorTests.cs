using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    [TestClass]
    public class MassOfCo2EmissionsCalculatorTests
    {
        [TestMethod]
        [DataRow(TypeOfFuel.DIESEL_OR_GASOIL, 3.206)]
        [DataRow(TypeOfFuel.LIGHTFUELOIL, 3.151)]
        [DataRow(TypeOfFuel.HEAVYFUELOIL, 3.114)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE, 3.000)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE, 3.030)]
        [DataRow(TypeOfFuel.ETHANE, 2.927)]
        [DataRow(TypeOfFuel.LIQUIFIEDNATURALGAS, 2.750)]
        [DataRow(TypeOfFuel.METHANOL, 1.375)]
        [DataRow(TypeOfFuel.ETHANOL, 1.913)]
        public void TestGetFuelMassConversionFactor_SupportedFuelTypes(TypeOfFuel fuelType, double expectedResult)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculator(); 

            // Act
            var result = fuelCalculation.GetFuelMassConversionFactor(fuelType);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(TypeOfFuel.UNKNOWN)]
        [DataRow(TypeOfFuel.OTHER)]
        public void TestGetFuelMassConversionFactor_UnsupportedFuelType_ThrowsArgumentException(TypeOfFuel fuelType)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculator(); 

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fuelCalculation.GetFuelMassConversionFactor(fuelType));
        }



        [TestMethod]
        [DataRow(TypeOfFuel.DIESEL_OR_GASOIL, 0.8744)]
        [DataRow(TypeOfFuel.LIGHTFUELOIL, 0.8594)]
        [DataRow(TypeOfFuel.HEAVYFUELOIL, 0.8493)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE, 0.8182)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE, 0.8264)]
        [DataRow(TypeOfFuel.ETHANE, 0.7989)]
        [DataRow(TypeOfFuel.LIQUIFIEDNATURALGAS, 0.7500)]
        [DataRow(TypeOfFuel.METHANOL, 0.3750)]
        [DataRow(TypeOfFuel.ETHANOL, 0.5217)]
        public void TestGetFuelCarbonContent_SupportedFuelTypes(TypeOfFuel fuelType, double expectedResult)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculator();

            // Act
            var result = fuelCalculation.GetFuelCarbonContent(fuelType);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(TypeOfFuel.UNKNOWN)]
        [DataRow(TypeOfFuel.OTHER)]
        public void TestGetFuelCarbonContent_UnsupportedFuelType_ThrowsArgumentException(TypeOfFuel fuelType)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculator();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fuelCalculation.GetFuelCarbonContent(fuelType));
        }




        [TestMethod]
        [DataRow(TypeOfFuel.DIESEL_OR_GASOIL, 42700)]
        [DataRow(TypeOfFuel.LIGHTFUELOIL, 41200)]
        [DataRow(TypeOfFuel.HEAVYFUELOIL, 40200)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE, 46300)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE, 45700)]
        [DataRow(TypeOfFuel.ETHANE, 46400)]
        [DataRow(TypeOfFuel.LIQUIFIEDNATURALGAS, 48000)]
        [DataRow(TypeOfFuel.METHANOL, 19900)]
        [DataRow(TypeOfFuel.ETHANOL, 26800)]
        public void TestGetFuelLowerCalorificValue_SupportedFuelTypes(TypeOfFuel fuelType, double expectedResult)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculator();

            // Act
            var result = fuelCalculation.GetFuelLowerCalorificValue(fuelType);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(TypeOfFuel.UNKNOWN)] 
        [DataRow(TypeOfFuel.OTHER)] 
        public void TestGetFuelLowerCalorificValue_UnsupportedFuelType_ThrowsArgumentException(TypeOfFuel fuelType)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculator();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fuelCalculation.GetFuelLowerCalorificValue(fuelType));
        }
    }
}
