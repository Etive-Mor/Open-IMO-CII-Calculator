class WeightClassification:
    """
    Represents weight classification boundaries for ship categories
    """
    
    def __init__(self, upper_limit: int, lower_limit: int):
        """
        Initialize a weight classification with upper and lower limits
        
        Args:
            - upper_limit (int): The upper weight limit for this classification
            - lower_limit (int): The lower weight limit for this classification
        """
        self.upper_limit = upper_limit
        self.lower_limit = lower_limit