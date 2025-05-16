from open_imo_cii_calculator.models.fuel_type import TypeOfFuel

class FuelTypeConsumption:
    """
    A request object to hold metadata about the fuel consumption of a ship
    """
    
    def __init__(self, fuel_type: TypeOfFuel, fuel_consumption: float):
        """
        Initialize a fuel type consumption instance
        
        Args:
            - fuel_type: The type of fuel consumed by the ship
            - fuel_consumption: The amount of fuel consumed by the ship (in grams)
        """
        self.fuel_type = fuel_type
        self.fuel_consumption = fuel_consumption