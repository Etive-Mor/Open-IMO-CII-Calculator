using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EtiveMor.OpenImoCiiCalculator.Core
{
    public class Calculator
    {
        /// <summary>
        /// 
        /// 
        /// 
        /// CII = (annualFuelConsumption * co2eqEmissionsFactor) / (distanceSailed * capacity)
        /// </summary>
        /// <param name="annualFuelConsumption"></param>
        /// <param name="co2eqEmissionsFactor"></param>
        /// <param name="distanceSailed"></param>
        /// <param name="capacity"></param>
        /// <param name="deadweightTonnage"></param>
        /// <param name="shipType"></param>
        /// <returns></returns>
        public ImoCiiRating CalculateImoCiiRating(double annualFuelConsumption, double co2eqEmissionsFactor, double distanceSailed, double capacity, double deadweightTonnage, double grossTonnage, ShipType shipType)
        {
            double massOfCo2Emissions = annualFuelConsumption * co2eqEmissionsFactor;
            double transportWork = annualFuelConsumption * co2eqEmissionsFactor;
            var cii = massOfCo2Emissions / transportWork;

            return ImoCiiRating.ERR;
        }



        /// <summary>
        /// Gets the ships capacity according to the MEPC 337(76) guidelines
        /// 
        /// 
        /// <seealso href="https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.337(76).pdf"/>
        /// </summary>
        /// <param name="shipType"></param>
        /// <param name="deadweightTonnage"></param>
        /// <param name="grossTonnage"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the ship type was not supported
        /// </exception>
        public double GetShipCapacity(ShipType shipType, double deadweightTonnage, double grossTonnage)
        {
            ValidateTonnageParamsSet(shipType, deadweightTonnage, grossTonnage);

            return shipType switch
            {
                ShipType.BulkCarrier => Math.Min(deadweightTonnage, 279000),
                ShipType.GasCarrier => deadweightTonnage,
                ShipType.Tanker => deadweightTonnage,
                ShipType.ContainerShip => deadweightTonnage,
                ShipType.GeneralCargoShip => deadweightTonnage,
                ShipType.RefrigeratedCargoCarrier => deadweightTonnage,
                ShipType.CombinationCarrier => deadweightTonnage,
                ShipType.LngCarrier => Math.Max(deadweightTonnage, 65000),
                ShipType.RoRoCargoShipVehicleCarrier => grossTonnage,
                ShipType.RoRoCargoShip => deadweightTonnage,
                ShipType.RoRoPassengerShip => grossTonnage,
                ShipType.RoRoCruisePassengerShip => grossTonnage,
                _ => throw new ArgumentOutOfRangeException(nameof(shipType), shipType, $"Unsupported {nameof(shipType)}: {shipType}")
            };
        }


        private void ValidateTonnageParamsSet(ShipType shipType, double deadweightTonnage, double grossTonnage)
        {
            _ = shipType switch
            {
                ShipType.BulkCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.GasCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.Tanker => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.ContainerShip => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.GeneralCargoShip => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.RefrigeratedCargoCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.CombinationCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.LngCarrier => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.RoRoCargoShipVehicleCarrier => ValidateTonnage(grossTonnage, nameof(grossTonnage), shipType) ? grossTonnage : throw new InvalidOperationException(),
                ShipType.RoRoCargoShip => ValidateTonnage(deadweightTonnage, nameof(deadweightTonnage), shipType) ? deadweightTonnage : throw new InvalidOperationException(),
                ShipType.RoRoPassengerShip => ValidateTonnage(grossTonnage, nameof(grossTonnage), shipType) ? grossTonnage : throw new InvalidOperationException(),
                ShipType.RoRoCruisePassengerShip => ValidateTonnage(grossTonnage, nameof(grossTonnage), shipType) ? grossTonnage : throw new InvalidOperationException(),
                _ => throw new ArgumentOutOfRangeException(nameof(shipType), shipType, $"Unsupported {nameof(shipType)}: {shipType}")
            };
        }

        /// <summary>
        /// Validates that the tonnage param is greater than 0 
        /// 
        /// </summary>
        /// <param name="tonnage">The tonnage value (accepts either gross tonnage or deadweight)</param>
        /// <param name="tonnageName">The ship's tonnage name (accepts either "gross" or "deadweight"</param>
        /// <param name="shipType">The ship type <seealso cref="ShipType"/></param>
        /// <returns>
        /// true if the tonnage is greater than 0
        /// throws an exception if the tonnage is less than or equal to 0
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is equal or lower than 0</exception>
        bool ValidateTonnage(double tonnage, string tonnageName, ShipType shipType)
        {
            if (tonnage <= 0)
            {
                throw new ArgumentOutOfRangeException(tonnageName, tonnage, $"{tonnageName} must be greater than 0 if {nameof(shipType)} is set to {shipType} ");
            }
            return true;
        }






        /// <summary>
        /// Calculates the mass of CO2 emissions from a ship, given the mass of CO2eq emissions, and the transport work undertaken by the ship in a full calendar year.
        /// 
        /// 
        /// </summary>
        /// <param name="massOfCo2Emissions"></param>
        /// <param name="transportWork"></param>
        /// <returns></returns>
        public ImoCiiRating CalculateImoCiiRating(decimal massOfCo2Emissions, decimal transportWork)
        {
            double cii = (double)(massOfCo2Emissions / transportWork);

            if (cii < 0.0001)
            {
                return ImoCiiRating.A;
            }
            else if (cii >= 0.0001 && cii < 0.0002)
            {
                return ImoCiiRating.B;
            }
            else if (cii >= 0.0002 && cii < 0.0003)
            {
                return ImoCiiRating.C;
            }
            else if (cii >= 0.0003 && cii < 0.0004)
            {
                return ImoCiiRating.D;
            }
            else if (cii >= 0.0004)
            {
                return ImoCiiRating.E;
            }
            else
            {
                return ImoCiiRating.ERR;
            }
        }

        /// <summary>
        /// Calculates the transport work undertaken by a ship, given its deadweight tonnage and distance sailed in a calendar year.
        /// </summary>
        /// <param name="deadweightTonnage">
        /// The Capacity of the ship in metric Tons
        ///     
        ///     For cargo ships submit the Deadweight Tonnage
        ///     For cruise ships submit the Gross Tonnage
        /// </param>
        /// <param name="distanceSailedCalendarYear">
        /// The distance travelled by the ship across one full calendar year
        /// </param>
        /// <returns></returns>
        public decimal CalculateTransportWork(decimal deadweightTonnage, decimal distanceSailedCalendarYear, ShipType shipType)
        {
            return deadweightTonnage * distanceSailedCalendarYear;
        }


        
    }

    /// <summary>
    /// An enum describing the possible IMO Carbon Intensity Indicator (CII) ratings
    /// 
    /// 0 indicates an error
    /// 
    /// A indicates the best rating
    /// B indicates the second best rating
    /// C indicates the third best rating
    /// D indicates the fourth best rating
    /// E indicates the worst rating
    /// 
    /// </summary>
    public enum ImoCiiRating
    {
        ERR = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
    }

    /// <summary>
    /// An enum describing the possible ship types outlined in MEPC 337(76)
    /// </summary>
    public enum ShipType
    {
        UNKNOWN = 0,
        BulkCarrier = 10,
        GasCarrier = 20,
        Tanker = 30,
        ContainerShip = 40,
        GeneralCargoShip = 50,
        CombinationCarrier = 60,
        RefrigeratedCargoCarrier = 70,
        LngCarrier = 80,
        RoRoCargoShipVehicleCarrier = 90,
        RoRoCargoShip = 100,
        RoRoPassengerShip = 110,
        RoRoCruisePassengerShip = 120
    }


    /// <summary>
    /// An enum describing the possible types of ship
    /// </summary>
    //public enum ShipType
    //{
    //    BulkCarrier = 1,
    //    BulkCarrier_LT_279k_DWT = 2,
    //    GasCarrier = 3,
    //    GasCarrier_LT_65k_DWT = 4,
    //    Tanker = 5,
    //    ContainerShip = 6,
    //    GeneralCargoShip = 7,
    //    GeneralCargoShip_LT_20k_DWT = 8,
    //    RefrigeratedCargoCarrier = 9,
    //    LngCarrier = 10,
    //    LngCarrier_LT_100k_DWT = 11,
    //    LngCarrier_LT_65k_DWT = 12,


    //}

    /// <summary>
    /// An enum describing the possible types of fuel used by ships 
    /// considered by the IMO's Carbon Intensity Indicator (CII) rating system
    /// </summary>
    public enum TypeOfFuel
    {
        UNKNOWN = 0,
        DIESEL_OR_GASOIL = 1,
        LIGHTFUELOIL = 2,
        HEAVYFUELOIL = 3,
        LIQUIFIEDPETROLEUMGAS = 4,
        LIQUIFIEDNATURALGAS = 5,
        OTHER = 6
    }
}
