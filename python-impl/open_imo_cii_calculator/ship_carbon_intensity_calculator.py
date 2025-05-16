"""
Main calculator class for IMO CII calculations
"""
from typing import List, Iterable

from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel
from open_imo_cii_calculator.models.imo_cii_rating import ImoCiiRating
from open_imo_cii_calculator.models.imo_cii_boundary import ImoCiiBoundary
from open_imo_cii_calculator.models.ship_models.ship import Ship
from open_imo_cii_calculator.models.dto.calculation_result import CalculationResult
from open_imo_cii_calculator.models.dto.result_year import ResultYear
from open_imo_cii_calculator.models.dto.fuel_type_consumption import FuelTypeConsumption
from open_imo_cii_calculator.models.measurement_models.ship_dd_vector_boundaries import ShipDdVectorBoundaries

from open_imo_cii_calculator.services.carbon_intensity_indicator_calculator_service import CarbonIntensityIndicatorCalculatorService
from open_imo_cii_calculator.services.rating_boundaries_service import RatingBoundariesService
from open_imo_cii_calculator.services.ship_capacity_calculator_service import ShipCapacityCalculatorService
from open_imo_cii_calculator.services.ship_mass_of_co2_emissions_calculator_service import ShipMassOfCo2EmissionsCalculatorService
from open_imo_cii_calculator.services.ship_transport_work_calculator_service import ShipTransportWorkCalculatorService


class ShipCarbonIntensityCalculator:
    """
    Main calculator for determining a ship's Carbon Intensity Indicator (CII) rating
    according to IMO's MEPC.354(78) guidelines.
    """
    
    def __init__(self):
        """
        Initialize a new instance of the ShipCarbonIntensityCalculator
        """
        self._ship_mass_of_co2_emissions_service = ShipMassOfCo2EmissionsCalculatorService()
        self._ship_capacity_service = ShipCapacityCalculatorService()
        self._ship_transport_work_service = ShipTransportWorkCalculatorService()
        self._carbon_intensity_indicator_service = CarbonIntensityIndicatorCalculatorService()
        self._rating_boundaries_service = RatingBoundariesService()
    
    def calculate_attained_cii_rating(self,
                                     ship_type: ShipType,
                                     gross_tonnage: float,
                                     deadweight_tonnage: float,
                                     distance_travelled: float,
                                     fuel_type_consumptions: Iterable[FuelTypeConsumption],
                                     target_year: int) -> CalculationResult:
        """
        Calculate the attained CII rating for a ship for a given year
        
        This method accepts multiple fuel types.
        
        Args:
            - ship_type (ShipType): The type of the ship
            - gross_tonnage (float): The gross tonnage of the ship in long-tons
            - deadweight_tonnage (float): The deadweight tonnage of the ship in long-tons
            - distance_travelled (float): The distance travelled by the ship in a calendar year (nautical miles)
            - fuel_type_consumptions (Iterable[FuelTypeConsumption]): List of fuel type consumption objects
            - target_year (int): The year for which to calculate the attained CII
        
        Returns:
            - CalculationResult: The result of the CII calculation for the ship
        
        Raises:
            - ValueError: If any input is invalid or missing
        """
        if not fuel_type_consumptions:
            raise ValueError("fuel_type_consumptions must be provided")
        
        ship_co2_emissions = 0
        for consumption in fuel_type_consumptions:
            ship_co2_emissions += self._ship_mass_of_co2_emissions_service.get_mass_of_co2_emissions(
                consumption.fuel_type, consumption.fuel_consumption)
        
        ship_capacity = self._ship_capacity_service.get_ship_capacity(
            ship_type, deadweight_tonnage, gross_tonnage)
        
        transport_work = self._ship_transport_work_service.get_ship_transport_work(
            ship_capacity, distance_travelled)
        
        results = []
        for year in range(2019, 2031):
            attained_cii_in_year = self._carbon_intensity_indicator_service.get_attained_carbon_intensity(
                ship_co2_emissions, transport_work)
            
            required_cii_in_year = self._carbon_intensity_indicator_service.get_required_carbon_intensity(
                ship_type, ship_capacity, year)
            
            vectors = self._rating_boundaries_service.get_boundaries(
                Ship(ship_type, deadweight_tonnage, gross_tonnage), required_cii_in_year, year)
            
            rating = self._get_imo_cii_rating_from_vectors(vectors, attained_cii_in_year, year)
            
            results.append(ResultYear(
                is_measured_year=(target_year == year),
                year=year,
                attained_cii=attained_cii_in_year,
                required_cii=required_cii_in_year,
                rating=rating,
                vector_boundaries_for_year=vectors,
                calculated_co2e_emissions=ship_co2_emissions,
                calculated_ship_capacity=ship_capacity,
                calculated_transport_work=transport_work
            ))
        
        return CalculationResult(results)
    
    def calculate_attained_cii_rating_single_fuel(self,
                                                ship_type: ShipType,
                                                gross_tonnage: float,
                                                deadweight_tonnage: float,
                                                distance_travelled: float,
                                                fuel_type: TypeOfFuel,
                                                fuel_consumption: float,
                                                target_year: int) -> CalculationResult:
        """
        Calculate the attained CII rating for a ship for a given year
        
        This method accepts exactly one fuel type. For multiple fuel types, use the
        calculate_attained_cii_rating method.
        
        Args:
            - ship_type (ShipType): The type of ship
            - gross_tonnage (float): in long-tons
            - deadweight_tonnage (float): in long-tons
            - distance_travelled (float): distance travelled in nautical miles
            - fuel_type (TypeOfFuel): The type of fuel
            - fuel_consumption (float): quantity of fuel consumed in grams
            - target_year (int): The calendar year being analyzed
            
        Returns:
            - (CalculationResult): A CalculationResult containing details of the ship's carbon intensity rating
        """
        ship_co2_emissions = self._ship_mass_of_co2_emissions_service.get_mass_of_co2_emissions(
            fuel_type, fuel_consumption)
        
        ship_capacity = self._ship_capacity_service.get_ship_capacity(
            ship_type, deadweight_tonnage, gross_tonnage)
        
        transport_work = self._ship_transport_work_service.get_ship_transport_work(
            ship_capacity, distance_travelled)
        
        results = []
        for year in range(2019, 2031):
            attained_cii_in_year = self._carbon_intensity_indicator_service.get_attained_carbon_intensity(
                ship_co2_emissions, transport_work)
            
            required_cii_in_year = self._carbon_intensity_indicator_service.get_required_carbon_intensity(
                ship_type, ship_capacity, year)
            
            vectors = self._rating_boundaries_service.get_boundaries(
                Ship(ship_type, deadweight_tonnage, gross_tonnage), required_cii_in_year, year)
            
            rating = self._get_imo_cii_rating_from_vectors(vectors, attained_cii_in_year, year)
            
            results.append(ResultYear(
                is_measured_year=(target_year == year),
                year=year,
                attained_cii=attained_cii_in_year,
                required_cii=required_cii_in_year,
                rating=rating,
                vector_boundaries_for_year=vectors,
                calculated_co2e_emissions=ship_co2_emissions,
                calculated_ship_capacity=ship_capacity,
                calculated_transport_work=transport_work
            ))
        
        return CalculationResult(results)
    
    def _get_imo_cii_rating_from_vectors(self, 
                                       boundaries: ShipDdVectorBoundaries, 
                                       attained_cii_in_year: float, 
                                       year: int) -> ImoCiiRating:
        """
        Determines the IMO CII rating from vector boundaries and attained CII
        
        Args:
            - boundaries (ShipDdVectorBoundaries): The ship's boundary vectors for the given year
            - attained_cii_in_year (float): The ship's attained CII in the given year
            - year (int): The calendar year being analyzed
            
        Returns:
            - (ImoCiiRating): The ship's IMO CII rating (A through E)
        """
        if attained_cii_in_year < boundaries.boundary_dd_vectors[ImoCiiBoundary.SUPERIOR]:
            # lower than the "superior" boundary
            return ImoCiiRating.A
        elif attained_cii_in_year < boundaries.boundary_dd_vectors[ImoCiiBoundary.LOWER]:
            # lower than the "lower" boundary
            return ImoCiiRating.B
        elif attained_cii_in_year < boundaries.boundary_dd_vectors[ImoCiiBoundary.UPPER]:
            # lower than the "upper" boundary
            return ImoCiiRating.C
        elif attained_cii_in_year < boundaries.boundary_dd_vectors[ImoCiiBoundary.INFERIOR]:
            # lower than the "inferior" boundary
            return ImoCiiRating.D
        else:
            # higher than the inferior boundary
            return ImoCiiRating.E
