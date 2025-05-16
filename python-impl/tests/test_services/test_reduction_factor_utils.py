import pytest
from open_imo_cii_calculator.services.reduction_factor_utils import get_annual_reduction_factor, apply_annual_reduction_factor

def test_get_annual_reduction_factor_all_years():
    """
    Test annual reduction factor for all valid years.

    Verifies:
    - get_annual_reduction_factor returns a non-negative factor for each year in the valid range (2019-2030).
    - Ensures reduction factors are defined for all expected years.
    """
    for year in range(2019, 2031):
        factor = get_annual_reduction_factor(year)
        assert factor >= 0

def test_get_annual_reduction_factor_out_of_range():
    """
    Test error handling for out-of-range years in reduction factor calculation.

    Verifies:
    - Exception is raised when requesting a reduction factor for an unsupported year (e.g., 1900).
    - Ensures robust error handling for invalid year input.
    """
    with pytest.raises(Exception):
        get_annual_reduction_factor(1900)

def test_apply_annual_reduction_factor():
    """
    Test application of annual reduction factor to a base value.

    Verifies:
    - apply_annual_reduction_factor returns a value less than or equal to the base for all valid years.
    - Ensures reduction is applied correctly for each year in the valid range.
    """
    base = 100
    for year in range(2019, 2031):
        reduced = apply_annual_reduction_factor(base, year)
        assert reduced <= base
