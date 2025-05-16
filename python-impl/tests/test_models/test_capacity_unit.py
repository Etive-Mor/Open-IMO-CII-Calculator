import pytest
from open_imo_cii_calculator.models.capacity_unit import CapacityUnit

def test_capacity_unit_enum():
    """
    Test presence of all expected enum values in CapacityUnit.

    Verifies:
    - All required capacity unit enum values are present and accessible in CapacityUnit.
    - Ensures enum covers all supported capacity units for logic elsewhere in the codebase.
    """
    assert hasattr(CapacityUnit, 'DWT')
    assert hasattr(CapacityUnit, 'GT')
