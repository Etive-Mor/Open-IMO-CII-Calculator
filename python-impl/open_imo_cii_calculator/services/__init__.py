"""
This module initializes the services package.
"""
# Import services to make them available at the package level
from open_imo_cii_calculator.services.carbon_intensity_indicator_calculator_service import CarbonIntensityIndicatorCalculatorService
from open_imo_cii_calculator.services.ship_capacity_calculator_service import ShipCapacityCalculatorService
from open_imo_cii_calculator.services.ship_mass_of_co2_emissions_calculator_service import ShipMassOfCo2EmissionsCalculatorService
from open_imo_cii_calculator.services.ship_transport_work_calculator_service import ShipTransportWorkCalculatorService
from open_imo_cii_calculator.services.rating_boundaries_service import RatingBoundariesService

__all__ = [
    'CarbonIntensityIndicatorCalculatorService',
    'ShipCapacityCalculatorService',
    'ShipMassOfCo2EmissionsCalculatorService',
    'ShipTransportWorkCalculatorService',
    'RatingBoundariesService'
]