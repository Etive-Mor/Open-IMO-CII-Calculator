from enum import IntEnum

class CapacityUnit(IntEnum):
    """
    An enum describing the units of capacity measurement for ships
    """
    ERR = 0
    DWT = 1
    DWT_CAP_HIGH = 2
    GT = 3
    GT_CAP_LOW = 4