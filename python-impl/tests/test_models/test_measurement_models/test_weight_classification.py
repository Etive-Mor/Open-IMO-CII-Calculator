import pytest
from open_imo_cii_calculator.models.measurement_models.weight_classification import WeightClassification

def test_weight_classification_init():
    """
    Test initialization and property assignment for WeightClassification.

    Verifies:
    - WeightClassification object is created with correct upper and lower limits.
    - Properties are assigned as expected during initialization.
    """
    wc = WeightClassification(2000, 1000)
    assert wc.upper_limit == 2000
    assert wc.lower_limit == 1000
