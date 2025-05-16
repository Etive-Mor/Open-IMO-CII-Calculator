import pytest
from open_imo_cii_calculator.services.ship_mass_of_co2_emissions_calculator_service import ShipMassOfCo2EmissionsCalculatorService
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel

class TestShipMassOfCo2EmissionsCalculatorService:
    @pytest.mark.parametrize(
        "fuel_type,expected_factor",
        [
            (TypeOfFuel.DIESEL_OR_GASOIL, 3.206),
            (TypeOfFuel.LIGHT_FUEL_OIL, 3.151),
            (TypeOfFuel.HEAVY_FUEL_OIL, 3.114),
            (TypeOfFuel.LIQUIFIED_NATURAL_GAS, 2.75),
            (TypeOfFuel.METHANOL, 1.375),
        ]
    )
    def test_get_fuel_mass_conversion_factor(self, fuel_type, expected_factor):
        """
        Test the retrieval of fuel mass conversion factors for various fuel types.

        Verifies:
        - Correct conversion factor is returned for each specified fuel type.
        - Uses parameterized tests to cover different fuel types.
        """
        service = ShipMassOfCo2EmissionsCalculatorService()
        factor = service.get_fuel_mass_conversion_factor(fuel_type)
        assert abs(factor - expected_factor) < 0.01

    @pytest.mark.parametrize(
        "fuel_type,expected_content",
        [
            (TypeOfFuel.DIESEL_OR_GASOIL, 0.875),
            (TypeOfFuel.LIGHT_FUEL_OIL, 0.86),
            (TypeOfFuel.HEAVY_FUEL_OIL, 0.85),
            (TypeOfFuel.LIQUIFIED_NATURAL_GAS, 0.75),
            (TypeOfFuel.METHANOL, 0.375),
        ]
    )
    def test_get_fuel_carbon_content(self, fuel_type, expected_content):
        """
        Test the retrieval of fuel carbon content for various fuel types.

        Verifies:
        - Correct carbon content is returned for each specified fuel type.
        - Uses parameterized tests to cover different fuel types.
        """
        service = ShipMassOfCo2EmissionsCalculatorService()
        content = service.get_fuel_carbon_content(fuel_type)
        assert abs(content - expected_content) < 0.01

    @pytest.mark.parametrize(
        "fuel_type,expected_lcv",
        [
            (TypeOfFuel.DIESEL_OR_GASOIL, 42700),
            (TypeOfFuel.LIGHT_FUEL_OIL, 41200),
            (TypeOfFuel.HEAVY_FUEL_OIL, 40200),
            (TypeOfFuel.LIQUIFIED_NATURAL_GAS, 48000),
            (TypeOfFuel.METHANOL, 19900),
        ]
    )
    def test_get_fuel_lower_calorific_value(self, fuel_type, expected_lcv):
        """
        Test the retrieval of lower calorific values (LCV) for various fuel types.

        Verifies:
        - Correct LCV is returned for each specified fuel type.
        - Uses parameterized tests to cover different fuel types.
        """
        service = ShipMassOfCo2EmissionsCalculatorService()
        lcv = service.get_fuel_lower_calorific_value(fuel_type)
        assert abs(lcv - expected_lcv) < 1
