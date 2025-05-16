import pytest
from open_imo_cii_calculator.models.dto.annual_consumption import AnnualConsumption
from open_imo_cii_calculator.models.dto.fuel_type_consumption import FuelTypeConsumption
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel

def test_annual_consumption_init():
    """
    Test AnnualConsumption object creation and property assignment.

    Verifies:
    - AnnualConsumption object is created with correct target year and fuel consumption list.
    - Properties are assigned as expected during initialization.
    """
    ftc = FuelTypeConsumption(fuel_type=TypeOfFuel.DIESEL_OR_GASOIL, fuel_consumption=1000)
    ac = AnnualConsumption(target_year=2023, fuel_consumption=[ftc])
    assert ac.target_year == 2023
    assert isinstance(ac.fuel_consumption, list)
    assert ac.fuel_consumption[0].fuel_type == TypeOfFuel.DIESEL_OR_GASOIL
