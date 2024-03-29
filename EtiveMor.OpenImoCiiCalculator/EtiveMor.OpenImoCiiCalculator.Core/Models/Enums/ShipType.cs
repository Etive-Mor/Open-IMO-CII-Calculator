namespace EtiveMor.OpenImoCiiCalculator.Core.Models.Enums
{
    /// <summary>
    /// An enum describing the possible ship types outlined in MEPC 337(76)
    /// </summary>
    public enum ShipType
    {
        /// <summary>
        /// Unknown or unspecified ship type.
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// A type of ship designed to carry unpackaged bulk cargo (e.g., grain, coal, ore) 
        /// in its cargo holds.
        /// </summary>
        BulkCarrier = 10,

        /// <summary>
        /// A type of ship designed to transport gases in specially designed tanks.
        /// </summary>
        /// <seealso cref="LngCarrier"/>
        GasCarrier = 20,

        /// <summary>
        /// A type of ship designed to carry petroleum products or other liquid cargoes 
        /// in bulk.
        /// </summary>
        Tanker = 30,

        /// <summary>
        /// A type of ship designed to carry containers, which are standardized boxes used 
        /// for transporting cargo.
        /// </summary>
        ContainerShip = 40,

        /// <summary>
        /// A type of ship designed to carry a variety of packaged goods, including manufactured 
        /// products, food, and raw materials.
        /// </summary>
        GeneralCargoShip = 50,

        /// <summary>
        /// A type of ship designed to carry multiple types of cargo, such as bulk cargo, 
        /// containers, and general cargo.
        /// </summary>
        CombinationCarrier = 60,

        /// <summary>
        /// A type of ship designed to carry refrigerated cargo, such as perishable food 
        /// items or temperature-sensitive goods.
        /// </summary>
        RefrigeratedCargoCarrier = 70,

        /// <summary>
        /// A type of ship designed to transport liquefied natural gas (LNG) in specially 
        /// designed tanks.
        /// </summary>
        /// <seealso cref="GasCarrier"/>
        LngCarrier = 80,

        /// <summary>
        /// A type of ship designed to carry wheeled cargo, such as cars, trucks, or trailers, 
        /// that can be driven on and off the ship using built-in ramps.
        /// </summary>
        RoRoCargoShipVehicleCarrier = 90,

        /// <summary>
        /// A type of ship designed to carry wheeled cargo using built-in ramps for 
        /// loading and unloading vehicles.
        /// </summary>
        RoRoCargoShip = 100,

        /// <summary>
        /// A type of ship designed to carry both wheeled cargo and passengers, with 
        /// built-in ramps for loading and unloading vehicles.
        /// </summary>
        /// <seealso cref="CruisePassengerShip"/>
        RoRoPassengerShip = 110,
        
        /// <summary>
        /// A type of high-speed ship designed to conform to SOLAS Chapter X standards 
        /// </summary>
        /// <seealso cref="CruisePassengerShip"/>
        RoRoPassengerShip_HighSpeedSOLAS = 111,

        /// <summary>
        /// A type of ship designed primarily for passenger accommodation and leisure 
        /// activities, often including amenities such as restaurants, entertainment 
        /// venues, and recreational facilities.
        /// </summary>
        /// <seealso cref="RoRoPassengerShip"/>
        CruisePassengerShip = 120
    }

}
