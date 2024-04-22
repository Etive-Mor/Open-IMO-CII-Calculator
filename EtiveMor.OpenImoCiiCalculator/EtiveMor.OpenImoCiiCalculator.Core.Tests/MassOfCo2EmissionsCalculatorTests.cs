using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;

namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    [TestClass]
    public class MassOfCo2EmissionsCalculatorTests
    {

        /// <summary>
        /// Tests that when 1000 grams of fuel of a given type is consumed, the mass of CO2 emissions 
        /// is calculated correctly.
        /// 
        /// The expeced result is 1000 x the MEPC.364(79) conversion factor for the given fuel type.
        /// </summary>
        /// <param name="fuelType"></param>
        /// <param name="expectedResult"></param>
        /// <remarks>
        /// Emissions mass conversion factors are outlined in IMO MEPC.364(79)
        /// 
        /// https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf
        /// </remarks>
        [TestMethod]
        [DataRow(TypeOfFuel.DIESEL_OR_GASOIL, 3206)]
        [DataRow(TypeOfFuel.LIGHTFUELOIL, 3151)]
        [DataRow(TypeOfFuel.HEAVYFUELOIL, 3114)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE, 3000)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE, 3030)]
        [DataRow(TypeOfFuel.ETHANE, 2927)]
        [DataRow(TypeOfFuel.LIQUIFIEDNATURALGAS, 2750)]
        [DataRow(TypeOfFuel.METHANOL, 1375)]
        [DataRow(TypeOfFuel.ETHANOL, 1913)]
        public void TestGetMassOfCo2Emissions(TypeOfFuel fuelType, double expectedResult)
        {
            // Arrange, set the  fuel consumption value to 1000 grams
            var fuelConsumption = 1000;

            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService();

            // Act
            var result = fuelCalculation.GetMassOfCo2Emissions(fuelType, fuelConsumption);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }


        /// <summary>
        /// Tests that, when passed an unsupported fuel type, an ArgumentException is thrown.
        /// </summary>
        [TestMethod]
        [DataRow(TypeOfFuel.UNKNOWN)]
        [DataRow(TypeOfFuel.OTHER)]
        public void TestGetMassOfCo2EmissionsThrowsExceptionOnInvalidFuel(TypeOfFuel fuelType)
        {
            // Arrange, set the  fuel consumption value to 1000 grams
            var fuelConsumption = 1000;

            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fuelCalculation.GetMassOfCo2Emissions(fuelType, fuelConsumption));
        }


        /// <summary>
        /// Tests that, when passed a negative value for fuel consumption, an ArgumentOutOfRangeException is thrown.
        /// </summary>
        [TestMethod]
        [DataRow(TypeOfFuel.DIESEL_OR_GASOIL)]
        [DataRow(TypeOfFuel.LIGHTFUELOIL)]
        [DataRow(TypeOfFuel.HEAVYFUELOIL)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE)]
        [DataRow(TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE)]
        [DataRow(TypeOfFuel.ETHANE)]
        [DataRow(TypeOfFuel.LIQUIFIEDNATURALGAS)]
        [DataRow(TypeOfFuel.METHANOL)]
        [DataRow(TypeOfFuel.ETHANOL)]
        [DataRow(TypeOfFuel.UNKNOWN)]
        [DataRow(TypeOfFuel.OTHER)]
        public void TestGetMassOfCo2EmissionsThrowsExceptionOnNegativeConsumptionValue(TypeOfFuel fuelType)
        {
            // Arrange, set the  fuel consumption value to -100 grams (an invalid number)
            var fuelConsumption = -100;

            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => fuelCalculation.GetMassOfCo2Emissions(fuelType, fuelConsumption));
        }



        /// <summary>
        /// Tests that the mass conversion factor for a given fuel type is returned correctly according to MEPC.364(79)
        /// </summary>
        /// <param name="fuelType"></param>
        /// <param name="expectedResult"></param>
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
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService(); 

            // Act
            var result = fuelCalculation.GetFuelMassConversionFactor(fuelType);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Tests that the mass conversion factor throws an ArgumentException when an unsupported fuel type is provided.
        /// </summary>
        [TestMethod]
        [DataRow(TypeOfFuel.UNKNOWN)]
        [DataRow(TypeOfFuel.OTHER)]
        public void TestGetFuelMassConversionFactor_UnsupportedFuelType_ThrowsArgumentException(TypeOfFuel fuelType)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService(); 

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fuelCalculation.GetFuelMassConversionFactor(fuelType));
        }


        /// <summary>
        /// Tests that the carbon content for a given fuel type is returned correctly according to MEPC.364(79)
        /// </summary>
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
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService();

            // Act
            var result = fuelCalculation.GetFuelCarbonContent(fuelType);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Tests that the carbon content throws an ArgumentException when an unsupported fuel type is provided.
        /// </summary>
        [TestMethod]
        [DataRow(TypeOfFuel.UNKNOWN)]
        [DataRow(TypeOfFuel.OTHER)]
        public void TestGetFuelCarbonContent_UnsupportedFuelType_ThrowsArgumentException(TypeOfFuel fuelType)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fuelCalculation.GetFuelCarbonContent(fuelType));
        }



        /// <summary>
        /// Tests that the lower calorific value for a given fuel type is returned correctly according to MEPC.364(79)
        /// </summary>
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
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService();

            // Act
            var result = fuelCalculation.GetFuelLowerCalorificValue(fuelType);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Tests that the lower calorific value throws an ArgumentException when an unsupported fuel type is provided.
        /// </summary>
        [TestMethod]
        [DataRow(TypeOfFuel.UNKNOWN)] 
        [DataRow(TypeOfFuel.OTHER)] 
        public void TestGetFuelLowerCalorificValue_UnsupportedFuelType_ThrowsArgumentException(TypeOfFuel fuelType)
        {
            // Arrange
            var fuelCalculation = new ShipMassOfCo2EmissionsCalculatorService();

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => fuelCalculation.GetFuelLowerCalorificValue(fuelType));
        }
    }
}
