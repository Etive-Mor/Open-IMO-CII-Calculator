import pytest
from open_imo_cii_calculator.models.imo_cii_rating import ImoCiiRating

def test_imo_cii_rating_enum():
    """
    Test presence of all expected enum values in ImoCiiRating.

    Verifies:
    - All required rating enum values (A-E) are present and accessible in ImoCiiRating.
    - Ensures enum covers all supported ratings for logic elsewhere in the codebase.
    """
    assert hasattr(ImoCiiRating, 'A')
    assert hasattr(ImoCiiRating, 'B')
    assert hasattr(ImoCiiRating, 'C')
    assert hasattr(ImoCiiRating, 'D')
    assert hasattr(ImoCiiRating, 'E')
