from typing import List
from open_imo_cii_calculator.models.dto.fuel_type_consumption import FuelTypeConsumption

class AnnualConsumption:
    """
    A request object to hold metadata about the annual consumption of a ship
    """
    
    def __init__(self, target_year: int, fuel_consumption: List[FuelTypeConsumption]):
        """
        Initialize an annual consumption instance
        
        Args:
            - target_year: The year this consumption measures
            - fuel_consumption: The total fuel consumption in the given year
        """
        self.target_year = target_year
        self.fuel_consumption = fuel_consumption