import pytest
from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel
from open_imo_cii_calculator.models.dto.fuel_type_consumption import FuelTypeConsumption

@pytest.fixture
def sample_ship_data():
    """Fixture providing sample ship data"""
    return {
        'ship_type': ShipType.RORO_PASSENGER_SHIP,
        'gross_tonnage': 25000,
        'deadweight_tonnage': 10000,
        'distance_travelled': 150000,
        'target_year': 2019
    }

@pytest.fixture
def sample_fuel_consumption():
    """Fixture providing sample fuel consumption data"""
    return [FuelTypeConsumption(TypeOfFuel.DIESEL_OR_GASOIL, 1.9e+10)]

@pytest.fixture
def sample_multi_fuel_consumption():
    """Fixture providing sample multi-fuel consumption data"""
    return [
        FuelTypeConsumption(TypeOfFuel.DIESEL_OR_GASOIL, 1.0e+10),
        FuelTypeConsumption(TypeOfFuel.LIQUIFIED_NATURAL_GAS, 9.0e+9)
    ]
