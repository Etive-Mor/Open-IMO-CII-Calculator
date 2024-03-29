using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;

namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    [TestClass]
    public class RatingBoundariesServiceTests
    {

        [DataRow(ShipType.GasCarrier, 65000)]
        [DataRow(ShipType.LngCarrier, 100000)]
        /// <summary>
        /// Tests that a ship with a weight capacity difference in MEPC354(78) has different boundaries 
        /// returned before and after the boundary.
        /// 
        /// For exmaple, that GasCarrirs with a deadweight tonnage of 65000 or more
        /// have different boundaries to GasCarriers with a deadweight tonnage of 64999 or less.
        /// </summary>
        [TestMethod] 
        public void TestShipWithCapacityDifferencesHasDifferentBoundariesReturnedBeforeAndAfterBoundary(
            ShipType shipType, double boundaryTonnage
            )
        {     
            var smallShip = new Ship(shipType, boundaryTonnage -1, 0);
            var largeShip = new Ship(shipType, boundaryTonnage, 0);

            var service = new RatingBoundariesService();

            var smallBoundaries = service.GetBoundaries(smallShip, 0.5, 2023);
            var largeBoundaries = service.GetBoundaries(largeShip, 0.5, 2023);

            Assert.AreNotEqual(smallBoundaries.BoundaryDdVectors, largeBoundaries.BoundaryDdVectors);

            Assert.AreNotEqual(
                smallBoundaries.BoundaryDdVectors[ImoCiiBoundary.Inferior],
                largeBoundaries.BoundaryDdVectors[ImoCiiBoundary.Inferior]);


            Assert.AreNotEqual(
                smallBoundaries.BoundaryDdVectors[ImoCiiBoundary.Upper],
                largeBoundaries.BoundaryDdVectors[ImoCiiBoundary.Upper]);

            Assert.AreNotEqual(
                   smallBoundaries.BoundaryDdVectors[ImoCiiBoundary.Lower],
                   largeBoundaries.BoundaryDdVectors[ImoCiiBoundary.Lower]);
            
            Assert.AreNotEqual(
              smallBoundaries.BoundaryDdVectors[ImoCiiBoundary.Superior],
              largeBoundaries.BoundaryDdVectors[ImoCiiBoundary.Superior]);
        }


        [DataRow(2019)]
        [DataRow(2020)]
        [DataRow(2021)]
        [DataRow(2022)]
        [DataRow(2023)]
        [DataRow(2024)]
        [DataRow(2025)]
        [DataRow(2026)]
        [DataRow(2027)]
        [DataRow(2028)]
        [DataRow(2029)]
        [DataRow(2030)]
        /// <summary>
        /// This method tests that ShipType enum values are considered by the
        /// GetBoundaries method. If a new value is added to the Enum, this method
        /// will fail until the RatingsBoundariesService is updated to handle the new value.
        /// </summary>
        [TestMethod]
        public void TestGetBoundariesProcessesAllEnumValues(int year)
        {
            ShipType[] possibleShipTypeEnums = (ShipType[])Enum.GetValues(typeof(ShipType));

            for (int i = 0; i < possibleShipTypeEnums.Length; i++)
            {
                if (possibleShipTypeEnums[i] == ShipType.UNKNOWN)
                {
                    // intentionally ignore the UNKNOWN value
                    continue;
                }
                var ship = new Ship(possibleShipTypeEnums[i], 250000, 250000);
                var service = new RatingBoundariesService();
                var boundaries = service.GetBoundaries(ship, 0.5, year);

                Assert.IsNotNull(boundaries);
            }
        }

        [DataRow(2019)]
        [DataRow(2020)]
        [DataRow(2021)]
        [DataRow(2022)]
        [DataRow(2023)]
        [DataRow(2024)]
        [DataRow(2025)]
        [DataRow(2026)]
        [DataRow(2027)]
        [DataRow(2028)]
        [DataRow(2029)]
        [DataRow(2030)]
        /// <summary>
        /// Method checks that an exception is thrown when an unknown ShipType 
        /// is passed to the GetBoundaries method.
        /// </summary>
        [TestMethod]
        public void TestGetBoundariesFailsOnUnknownShipType(int year)
        {
            var ship = new Ship(ShipType.UNKNOWN, 250000, 0);
            var service = new RatingBoundariesService();

            Assert.ThrowsException<NotSupportedException>(() => service.GetBoundaries(ship, 0.5, year));
        }


        /// <summary>
        /// This method tests that the CapacityUnit is correctly set for each ShipType.
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="expectedCapacityUnit"></param>
        [DataRow(ShipType.BulkCarrier, CapacityUnit.DWT)]
        [DataRow(ShipType.GasCarrier, CapacityUnit.DWT)]
        [DataRow(ShipType.Tanker, CapacityUnit.DWT)]
        [DataRow(ShipType.ContainerShip, CapacityUnit.DWT)]
        [DataRow(ShipType.GeneralCargoShip, CapacityUnit.DWT)]
        [DataRow(ShipType.RefrigeratedCargoCarrier, CapacityUnit.DWT)]
        [DataRow(ShipType.CombinationCarrier, CapacityUnit.DWT)]
        [DataRow(ShipType.LngCarrier, CapacityUnit.DWT)]
        [DataRow(ShipType.RoRoCargoShipVehicleCarrier, CapacityUnit.GT)]
        [DataRow(ShipType.RoRoPassengerShip, CapacityUnit.GT)]
        [DataRow(ShipType.CruisePassengerShip, CapacityUnit.GT)]
        [TestMethod]
        public void TestGrossTonnageCapacityShipTypesAreHandledCorrectly(ShipType shipType, CapacityUnit expectedCapacityUnit)
        {

            var ship = new Ship(shipType, 
                expectedCapacityUnit == CapacityUnit.DWT ? 250000 : 0,
                expectedCapacityUnit == CapacityUnit.GT ?  250000 : 0);
            var service = new RatingBoundariesService();
            var boundaries = service.GetBoundaries(ship, 0.5, 2019);

            Assert.AreEqual(expectedCapacityUnit, boundaries.CapacityUnit);
        }



        /// <summary>
        /// this method checks that RatingBoundariesService verifies the ship's tonnage is 
        /// correctly set 
        /// 
        /// It ensures that DeadweightTonnage is set for all ship types with a DWT Capacity type, and
        /// gross tonnage is set for all ship types with a GT Capacity type
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="deadweightTonnage"></param>
        /// <param name="grossTonnage"></param>
        [TestMethod]
        [DataRow(ShipType.BulkCarrier, 1, 0)]
        [DataRow(ShipType.GasCarrier, 2, 0)]
        [DataRow(ShipType.Tanker, 3, 0)]
        [DataRow(ShipType.ContainerShip, 4, 0)]
        [DataRow(ShipType.GeneralCargoShip, 5, 0)]
        [DataRow(ShipType.RefrigeratedCargoCarrier, 6, 0)]
        [DataRow(ShipType.CombinationCarrier, 7, 0)]
        [DataRow(ShipType.LngCarrier, 8, 0)]
        [DataRow(ShipType.RoRoCargoShipVehicleCarrier, 0, 1)]
        [DataRow(ShipType.RoRoCargoShip, 0, 2)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 3)]
        [DataRow(ShipType.RoRoPassengerShip_HighSpeedSOLAS, 0, 4)]
        [DataRow(ShipType.CruisePassengerShip, 0, 5)]
        public void TestValidateShipTonnageValid(ShipType shipType, int deadweightTonnage, int grossTonnage)
        {
            var ship = new Ship(shipType, deadweightTonnage, grossTonnage);
            var service = new RatingBoundariesService();
            var boundaries = service.GetBoundaries(ship, 0.5, 2030);
        }
    }
}
