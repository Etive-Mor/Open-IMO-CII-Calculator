from typing import List, Optional
from open_imo_cii_calculator.models.imo_cii_rating import ImoCiiRating
from open_imo_cii_calculator.models.measurement_models.ship_dd_vector_boundaries import ShipDdVectorBoundaries

class ResultYear:
    """
    Results for a specific year's CII calculation
    """
    
    def __init__(self, 
                 is_measured_year: bool = False,
                 year: int = 0,
                 required_cii: float = 0.0,
                 attained_cii: float = 0.0,
                 rating: ImoCiiRating = ImoCiiRating.UNKNOWN,
                 vector_boundaries_for_year: Optional[ShipDdVectorBoundaries] = None,
                 calculated_co2e_emissions: float = 0.0,
                 calculated_ship_capacity: float = 0.0,
                 calculated_transport_work: float = 0.0):
        """
        Initialize a result year instance
        
        Args:
            - is_measured_year: Indicates if this year is a measured year. If true, all values are measured; if false, all values are estimates
            - year: The year this result references
            - required_cii: The ship's required carbon intensity for this year
            - attained_cii: The ship's attained Carbon Intensity Indicator for this year
            - rating: The ship's IMO CII Rating, from A to E
            - vector_boundaries_for_year: The VectorBoundaries for this ship/year
            - calculated_co2e_emissions: The Co2e Emissions calculated for this year
            - calculated_ship_capacity: The Ship Capacity calculated for this year
            - calculated_transport_work: The Transport Work calculated for this year
        """
        self.is_measured_year = is_measured_year
        self.year = year
        self.required_cii = required_cii
        self.attained_cii = attained_cii
        self.rating = rating
        self.vector_boundaries_for_year = vector_boundaries_for_year
        self.calculated_co2e_emissions = calculated_co2e_emissions
        self.calculated_ship_capacity = calculated_ship_capacity
        self.calculated_transport_work = calculated_transport_work
    
    @property
    def is_estimated_year(self) -> bool:
        """
        Indicates if this year is an estimated year
        
        Returns:
            - True if the year is estimated, False otherwise
        """
        return not self.is_measured_year
    
    @property
    def attained_required_ratio(self) -> float:
        """
        The ratio of Attained:Required CII
        
        Returns:
            - The ratio of Attained to Required CII
        """
        if self.required_cii > 0:
            return self.attained_cii / self.required_cii
        return 0.0