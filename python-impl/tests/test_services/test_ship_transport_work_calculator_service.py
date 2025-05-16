import pytest
from open_imo_cii_calculator.services.ship_transport_work_calculator_service import ShipTransportWorkCalculatorService

class TestShipTransportWorkCalculatorService:
    def test_get_ship_transport_work(self):
        """
        Test transport work calculation and error handling.

        Verifies:
        - Correct transport work is calculated for valid input (multiplication of distance and capacity).
        - Exception is raised for zero or negative distance or capacity values.
        - Ensures robust input validation for transport work calculation.
        """
        service = ShipTransportWorkCalculatorService()
        # Normal case
        result = service.get_ship_transport_work(1000, 100)
        assert result == 100000
        # Zero or negative values should raise
        with pytest.raises(Exception):
            service.get_ship_transport_work(0, 100)
        with pytest.raises(Exception):
            service.get_ship_transport_work(1000, 0)
        with pytest.raises(Exception):
            service.get_ship_transport_work(-1000, 100)
        with pytest.raises(Exception):
            service.get_ship_transport_work(1000, -100)
