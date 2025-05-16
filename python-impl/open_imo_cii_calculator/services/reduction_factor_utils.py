"""
Utility functions for working with annual reduction factors according to MEPC.338(76)
"""


def get_annual_reduction_factor(year: int) -> float:
    """
    Gets an annual reduction factor for a given year, according to MEPC.338(76)

    Args:
        - year (int): The calendar year being analyzed

    Returns:
        - float: The reduction factor

    Raises:
        - ValueError: If a year outside of the range 2019-2030 (inclusive) is provided
    """
    reduction_factors = {
        2019: 0.00,
        2020: 0.01,
        2021: 0.02,
        2022: 0.03,
        2023: 0.05,
        2024: 0.07,
        2025: 0.09,
        2026: 0.11,
        2027: 0.13,
        2028: 0.15,
        2029: 0.17,
        2030: 0.19
    }
    
    if year not in reduction_factors:
        raise ValueError(f"Year {year} is not supported")
    
    return reduction_factors[year]


def apply_annual_reduction_factor(value: float, year: int) -> float:
    """
    Apply annual reduction factor to a value
    
    Args:
        - value (float): The value to apply the reduction factor to
        - year (int): The calendar year being analyzed
    
    Returns:
        - float: The value with the reduction factor applied
    """
    return value * (1 - get_annual_reduction_factor(year))