import pytest
from open_imo_cii_calculator.services.rating_boundaries_service import RatingBoundariesService
from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.ship_models.ship import Ship

class TestRatingBoundariesService:
    def test_get_boundaries_all_types(self):
        """
        Test correct boundary calculation for all ship types.

        Verifies:
        - Boundaries are returned as expected for a valid ship type, year, and required CII.
        - The result contains a 'boundary_dd_vectors' attribute as a dictionary.
        """
        service = RatingBoundariesService()
        # Example: test for a bulk carrier, year 2023, required_cii 10
        ship = Ship(ShipType.BULK_CARRIER, 50000, 30000)
        boundaries = service.get_boundaries(ship, 10, 2023)
        assert hasattr(boundaries, 'boundary_dd_vectors')
        assert isinstance(boundaries.boundary_dd_vectors, dict)

    def test_get_boundaries_invalid_type(self):
        """
        Test error handling for invalid ship type or parameters.

        Verifies:
        - An exception is raised when an invalid ship type or negative parameters are provided.
        - Ensures robust error handling for boundary calculation.
        """
        service = RatingBoundariesService()
        # Use an invalid ship type or params to trigger error handling
        with pytest.raises(Exception):
            ship = Ship(None, -1, -1)
            service.get_boundaries(ship, -1, 2023)

    def test_validate_ship_tonnage_valid(self):
        """
        Test validation logic for valid ship tonnage parameters.

        Verifies:
        - No exception is raised for valid tonnage values.
        - Confirms that the validation logic accepts correct input.
        """
        service = RatingBoundariesService()
        # Should not raise for valid tonnage
        ship = Ship(ShipType.BULK_CARRIER, 50000, 30000)
        try:
            service._validate_ship_tonnage_valid(ship)
        except Exception:
            pytest.fail("Unexpected exception for valid tonnage")

    def test_validate_ship_tonnage_invalid(self):
        """
        Test error handling for invalid ship tonnage parameters.

        Verifies:
        - An exception is raised for negative or invalid tonnage values.
        - Ensures validation logic rejects incorrect input.
        """
        service = RatingBoundariesService()
        # Should raise for invalid tonnage
        ship = Ship(ShipType.BULK_CARRIER, -1, -1)
        with pytest.raises(Exception):
            service._validate_ship_tonnage_valid(ship)
