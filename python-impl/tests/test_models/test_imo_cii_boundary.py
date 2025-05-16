import pytest
from open_imo_cii_calculator.models.imo_cii_boundary import ImoCiiBoundary

def test_imo_cii_boundary_enum():
    """
    Test presence of all expected enum values in ImoCiiBoundary.

    Verifies:
    - All required boundary enum values are present and accessible in ImoCiiBoundary.
    - Ensures enum covers all supported boundaries for logic elsewhere in the codebase.
    """
    assert hasattr(ImoCiiBoundary, 'SUPERIOR')
    assert hasattr(ImoCiiBoundary, 'LOWER')
    assert hasattr(ImoCiiBoundary, 'UPPER')
    assert hasattr(ImoCiiBoundary, 'INFERIOR')
