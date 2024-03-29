using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Models.ShipModels;
using EtiveMor.OpenImoCiiCalculator.Core.Services.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    [TestClass]
    public class RatingBoundariesServiceTests
    {

        /// <summary>
        /// This method tests that ShipType enum values are considered by the
        /// GetBoundaries method. If a new value is added to the Enum, this method
        /// will fail until the RatingsBoundariesService is updated to handle the new value.
        /// </summary>
        [TestMethod]
        public void TestGetBoundariesProcessesAllEnumValues()
        {
            ShipType[] possibleShipTypeEnums = (ShipType[])Enum.GetValues(typeof(ShipType));

            for (int i = 0; i < possibleShipTypeEnums.Length; i++)
            {
                if (possibleShipTypeEnums[i] == ShipType.UNKNOWN)
                {
                    // intentionally ignore the UNKNOWN value
                    continue;
                }
                var ship = new Ship(possibleShipTypeEnums[i], 250000, 0);
                var service = new RatingBoundariesService();
                var boundaries = service.GetBoundaries(ship, 0.5);

                Assert.IsNotNull(boundaries);
            }
        }


        /// <summary>
        /// Method checks that an exception is thrown when an unknown ShipType 
        /// is passed to the GetBoundaries method.
        /// </summary>
        [TestMethod]
        public void TestGetBoundariesFailsOnUnknownShipType()
        {
            var ship = new Ship(ShipType.UNKNOWN, 250000, 0);
            var service = new RatingBoundariesService();

            Assert.ThrowsException<NotSupportedException>(() => service.GetBoundaries(ship, 0.5));
        }


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
            var ship = new Ship(shipType, 250000, 0);
            var service = new RatingBoundariesService();
            var boundaries = service.GetBoundaries(ship, 0.5);

            Assert.AreEqual(expectedCapacityUnit, boundaries.CapacityUnit);
        }
    }
}
