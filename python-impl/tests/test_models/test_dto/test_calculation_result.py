import pytest
from open_imo_cii_calculator.models.dto.calculation_result import CalculationResult
from open_imo_cii_calculator.models.dto.result_year import ResultYear

def test_calculation_result_init():
    """
    Test CalculationResult object creation and property assignment.

    Verifies:
    - CalculationResult object is created with a list of ResultYear objects.
    - Properties are assigned as expected during initialization.
    """
    ry = ResultYear(is_measured_year=True, year=2023, attained_cii=1.0, required_cii=2.0, rating='A', vector_boundaries_for_year=None, calculated_co2e_emissions=100, calculated_ship_capacity=200, calculated_transport_work=300)
    cr = CalculationResult(results=[ry])
    assert isinstance(cr.results, list)
    assert cr.results[0].year == 2023
