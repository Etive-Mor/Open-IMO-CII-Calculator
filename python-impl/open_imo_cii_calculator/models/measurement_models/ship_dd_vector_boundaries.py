from typing import Dict
from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.capacity_unit import CapacityUnit
from open_imo_cii_calculator.models.imo_cii_boundary import ImoCiiBoundary
from open_imo_cii_calculator.models.measurement_models.weight_classification import WeightClassification

class ShipDdVectorBoundaries:
    """
    IMO MEPC.354(78) ddvectors for a given year for the specified ship type
    """
    
    def __init__(self, 
                 ship_type: ShipType, 
                 weight_classification: WeightClassification,
                 capacity_unit: CapacityUnit,
                 boundary_dd_vectors: Dict[ImoCiiBoundary, float], 
                 year: int):
        """
        Initialize ship ddvector boundaries
        
        Args:
            - ship_type (ShipType): The type of ship to generate ddvector boundaries for
            - weight_classification (WeightClassification): The weight classification of the ship. If these ddvectors have a min/max 
                weight boundary in MEPC.354(78), this describes the lower and upper bound of that classification.
            - capacity_unit (CapacityUnit): Indicates the capacity unit these ddvectors are calculated against
            - boundary_dd_vectors (Dict[ImoCiiBoundary, float]): The ddvectors for the specified ship type, weight classification and capacity unit
            - year (int): The year these ddvectors apply to
        """
        self.year = year
        self.ship_type = ship_type
        self.weight_classification = weight_classification
        self.capacity_unit = capacity_unit
        self.boundary_dd_vectors = boundary_dd_vectors