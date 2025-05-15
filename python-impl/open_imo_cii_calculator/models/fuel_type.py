from enum import IntEnum

class TypeOfFuel(IntEnum):
    """
    An enum describing the possible types of fuel used by ships 
    considered by the IMO's Carbon Intensity Indicator (CII) rating system
    """
    UNKNOWN = 0
    DIESEL_OR_GASOIL = 10
    LIGHT_FUEL_OIL = 20
    HEAVY_FUEL_OIL = 30
    LIQUIFIED_PETROLEUM_PROPANE = 40
    LIQUIFIED_PETROLEUM_BUTANE = 50
    ETHANE = 60
    LIQUIFIED_NATURAL_GAS = 70
    METHANOL = 80
    ETHANOL = 90
    OTHER = 100