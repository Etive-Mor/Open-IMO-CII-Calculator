import pytest
from open_imo_cii_calculator.models.dto.result_year import ResultYear

def test_result_year_init():
    """
    Test ResultYear object creation and property assignment.

    Verifies:
    - ResultYear object is created with correct year, measured flag, rating, and other properties.
    - Properties are assigned as expected during initialization.
    """
    ry = ResultYear(is_measured_year=True, year=2023, attained_cii=1.0, required_cii=2.0, rating='A', vector_boundaries_for_year=None, calculated_co2e_emissions=100, calculated_ship_capacity=200, calculated_transport_work=300)
    assert ry.year == 2023
    assert ry.is_measured_year is True
    assert ry.rating == 'A'

def test_is_estimated_year():
    """
    Test property logic for is_estimated_year in ResultYear.

    Verifies:
    - is_estimated_year returns True when is_measured_year is False, and False otherwise.
    - Ensures property logic is correct for measured/estimated year distinction.
    """
    ry = ResultYear(is_measured_year=False, year=2023, attained_cii=1.0, required_cii=2.0, rating='A', vector_boundaries_for_year=None, calculated_co2e_emissions=100, calculated_ship_capacity=200, calculated_transport_work=300)
    assert ry.is_estimated_year is True
    ry2 = ResultYear(is_measured_year=True, year=2023, attained_cii=1.0, required_cii=2.0, rating='A', vector_boundaries_for_year=None, calculated_co2e_emissions=100, calculated_ship_capacity=200, calculated_transport_work=300)
    assert ry2.is_estimated_year is False

def test_attained_required_ratio():
    """
    Test attained_required_ratio property logic in ResultYear, including zero/negative required.

    Verifies:
    - attained_required_ratio returns correct ratio when required_cii is positive.
    - Returns 0.0 when required_cii is zero to avoid division by zero.
    - Ensures robust calculation logic for edge cases.
    """
    ry = ResultYear(is_measured_year=True, year=2023, attained_cii=10.0, required_cii=2.0, rating='A', vector_boundaries_for_year=None, calculated_co2e_emissions=100, calculated_ship_capacity=200, calculated_transport_work=300)
    assert ry.attained_required_ratio == 5.0
    ry2 = ResultYear(is_measured_year=True, year=2023, attained_cii=10.0, required_cii=0, rating='A', vector_boundaries_for_year=None, calculated_co2e_emissions=100, calculated_ship_capacity=200, calculated_transport_work=300)
    assert ry2.attained_required_ratio == 0.0
