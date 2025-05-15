from enum import IntEnum

class ImoCiiBoundary(IntEnum):
    """
    An enum describing the possible IMO Carbon Intensity Indicator (CII) boundaries
    used to determine ship ratings
    """
    SUPERIOR = 1
    LOWER = 2
    UPPER = 3
    INFERIOR = 4