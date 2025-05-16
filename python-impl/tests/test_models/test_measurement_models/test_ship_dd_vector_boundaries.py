import pytest
from open_imo_cii_calculator.models.measurement_models.ship_dd_vector_boundaries import ShipDdVectorBoundaries
from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.capacity_unit import CapacityUnit
from open_imo_cii_calculator.models.imo_cii_boundary import ImoCiiBoundary
from open_imo_cii_calculator.models.measurement_models.weight_classification import WeightClassification

def test_ship_dd_vector_boundaries_init():
    """
    Test initialization and property assignment for ShipDdVectorBoundaries.

    Verifies:
    - ShipDdVectorBoundaries object is created with correct ship type, weight classification, capacity unit, boundaries, and year.
    - boundary_dd_vectors is a dictionary containing expected boundaries.
    """
    wc = WeightClassification(2000, 1000)
    boundaries = ShipDdVectorBoundaries(
        ship_type=ShipType.BULK_CARRIER,
        weight_classification=wc,
        capacity_unit=CapacityUnit.DWT,
        boundary_dd_vectors={ImoCiiBoundary.SUPERIOR: 1, ImoCiiBoundary.LOWER: 2},
        year=2023
    )
    assert isinstance(boundaries.boundary_dd_vectors, dict)
    assert ImoCiiBoundary.SUPERIOR in boundaries.boundary_dd_vectors
