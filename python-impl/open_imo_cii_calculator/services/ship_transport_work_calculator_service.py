"""
Service for calculating ship transport work
"""


class ShipTransportWorkCalculatorService:
    """
    Service for calculating the transport work of a ship
    """
    
    def get_ship_transport_work(self, capacity: float, distance_travelled_in_nautical_miles: float) -> float:
        """
        Gets a ship's transport work, which is the product of the ship's capacity and the distance travelled
        in a calendar year expressed in nautical miles.

        Args:
            - capacity (float): The ship's capacity (pre-calculated)
            - distance_travelled_in_nautical_miles (float): The distance travelled by the ship in a calendar year in nautical miles

        Returns:
            - float: The ship's transport work
        """
        if capacity <= 0:
            raise ValueError("Capacity must be a positive value")
            
        if distance_travelled_in_nautical_miles <= 0:
            raise ValueError("Distance travelled must be a positive value")
            
        return capacity * distance_travelled_in_nautical_miles