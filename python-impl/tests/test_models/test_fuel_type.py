import pytest
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel

def test_fuel_type_enum():
    """
    Test presence of all expected enum values in TypeOfFuel.

    Verifies:
    - All required fuel type enum values are present and accessible in TypeOfFuel.
    - Ensures enum covers all supported fuel types for logic elsewhere in the codebase.
    """
    assert hasattr(TypeOfFuel, 'DIESEL_OR_GASOIL')
    assert hasattr(TypeOfFuel, 'LIGHT_FUEL_OIL')
    assert hasattr(TypeOfFuel, 'HEAVY_FUEL_OIL')
    assert hasattr(TypeOfFuel, 'LIQUIFIED_NATURAL_GAS')
    assert hasattr(TypeOfFuel, 'METHANOL')
