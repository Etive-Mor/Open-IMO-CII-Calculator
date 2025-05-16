import pytest
from open_imo_cii_calculator.models.ship_models.ship import Ship
from open_imo_cii_calculator.models.ship_type import ShipType

def test_ship_init():
    """
    Test Ship object creation and property assignment.

    Verifies:
    - Ship object is created with correct ship type, deadweight tonnage, and gross tonnage.
    - Properties are assigned as expected during initialization.
    """
    ship = Ship(ShipType.BULK_CARRIER, 50000, 30000)
    assert ship.ship_type == ShipType.BULK_CARRIER
    assert ship.deadweight_tonnage == 50000
    assert ship.gross_tonnage == 30000
