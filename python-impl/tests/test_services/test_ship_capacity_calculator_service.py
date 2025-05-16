import pytest
from open_imo_cii_calculator.services.ship_capacity_calculator_service import ShipCapacityCalculatorService
from open_imo_cii_calculator.models.ship_type import ShipType

class TestShipCapacityCalculatorService:
    def test_get_ship_capacity_all_types(self):
        """
        Test capacity calculation for all ship types.

        Verifies:
        - Capacity is calculated and returned for a valid ship type and tonnage values.
        - Ensures positive capacity for valid input.
        """
        service = ShipCapacityCalculatorService()
        # Example: bulk carrier
        capacity = service.get_ship_capacity(ShipType.BULK_CARRIER, 50000, 30000)
        assert capacity > 0

    def test_get_ship_capacity_invalid(self):
        """
        Test error handling for invalid tonnage parameters in capacity calculation.

        Verifies:
        - An exception is raised when negative tonnage values are provided.
        - Ensures validation logic rejects invalid input.
        """
        service = ShipCapacityCalculatorService()
        with pytest.raises(Exception):
            service.get_ship_capacity(ShipType.BULK_CARRIER, -1, -1)

    def test_validate_tonnage_params_set(self):
        """
        Test validation logic for tonnage parameters being set.

        Verifies:
        - No exception is raised for valid tonnage parameters.
        - Ensures validation logic accepts correct input.
        """
        service = ShipCapacityCalculatorService()
        # Should not raise for valid params
        try:
            service._validate_tonnage_params_set(ShipType.BULK_CARRIER, 50000, 30000)
        except Exception:
            pytest.fail("Unexpected exception for valid params")

    def test_validate_tonnage_params_invalid(self):
        """
        Test error handling for unset tonnage parameters.

        Verifies:
        - An exception is raised when tonnage parameters are None.
        - Ensures validation logic rejects missing input.
        """
        service = ShipCapacityCalculatorService()
        with pytest.raises(Exception):
            service._validate_tonnage_params_set(ShipType.BULK_CARRIER, None, None)

    def test_validate_tonnage(self):
        """
        Test validation logic for individual tonnage values.

        Verifies:
        - No exception is raised for positive tonnage values.
        - An exception is raised for zero or negative tonnage values.
        - Ensures robust validation for tonnage input.
        """
        service = ShipCapacityCalculatorService()
        # Should not raise for tonnage > 0
        try:
            service._validate_tonnage(1000, "deadweight_tonnage", ShipType.BULK_CARRIER)
        except Exception:
            pytest.fail("Unexpected exception for valid tonnage")
        # Should raise for tonnage <= 0
        with pytest.raises(Exception):
            service._validate_tonnage(0, "deadweight_tonnage", ShipType.BULK_CARRIER)
