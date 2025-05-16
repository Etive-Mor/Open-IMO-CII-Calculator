from open_imo_cii_calculator.models.ship_type import ShipType

class Ship:
    """
    Class representing a ship with its properties used for CII calculations
    """
    
    def __init__(self, ship_type: ShipType, deadweight_tonnage: float, gross_tonnage: float):
        """
        Initialize a Ship instance with its core properties
        
        Args:
            - ship_type (ShipType): The type of ship (e.g., BULK_CARRIER, TANKER, etc.)
            - deadweight_tonnage (float): The deadweight tonnage (DWT) in long-tons
            - gross_tonnage (float): The gross tonnage (GT) in long-tons
        """
        self.ship_type = ship_type
        self.deadweight_tonnage = deadweight_tonnage
        self.gross_tonnage = gross_tonnage