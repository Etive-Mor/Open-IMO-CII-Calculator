import pytest
from open_imo_cii_calculator.services.ship_mass_of_co2_emissions_calculator_service import ShipMassOfCo2EmissionsCalculatorService
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel

class TestShipMassOfCo2EmissionsCalculatorService:
    @pytest.mark.parametrize(
        "fuel_type,fuel_consumption,expected_result", 
        [
            (TypeOfFuel.DIESEL_OR_GASOIL, 1000, 3206),
            (TypeOfFuel.LIGHT_FUEL_OIL, 1000, 3151),
            (TypeOfFuel.HEAVY_FUEL_OIL, 1000, 3114),
            (TypeOfFuel.LIQUIFIED_NATURAL_GAS, 1000, 2750),
            (TypeOfFuel.METHANOL, 1000, 1375),
        ]
    )
    def test_get_mass_of_co2_emissions(self, fuel_type, fuel_consumption, expected_result):
        """Test CO2 emission calculations for each fuel type"""
        service = ShipMassOfCo2EmissionsCalculatorService()
        result = service.get_mass_of_co2_emissions(fuel_type, fuel_consumption)
        assert result == expected_result
    
    def test_negative_fuel_consumption_raises_error(self):
        """Test that negative fuel consumption raises ValueError"""
        service = ShipMassOfCo2EmissionsCalculatorService()
        with pytest.raises(ValueError):
            service.get_mass_of_co2_emissions(TypeOfFuel.DIESEL_OR_GASOIL, -100)
