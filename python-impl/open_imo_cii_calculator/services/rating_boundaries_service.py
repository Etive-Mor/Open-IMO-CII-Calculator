"""
Service for calculating rating boundaries according to MEPC.354(78) guidelines
"""
from typing import Dict

from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.capacity_unit import CapacityUnit
from open_imo_cii_calculator.models.imo_cii_boundary import ImoCiiBoundary
from open_imo_cii_calculator.models.measurement_models.ship_dd_vector_boundaries import ShipDdVectorBoundaries
from open_imo_cii_calculator.models.measurement_models.weight_classification import WeightClassification
from open_imo_cii_calculator.models.ship_models.ship import Ship


class RatingBoundariesService:
    """
    Service for calculating ship rating boundaries according to MEPC.354(78) guidelines
    """
    
    def get_boundaries(self, ship: Ship, required_cii_in_year: float, year: int) -> ShipDdVectorBoundaries:
        """
        Returns the ship grading boundaries outlined in MEPC.354(78) for a given
        ship and required CII in a year.
        
        Args:
            - ship (Ship): The ship object containing ship type and tonnage information
            - required_cii_in_year (float): The required CII value for the specified year
            - year (int): The year for which to calculate the boundaries
        
        Returns:
            - ShipDdVectorBoundaries: The rating boundaries for the ship
        
        Raises:
            - ValueError: If the ship tonnage is invalid or ship type is not supported
        """
        self._validate_ship_tonnage_valid(ship)

        if ship.ship_type == ShipType.BULK_CARRIER:
            if ship.deadweight_tonnage <= 0:
                raise ValueError(f"Deadweight tonnage must be greater than 0 for ship type {ship.ship_type}")
                
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.DWT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.86 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.94 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.06 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.18 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.GAS_CARRIER:
            if ship.deadweight_tonnage >= 65000:
                return ShipDdVectorBoundaries(
                    ship.ship_type,
                    WeightClassification(65000, float('inf')),
                    CapacityUnit.DWT,
                    {
                        ImoCiiBoundary.SUPERIOR: 0.81 * required_cii_in_year,
                        ImoCiiBoundary.LOWER: 0.91 * required_cii_in_year,
                        ImoCiiBoundary.UPPER: 1.12 * required_cii_in_year,
                        ImoCiiBoundary.INFERIOR: 1.44 * required_cii_in_year
                    },
                    year
                )
            else:
                return ShipDdVectorBoundaries(
                    ship.ship_type,
                    WeightClassification(0, 65000 - 1),
                    CapacityUnit.DWT,
                    {
                        ImoCiiBoundary.SUPERIOR: 0.85 * required_cii_in_year,
                        ImoCiiBoundary.LOWER: 0.95 * required_cii_in_year,
                        ImoCiiBoundary.UPPER: 1.06 * required_cii_in_year,
                        ImoCiiBoundary.INFERIOR: 1.25 * required_cii_in_year
                    },
                    year
                )
        elif ship.ship_type == ShipType.TANKER:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.DWT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.82 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.93 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.08 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.28 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.CONTAINER_SHIP:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.DWT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.83 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.94 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.07 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.19 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.GENERAL_CARGO_SHIP:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.DWT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.83 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.94 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.06 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.19 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.REFRIGERATED_CARGO_CARRIER:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.DWT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.78 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.91 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.07 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.20 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.COMBINATION_CARRIER:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.DWT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.87 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.96 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.06 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.14 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.LNG_CARRIER:
            if ship.deadweight_tonnage >= 100000:
                return ShipDdVectorBoundaries(
                    ship.ship_type,
                    WeightClassification(100000, float('inf')),
                    CapacityUnit.DWT,
                    {
                        ImoCiiBoundary.SUPERIOR: 0.89 * required_cii_in_year,
                        ImoCiiBoundary.LOWER: 0.98 * required_cii_in_year,
                        ImoCiiBoundary.UPPER: 1.06 * required_cii_in_year,
                        ImoCiiBoundary.INFERIOR: 1.13 * required_cii_in_year
                    },
                    year
                )
            else:
                return ShipDdVectorBoundaries(
                    ship.ship_type,
                    WeightClassification(0, 100000 - 1),
                    CapacityUnit.DWT,
                    {
                        ImoCiiBoundary.SUPERIOR: 0.78 * required_cii_in_year,
                        ImoCiiBoundary.LOWER: 0.92 * required_cii_in_year,
                        ImoCiiBoundary.UPPER: 1.10 * required_cii_in_year,
                        ImoCiiBoundary.INFERIOR: 1.37 * required_cii_in_year
                    },
                    year
                )
        elif ship.ship_type == ShipType.RORO_CARGO_SHIP_VEHICLE_CARRIER:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.GT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.86 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.94 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.06 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.16 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.RORO_CARGO_SHIP:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.GT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.76 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.89 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.08 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.27 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.RORO_PASSENGER_SHIP:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.GT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.76 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.92 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.14 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.30 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.RORO_PASSENGER_SHIP_HIGH_SPEED_SOLAS:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.GT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.76 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.92 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.14 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.30 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.CRUISE_PASSENGER_SHIP:
            return ShipDdVectorBoundaries(
                ship.ship_type,
                WeightClassification(0, float('inf')),
                CapacityUnit.GT,
                {
                    ImoCiiBoundary.SUPERIOR: 0.87 * required_cii_in_year,
                    ImoCiiBoundary.LOWER: 0.95 * required_cii_in_year,
                    ImoCiiBoundary.UPPER: 1.06 * required_cii_in_year,
                    ImoCiiBoundary.INFERIOR: 1.16 * required_cii_in_year
                },
                year
            )
        elif ship.ship_type == ShipType.UNKNOWN:
            raise ValueError(f"Ship type '{ship.ship_type}' not supported")
        else:
            raise ValueError(f"Ship type '{ship.ship_type}' not supported")

    def _validate_ship_tonnage_valid(self, ship: Ship) -> None:
        """
        Checks that the ship tonnage is valid for the ship type.
        
        Args:
            - ship: The ship object to validate
            
        Raises:
            - ValueError: If the ship's tonnage is invalid for its type
        """
        dwt_ships = [
            ShipType.BULK_CARRIER,
            ShipType.GAS_CARRIER,
            ShipType.TANKER,
            ShipType.CONTAINER_SHIP,
            ShipType.GENERAL_CARGO_SHIP,
            ShipType.REFRIGERATED_CARGO_CARRIER,
            ShipType.COMBINATION_CARRIER,
            ShipType.LNG_CARRIER
        ]
        
        gt_ships = [
            ShipType.RORO_CARGO_SHIP_VEHICLE_CARRIER,
            ShipType.RORO_CARGO_SHIP,
            ShipType.RORO_PASSENGER_SHIP,
            ShipType.RORO_PASSENGER_SHIP_HIGH_SPEED_SOLAS,
            ShipType.CRUISE_PASSENGER_SHIP
        ]
        
        if ship.ship_type in dwt_ships:
            if ship.deadweight_tonnage <= 0:
                raise ValueError(
                    f"Deadweight tonnage must be greater than 0 for ship type {ship.ship_type}. "
                    f"Was provided {ship.deadweight_tonnage}"
                )
        elif ship.ship_type in gt_ships:
            if ship.gross_tonnage <= 0:
                raise ValueError(
                    f"Gross tonnage must be greater than 0 for ship type {ship.ship_type}. "
                    f"Was provided {ship.gross_tonnage}"
                )
        elif ship.ship_type == ShipType.UNKNOWN:
            raise ValueError(f"Ship type '{ship.ship_type}' not supported")
        else:
            raise ValueError(f"Ship type '{ship.ship_type}' not supported")
