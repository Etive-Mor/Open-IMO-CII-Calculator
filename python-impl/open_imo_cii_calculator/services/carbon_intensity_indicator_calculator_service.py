"""
Service for calculating Carbon Intensity Indicator (CII)
"""
import math
from enum import Enum
from typing import Dict

from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.services.reduction_factor_utils import apply_annual_reduction_factor


class ValType(Enum):
    """Enumeration for coefficient types used in CII calculations"""
    A = "a"
    C = "c"


class CarbonIntensityIndicatorCalculatorService:
    """
    Service for calculating the Carbon Intensity Indicator (CII) for ships in accordance with IMO regulations
    """
    
    def get_attained_carbon_intensity(self, mass_of_co2_emissions: float, transport_work: float) -> float:
        """
        Gets a ship's attained carbon intensity, which is the ratio of the cumulative mass
        of CO2 emissions in a calendar year to the ship's transport work in a calendar year
        
        Args:
            - mass_of_co2_emissions (float): The cumulative mass of CO2 emissions in a calendar year in grams (g)
            - transport_work (float): The ship's transport work in a calendar year
        
        Returns:
            - float: A ship's attained Carbon Intensity (CII)
        
        Raises:
            - ValueError: If mass_of_co2_emissions or transport_work is less than or equal to zero
        """
        if mass_of_co2_emissions <= 0:
            raise ValueError("Mass of CO2 emissions must be a positive value")
        
        if transport_work <= 0:
            raise ValueError("Transport work must be a positive value")
            
        return mass_of_co2_emissions / transport_work
    
    def get_required_carbon_intensity(self, ship_type: ShipType, capacity: float, year: int) -> float:
        """
        Gets a ship's required CII in accordance with MEPC.323(74)
        
        Args:
            - ship_type (ShipType): The type of ship
            - capacity (float): The ship's capacity according to MEPC 337(76) (pre-calculated)
            - year (int): The calendar year being analyzed
            
        Returns:
            - (float): The required CII for a ship of the given type and capacity
            
        Raises:
            ValueError: If capacity is equal or lower than 0
        """
        if capacity <= 0:
            raise ValueError("Capacity must be a positive value")
            
        a = self._get_value(ValType.A, ship_type, capacity)
        c = self._get_value(ValType.C, ship_type, capacity)
        
        cii_reference = a * math.pow(capacity, -c)
        
        return apply_annual_reduction_factor(cii_reference, year)
    
    def get_required_carbon_intensity_dict(self, ship_type: ShipType, capacity: float) -> Dict[int, float]:
        """
        Gets a ship's required CII for years 2019-2030
        
        Args:
            - ship_type (ShipType): The type of ship
            - capacity (float): The ship's capacity
            
        Returns:
            - (Dict[int, float]): Dictionary mapping years to their required CII values
        """
        cii_dict = {}
        
        for year in range(2019, 2031):
            cii_dict[year] = self.get_required_carbon_intensity(ship_type, capacity, year)
            
        return cii_dict
    
    def _get_value(self, val_type: ValType, ship_type: ShipType, capacity: float) -> float:
        """
        Gets either the `a` or `c` value for a given ship type and capacity
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - ship_type (ShipType): The type of ship being queried
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for the given ship type and capacity
            
        Raises:
            ValueError: If val_type is not a or c
            ValueError: If the ship type is not supported
        """
        if val_type not in [ValType.A, ValType.C]:
            raise ValueError(f"Invalid value type '{val_type}'")
        
        ship_type_handlers = {
            ShipType.BULK_CARRIER: self._get_bulk_carrier_value,
            ShipType.GAS_CARRIER: self._get_gas_carrier_value,
            ShipType.TANKER: self._get_tanker_value,
            ShipType.CONTAINER_SHIP: self._get_container_ship_value,
            ShipType.GENERAL_CARGO_SHIP: self._get_general_cargo_ship_value,
            ShipType.REFRIGERATED_CARGO_CARRIER: self._get_refrigerated_cargo_carrier_value,
            ShipType.COMBINATION_CARRIER: self._get_combination_carrier_value,
            ShipType.LNG_CARRIER: self._get_lng_carrier_ship_value,
            ShipType.RORO_CARGO_SHIP_VEHICLE_CARRIER: self._get_roro_cargo_ship_vehicle_carrier_value,
            ShipType.RORO_CARGO_SHIP: self._get_roro_cargo_ship_value,
            ShipType.RORO_PASSENGER_SHIP: self._get_roro_passenger_ship_value,
            ShipType.RORO_PASSENGER_SHIP_HIGH_SPEED_SOLAS: self._get_roro_passenger_ship_high_speed_solas_value,
            ShipType.CRUISE_PASSENGER_SHIP: self._get_roro_cruise_passenger_ship_value
        }
        
        if ship_type not in ship_type_handlers or ship_type == ShipType.UNKNOWN:
            raise ValueError(f"Unsupported ship type: {ship_type}")
            
        return ship_type_handlers[ship_type](val_type, capacity)
    
    def _get_lng_carrier_ship_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a LNG Carrier, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a LNG Carrier
        """
        if capacity >= 100000:
            return 9.827 if val_type == ValType.A else 0.000
        if capacity >= 65000:
            return 14479E10 if val_type == ValType.A else 2.673
        return 14779E10 if val_type == ValType.A else 2.673
    
    def _get_general_cargo_ship_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a General Cargo ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a General Cargo ship
        """
        if capacity >= 20000:
            return 31948 if val_type == ValType.A else 0.792
        return 588 if val_type == ValType.A else 0.3885
    
    def _get_bulk_carrier_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a Bulk Carrier ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a Bulk Carrier ship
        """
        if capacity >= 279000:
            return 4745 if val_type == ValType.A else 0.622
        return 4745 if val_type == ValType.A else 0.622
    
    def _get_gas_carrier_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a Gas Carrier ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a Gas Carrier ship
        """
        if capacity >= 65000:
            return 14405E7 if val_type == ValType.A else 2.071
        return 8104 if val_type == ValType.A else 0.639
    
    def _get_tanker_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a Tanker ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a Tanker ship
        """
        return 5247 if val_type == ValType.A else 0.610
    
    def _get_container_ship_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a Container ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a Container ship
        """
        return 1984 if val_type == ValType.A else 0.489
    
    def _get_refrigerated_cargo_carrier_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a Refrigerated Cargo Carrier ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a Refrigerated Cargo Carrier ship
        """
        return 4600 if val_type == ValType.A else 0.557
    
    def _get_combination_carrier_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a Combination Carrier ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a Combination Carrier ship
        """
        return 5119 if val_type == ValType.A else 622
    
    def _get_roro_cargo_ship_vehicle_carrier_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a RoRo Cargo Ship Vehicle Carrier, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a RoRo Cargo Ship Vehicle Carrier
        """
        if capacity >= 57700:
            return 3627 if val_type == ValType.A else 0.590
        if capacity >= 30000:
            return 5739 if val_type == ValType.A else 0.590
        return 330 if val_type == ValType.A else 329
    
    def _get_roro_cargo_ship_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a RoRo Cargo Ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a RoRo Cargo Ship
        """
        return 1967 if val_type == ValType.A else 0.485
    
    def _get_roro_passenger_ship_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a RoRo Passenger Ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a RoRo Passenger Ship
        """
        return 2023 if val_type == ValType.A else 0.460
    
    def _get_roro_passenger_ship_high_speed_solas_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a RoRo Passenger Ship (High Speed SOLAS), according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a RoRo Passenger Ship (High Speed SOLAS)
        """
        return 4196 if val_type == ValType.A else 0.460
    
    def _get_roro_cruise_passenger_ship_value(self, val_type: ValType, capacity: float) -> float:
        """
        Gets the appropriate `a` or `c` value for a Cruise Passenger Ship, according to Table 1: MEPC.353(78)
        
        Args:
            - val_type (ValType): The coefficient type to return (a or c)
            - capacity (float): The capacity of the ship being queried
            
        Returns:
            - (float): The `a` or `c` value for a Cruise Passenger Ship
        """
        return 930 if val_type == ValType.A else 0.383