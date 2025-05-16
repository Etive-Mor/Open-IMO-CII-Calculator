import pytest
from open_imo_cii_calculator.models.dto.fuel_type_consumption import FuelTypeConsumption
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel

def test_fuel_type_consumption_init():
    """
    Test FuelTypeConsumption object creation and property assignment.

    Verifies:
    - FuelTypeConsumption object is created with correct fuel type and consumption value.
    - Properties are assigned as expected during initialization.
    """
    ftc = FuelTypeConsumption(fuel_type=TypeOfFuel.DIESEL_OR_GASOIL, fuel_consumption=1000)
    assert ftc.fuel_type == TypeOfFuel.DIESEL_OR_GASOIL
    assert ftc.fuel_consumption == 1000
