"""
Service for calculating ship capacity according to MEPC.353(78) guidelines
"""
from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.ship_models.ship import Ship


class ShipCapacityCalculatorService:
    """
    Service for calculating ship capacity according to MEPC.353(78) guidelines
    """
    
    def get_ship_capacity(self, ship_or_type, deadweight_tonnage=None, gross_tonnage=None):
        """
        Calculates the ship's capacity according to the MEPC.353(78) guidelines
        
        This method can be called in two ways:
        1. with a Ship object: get_ship_capacity(ship)
        2. with ship type and tonnage values: get_ship_capacity(ship_type, deadweight_tonnage, gross_tonnage)
        
        Args:
            ship_or_type: Either a Ship object or a ShipType enum
            deadweight_tonnage: The ship's deadweight tonnage (DWT) - only required if ship_or_type is a ShipType
            gross_tonnage: The ship's gross tonnage (GT) - only required if ship_or_type is a ShipType
            
        Returns:
            float: The ship's capacity according to MEPC.353(78)
            
        Raises:
            ValueError: If parameters are invalid or ship type is not supported
        """
        if isinstance(ship_or_type, Ship):
            ship = ship_or_type
            return self.get_ship_capacity(ship.ship_type, ship.deadweight_tonnage, ship.gross_tonnage)
        
        ship_type = ship_or_type
        
        self._validate_tonnage_params_set(ship_type, deadweight_tonnage, gross_tonnage)
        
        if ship_type == ShipType.BULK_CARRIER:
            return 279000 if deadweight_tonnage >= 279000 else deadweight_tonnage
        elif ship_type == ShipType.GAS_CARRIER:
            return deadweight_tonnage
        elif ship_type == ShipType.TANKER:
            return deadweight_tonnage
        elif ship_type == ShipType.CONTAINER_SHIP:
            return deadweight_tonnage
        elif ship_type == ShipType.GENERAL_CARGO_SHIP:
            return deadweight_tonnage
        elif ship_type == ShipType.REFRIGERATED_CARGO_CARRIER:
            return deadweight_tonnage
        elif ship_type == ShipType.COMBINATION_CARRIER:
            return deadweight_tonnage
        elif ship_type == ShipType.LNG_CARRIER:
            return 65000 if deadweight_tonnage < 65000 else deadweight_tonnage
        elif ship_type == ShipType.RORO_CARGO_SHIP_VEHICLE_CARRIER:
            return 57700 if deadweight_tonnage >= 57700 else gross_tonnage
        elif ship_type == ShipType.RORO_CARGO_SHIP:
            return gross_tonnage
        elif ship_type == ShipType.RORO_PASSENGER_SHIP:
            return gross_tonnage
        elif ship_type == ShipType.RORO_PASSENGER_SHIP_HIGH_SPEED_SOLAS:
            return gross_tonnage
        elif ship_type == ShipType.CRUISE_PASSENGER_SHIP:
            return gross_tonnage
        else:
            raise ValueError(f"Unsupported ship type: {ship_type}")
            
    def _validate_tonnage_params_set(self, ship_type, deadweight_tonnage, gross_tonnage):
        """
        Validates that the required tonnage parameters are set for the given ship type
        
        Args:
            ship_type: The type of ship
            deadweight_tonnage: The ship's deadweight tonnage
            gross_tonnage: The ship's gross tonnage
            
        Raises:
            ValueError: If the required tonnage parameters are not set or invalid
        """
        # Ship types that require deadweight tonnage
        deadweight_ship_types = [
            ShipType.BULK_CARRIER,
            ShipType.GAS_CARRIER,
            ShipType.TANKER,
            ShipType.CONTAINER_SHIP,
            ShipType.GENERAL_CARGO_SHIP,
            ShipType.REFRIGERATED_CARGO_CARRIER,
            ShipType.COMBINATION_CARRIER,
            ShipType.LNG_CARRIER
        ]
        
        # Ship types that require gross tonnage
        gross_tonnage_ship_types = [
            ShipType.RORO_CARGO_SHIP_VEHICLE_CARRIER,
            ShipType.RORO_CARGO_SHIP,
            ShipType.RORO_PASSENGER_SHIP,
            ShipType.RORO_PASSENGER_SHIP_HIGH_SPEED_SOLAS,
            ShipType.CRUISE_PASSENGER_SHIP
        ]
        
        if ship_type in deadweight_ship_types:
            self._validate_tonnage(deadweight_tonnage, "deadweight_tonnage", ship_type)
            
        if ship_type in gross_tonnage_ship_types:
            self._validate_tonnage(gross_tonnage, "gross_tonnage", ship_type)
            
    def _validate_tonnage(self, tonnage, tonnage_name, ship_type):
        """
        Validates that the tonnage value is greater than 0
        
        Args:
            tonnage: The tonnage value to validate
            tonnage_name: The name of the tonnage parameter
            ship_type: The type of ship
            
        Raises:
            ValueError: If the tonnage value is less than or equal to 0
        """
        if tonnage is None or tonnage <= 0:
            raise ValueError(f"{tonnage_name} must be greater than 0 if ship_type is set to {ship_type}")
        
        return True