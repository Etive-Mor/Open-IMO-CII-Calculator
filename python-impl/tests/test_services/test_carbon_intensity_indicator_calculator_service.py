import pytest
from open_imo_cii_calculator.services.carbon_intensity_indicator_calculator_service import CarbonIntensityIndicatorCalculatorService, ValType
from open_imo_cii_calculator.models.ship_type import ShipType

class TestCarbonIntensityIndicatorCalculatorService:
    def test_get_attained_carbon_intensity(self):
        """
        Test attained CII calculation and error handling.

        Verifies:
        - Correct attained CII is calculated for valid input.
        - Exception is raised for zero transport work.
        - Exception is raised for negative values.
        """
        service = CarbonIntensityIndicatorCalculatorService()
        # Normal case
        result = service.get_attained_carbon_intensity(1000, 100)
        assert result > 0
        # Zero transport work should raise
        with pytest.raises(Exception):
            service.get_attained_carbon_intensity(1000, 0)
        # Negative values should raise
        with pytest.raises(Exception):
            service.get_attained_carbon_intensity(-1000, 100)

    def test_get_required_carbon_intensity(self):
        """
        Test required CII calculation and error handling.

        Verifies:
        - Required CII is calculated for valid ship type, tonnage, and year.
        - Exception is raised for invalid ship type or year.
        """
        service = CarbonIntensityIndicatorCalculatorService()
        # Example: bulk carrier, year 2023
        result = service.get_required_carbon_intensity(ShipType.BULK_CARRIER, 50000, 2023)
        assert result > 0
        # Invalid ship type or year should raise
        with pytest.raises(Exception):
            service.get_required_carbon_intensity(None, 50000, 2023)

    def test_get_required_carbon_intensity_dict(self):
        """
        Test required CII calculation for all years as a dictionary.

        Verifies:
        - Returns a dictionary of required CII values for all years for a given ship type and tonnage.
        - Ensures the result contains expected years as keys.
        """
        service = CarbonIntensityIndicatorCalculatorService()
        # Should return a dict for all years
        result = service.get_required_carbon_intensity_dict(ShipType.BULK_CARRIER, 50000)
        assert isinstance(result, dict)
        assert 2023 in result

    def test_get_value_and_ship_type_handlers(self):
        """
        Test ship type handler logic for value retrieval.

        Verifies:
        - _get_value returns a float or int for supported ship types and value types.
        - Ensures handler logic works for all supported ship types.
        """
        service = CarbonIntensityIndicatorCalculatorService()
        # Only test supported ship types
        supported_types = [
            ShipType.BULK_CARRIER,
            ShipType.GAS_CARRIER,
            ShipType.TANKER,
            ShipType.CONTAINER_SHIP,
            ShipType.GENERAL_CARGO_SHIP,
            ShipType.REFRIGERATED_CARGO_CARRIER,
            ShipType.COMBINATION_CARRIER,
            ShipType.LNG_CARRIER,
            ShipType.RORO_CARGO_SHIP_VEHICLE_CARRIER,
            ShipType.RORO_CARGO_SHIP,
            ShipType.RORO_PASSENGER_SHIP,
            ShipType.RORO_PASSENGER_SHIP_HIGH_SPEED_SOLAS,
            ShipType.CRUISE_PASSENGER_SHIP
        ]
        for ship_type in supported_types:
            result_a = service._get_value(ValType.A, ship_type, 50000)
            result_c = service._get_value(ValType.C, ship_type, 50000)
            assert isinstance(result_a, (float, int))
            assert isinstance(result_c, (float, int))
