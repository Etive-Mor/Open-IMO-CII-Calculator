import json
import os
import uuid
from open_imo_cii_calculator.ship_carbon_intensity_calculator import ShipCarbonIntensityCalculator
from open_imo_cii_calculator.models.fuel_type import TypeOfFuel
from open_imo_cii_calculator.models.ship_type import ShipType
from open_imo_cii_calculator.models.dto.fuel_type_consumption import FuelTypeConsumption

def ship_dd_vector_boundaries_to_dict(vb):
    return {
        "year": vb.year,
        "ship_type": vb.ship_type,
        "weight_classification": {
            "upper_limit": vb.weight_classification.upper_limit,
            "lower_limit": vb.weight_classification.lower_limit
        } if vb.weight_classification else None,
        "capacity_unit": vb.capacity_unit,
        "boundary_dd_vectors": vb.boundary_dd_vectors
    }

def result_year_to_dict(ry):
    return {
        "is_measured_year": ry.is_measured_year,
        "is_estimated_year": ry.is_estimated_year,
        "year": ry.year,
        "required_cii": ry.required_cii,
        "attained_cii": ry.attained_cii,
        "attained_required_ratio": ry.attained_required_ratio,
        "rating": int(ry.rating) if hasattr(ry.rating, 'value') else ry.rating,
        "calculated_co2e_emissions": ry.calculated_co2e_emissions,
        "calculated_ship_capacity": ry.calculated_ship_capacity,
        "calculated_transport_work": ry.calculated_transport_work,
        "vector_boundaries_for_year": ship_dd_vector_boundaries_to_dict(ry.vector_boundaries_for_year) if ry.vector_boundaries_for_year else None
    }

def calculation_result_to_dict(result):
    return {
        "results": [result_year_to_dict(ry) for ry in result.results]
    }

def single_fuel_gas_carrier_demo():
    print("---------------------")
    print("Generating a single-fuel Gas Carrier ship report...")
    print("---------------------")

    calculator = ShipCarbonIntensityCalculator()
    # Sample values for a Gas Carrier (DWT < 65,000)
    ship_type = ShipType.GAS_CARRIER
    gross_tonnage = 0
    deadweight_tonnage = 60000  # less than 65,000 DWT
    distance_travelled = 150_000
    fuel_type = TypeOfFuel.LIQUIFIED_NATURAL_GAS
    fuel_consumption_megatons = 10_000
    result = calculator.calculate_attained_cii_rating(
        ship_type=ship_type,
        gross_tonnage=gross_tonnage,
        deadweight_tonnage=deadweight_tonnage,
        distance_travelled=distance_travelled,
        fuel_type_consumptions=[FuelTypeConsumption(fuel_type, fuel_consumption_megatons * 1_000_000)],
        target_year=2019
    )
    result_dict = calculation_result_to_dict(result)
    print(json.dumps(result_dict, indent=2, default=str))
    # Write to file
    os.makedirs("demo-results", exist_ok=True)
    run_id = str(uuid.uuid4())
    out_path = os.path.join("demo-results", f"{run_id}-single-fuel-gas-carrier-demo.json")
    with open(out_path, "w") as f:
        json.dump(result_dict, f, indent=2, default=str)
    print(f"Result written to {out_path}")
    print("---------------------")
    print("Completed the single-fuel Gas Carrier ship report...")
    print("---------------------")
