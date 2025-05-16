from typing import List
from open_imo_cii_calculator.models.dto.result_year import ResultYear

class CalculationResult:
    """
    Contains results of CII calculations for multiple years
    """
    
    def __init__(self, results: List[ResultYear]):
        """
        Initialize a calculation result instance
        
        Args:
            - results: Contains a collection of CII Ratings for each year between 2019 and 2030
        """
        self.results = results