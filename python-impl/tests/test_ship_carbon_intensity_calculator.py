import pytest
from open_imo_cii_calculator.ship_carbon_intensity_calculator import ShipCarbonIntensityCalculator
from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel
from open_imo_cii_calculator.models.dto.fuel_type_consumption import FuelTypeConsumption

class TestShipCarbonIntensityCalculator:
    def test_init(self):
        """Test that the calculator initializes correctly"""
        calculator = ShipCarbonIntensityCalculator()
        assert calculator is not None
    
    def test_calculate_attained_cii_rating_single_fuel(self, sample_ship_data, sample_fuel_consumption):
        """Test calculation with a single fuel type"""
        calculator = ShipCarbonIntensityCalculator()
        result = calculator.calculate_attained_cii_rating(
            sample_ship_data['ship_type'],
            sample_ship_data['gross_tonnage'],
            sample_ship_data['deadweight_tonnage'],
            sample_ship_data['distance_travelled'],
            sample_fuel_consumption,
            sample_ship_data['target_year']
        )
        assert result is not None
        assert hasattr(result, 'results')
