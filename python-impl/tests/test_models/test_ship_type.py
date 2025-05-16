import pytest
from open_imo_cii_calculator.models.ship_type import ShipType

def test_ship_type_enum():
    """
    Test presence of all expected enum values in ShipType.

    Verifies:
    - All required ship type enum values are present and accessible in ShipType.
    - Ensures enum covers all supported ship types for logic elsewhere in the codebase.
    """
    assert hasattr(ShipType, 'BULK_CARRIER')
    assert hasattr(ShipType, 'TANKER')
    assert hasattr(ShipType, 'CONTAINER_SHIP')
    assert hasattr(ShipType, 'GENERAL_CARGO_SHIP')
    assert hasattr(ShipType, 'REFRIGERATED_CARGO_CARRIER')
    assert hasattr(ShipType, 'RORO_CARGO_SHIP')
    assert hasattr(ShipType, 'RORO_PASSENGER_SHIP')
    assert hasattr(ShipType, 'LNG_CARRIER')
    assert hasattr(ShipType, 'GAS_CARRIER')
    assert hasattr(ShipType, 'CRUISE_PASSENGER_SHIP')
    assert hasattr(ShipType, 'UNKNOWN')
