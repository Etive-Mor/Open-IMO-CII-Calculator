using EtiveMor.OpenImoCiiCalculator.Core.Models;
using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Tests
{
    /// <summary>
    /// Test the CalculateCapacity method for BulkCarrier ship type with valid deadweight tonnage and zero gross tonnage.
    /// </summary>
    [TestClass]
    public class ShipCapacityTests
    {

        /// <summary>
        /// Test the ValidateTonnageParamsSet method with various ship types and invalid deadweight or gross tonnage values,
        /// expecting ArgumentOutOfRangeException to be thrown.
        /// </summary>
        /// <param name="shipType">The ship type to test</param>
        /// <param name="deadweightTonnage">The deadweight tonnage value to test</param>
        /// <param name="grossTonnage">The gross tonnage value to test</param>
        [TestMethod]
        public void TestCalculateCapacity_BulkCarrier()
        {
            var ship = new Ship
            {
                ShipType = ShipType.BulkCarrier,
                DeadweightTonnage = 250000,
                GrossTonnage = 0
            };

            var capacity = new ShipCapacityCalculator().GetShipCapacity(ship);

            Assert.AreEqual(250000, capacity);
        }



        /// <summary>
        /// Test the CalculateCapacity method with various ship types, deadweight tonnage, gross tonnage values,
        /// and their expected capacity values.
        /// </summary>
        /// <param name="shipType">The ship type to test</param>
        /// <param name="deadweightTonnage">The deadweight tonnage value to test</param>
        /// <param name="grossTonnage">The gross tonnage value to test</param>
        /// <param name="expectedCapacity">The expected capacity value for the given ship type and tonnage values</param>
        [DataTestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(ShipType.BulkCarrier, 0, 100000)]
        [DataRow(ShipType.GasCarrier, 0, 100000)]
        [DataRow(ShipType.Tanker, 0, 100000)]
        [DataRow(ShipType.ContainerShip, 0, 100000)]
        [DataRow(ShipType.GeneralCargoShip, 0, 100000)]
        [DataRow(ShipType.RefrigeratedCargoCarrier, 0, 100000)]
        [DataRow(ShipType.CombinationCarrier, 0, 100000)]
        [DataRow(ShipType.LngCarrier, 0, 100000)]
        [DataRow(ShipType.RoRoCargoShipVehicleCarrier, 250000, 0)]
        [DataRow(ShipType.RoRoPassengerShip, 250000, 0)]
        [DataRow(ShipType.RoRoCruisePassengerShip, 250000, 0)]
        public void TestValidateTonnageParamsSet_ArgumentOutOfRangeException(ShipType shipType, double deadweightTonnage, double grossTonnage)
        {
            var ship = new Ship
            {
                ShipType = shipType,
                DeadweightTonnage = deadweightTonnage,
                GrossTonnage = grossTonnage
            };

            new ShipCapacityCalculator().GetShipCapacity(ship);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="deadweightTonnage"></param>
        /// <param name="grossTonnage"></param>
        /// <param name="expectedCapacity"></param>
        [DataTestMethod]
        [DataRow(ShipType.BulkCarrier, 250000, 0, 250000)]
        [DataRow(ShipType.BulkCarrier, 300000, 0, 279000)]
        [DataRow(ShipType.GasCarrier, 250000, 0, 250000)]
        [DataRow(ShipType.Tanker, 250000, 0, 250000)]
        [DataRow(ShipType.ContainerShip, 250000, 0, 250000)]
        [DataRow(ShipType.GeneralCargoShip, 250000, 0, 250000)]
        [DataRow(ShipType.RefrigeratedCargoCarrier, 250000, 0, 250000)]
        [DataRow(ShipType.CombinationCarrier, 250000, 0, 250000)]
        [DataRow(ShipType.LngCarrier, 250000, 0, 250000)]
        [DataRow(ShipType.RoRoCargoShip, 250000, 0, 250000)]
        [DataRow(ShipType.RoRoCargoShipVehicleCarrier, 0, 100000, 100000)]
        [DataRow(ShipType.RoRoPassengerShip, 0, 100000, 100000)]
        [DataRow(ShipType.RoRoCruisePassengerShip, 0, 100000, 100000)]
        public void TestCalculateCapacity(ShipType shipType, double deadweightTonnage, double grossTonnage, double expectedCapacity)
        {
            var ship = new Ship
            {
                ShipType = shipType,
                DeadweightTonnage = deadweightTonnage,
                GrossTonnage = grossTonnage
            };

            var capacity = new ShipCapacityCalculator().GetShipCapacity(ship);

            Assert.AreEqual(expectedCapacity, capacity);
        }
    }
}
