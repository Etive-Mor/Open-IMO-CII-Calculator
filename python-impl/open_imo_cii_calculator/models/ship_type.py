from enum import IntEnum

class ShipType(IntEnum):
    """
    An enum describing the possible ship types outlined in MEPC 337(76)
    """
    UNKNOWN = 0
    
    # A type of ship designed to carry unpackaged bulk cargo (e.g., grain, coal, ore) in its cargo holds.
    BULK_CARRIER = 10
    
    # A type of ship designed to transport gases in specially designed tanks.
    GAS_CARRIER = 20
    
    # A type of ship designed to carry petroleum products or other liquid cargoes in bulk.
    TANKER = 30
    
    # A type of ship designed to carry containers, which are standardized boxes used for transporting cargo.
    CONTAINER_SHIP = 40
    
    # A type of ship designed to carry a variety of packaged goods, including manufactured products, food, and raw materials.
    GENERAL_CARGO_SHIP = 50
    
    # A type of ship designed to carry multiple types of cargo, such as bulk cargo, containers, and general cargo.
    COMBINATION_CARRIER = 60
    
    # A type of ship designed to carry refrigerated cargo, such as perishable food items or temperature-sensitive goods.
    REFRIGERATED_CARGO_CARRIER = 70
    
    # A type of ship designed to transport liquefied natural gas (LNG) in specially designed tanks.
    LNG_CARRIER = 80
    
    # A type of ship designed to carry wheeled cargo, such as cars, trucks, or trailers, that can be driven on and off the ship using built-in ramps.
    RORO_CARGO_SHIP_VEHICLE_CARRIER = 90
    
    # A type of ship designed to carry wheeled cargo using built-in ramps for loading and unloading vehicles.
    RORO_CARGO_SHIP = 100
    
    # A type of ship designed to carry both wheeled cargo and passengers, with built-in ramps for loading and unloading vehicles.
    RORO_PASSENGER_SHIP = 110
    
    # A type of high-speed ship designed to conform to SOLAS Chapter X standards
    RORO_PASSENGER_SHIP_HIGH_SPEED_SOLAS = 111
    
    # A type of ship designed primarily for passenger accommodation and leisure activities, often including amenities such as restaurants, entertainment venues, and recreational facilities.
    CRUISE_PASSENGER_SHIP = 120