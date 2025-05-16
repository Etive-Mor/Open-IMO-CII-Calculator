"""
Service for calculating ship mass of CO2 emissions
"""
from typing import Dict

from open_imo_cii_calculator.models.fuel_type import TypeOfFuel


class ShipMassOfCo2EmissionsCalculatorService:
    """
    Service for calculating the mass of CO2 emissions for a ship
    """
    
    def get_mass_of_co2_emissions(self, fuel_type: TypeOfFuel, fuel_consumption_mass_in_grams: float) -> float:
        """
        Gets the mass of CO2 emissions in grams (g) for a given fuel type and fuel consumption mass.
        
        Args:
            - fuel_type (TypeOfFuel): The fuel type in use by the ship's engine
            - fuel_consumption_mass_in_grams (float): The cumulative mass of consumed fuel across the calendar year in grams (g)
        
        Returns:
            - float: The mass of CO2 emissions for a ship in a calendar year in grams (g)
        
        Raises:
            - ValueError: If fuel_consumption_mass_in_grams is less than or equal to zero
            - ValueError: If an unsupported fuel type is provided
        """
        if fuel_consumption_mass_in_grams < 0:
            raise ValueError("Fuel consumption mass must be a positive value")
            
        fuel_mass_conversion_factor = self.get_fuel_mass_conversion_factor(fuel_type)
        mass_of_co2_emissions = fuel_consumption_mass_in_grams * fuel_mass_conversion_factor
        
        return mass_of_co2_emissions
    
    def get_fuel_mass_conversion_factor(self, fuel_type: TypeOfFuel) -> float:
        """
        Gets a fuel type's mass conversion factor in accordance with MEPC.364(79)
        
        Args:
            - fuel_type (TypeOfFuel): The fuel type to get the conversion factor for
        
        Returns:
            - float: The fuel mass conversion factor for the given fuel type
        
        Raises:
            - ValueError: If an unsupported fuel type is provided
            
        Note:
            Emissions mass conversion factors are outlined in IMO MEPC.364(79)
            https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf
        """
        conversion_factors = {
            TypeOfFuel.DIESEL_OR_GASOIL: 3.206,
            TypeOfFuel.LIGHTFUELOIL: 3.151,
            TypeOfFuel.HEAVYFUELOIL: 3.114,
            TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE: 3.000,
            TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE: 3.030,
            TypeOfFuel.ETHANE: 2.927,
            TypeOfFuel.LIQUIFIEDNATURALGAS: 2.750,
            TypeOfFuel.METHANOL: 1.375,
            TypeOfFuel.ETHANOL: 1.913
        }
        
        if fuel_type not in conversion_factors:
            raise ValueError(f"Unsupported fuel type: {fuel_type}")
            
        return conversion_factors[fuel_type]
    
    def get_fuel_carbon_content(self, fuel_type: TypeOfFuel) -> float:
        """
        Gets a fuel type's carbon content in accordance with MEPC.364(79)
        
        Args:
            - fuel_type: The fuel type to get the carbon content for
            
        Returns:
            - float: The carbon content for the given fuel type
            
        Raises:
            - ValueError: If an unsupported fuel type is provided
            
        Note:
            Carbon content data are outlined in IMO MEPC.364(79)
            https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf
        """
        carbon_contents = {
            TypeOfFuel.DIESEL_OR_GASOIL: 0.8744,
            TypeOfFuel.LIGHTFUELOIL: 0.8594,
            TypeOfFuel.HEAVYFUELOIL: 0.8493,
            TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE: 0.8182,
            TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE: 0.8264,
            TypeOfFuel.ETHANE: 0.7989,
            TypeOfFuel.LIQUIFIEDNATURALGAS: 0.7500,
            TypeOfFuel.METHANOL: 0.3750,
            TypeOfFuel.ETHANOL: 0.5217
        }
        
        if fuel_type not in carbon_contents:
            raise ValueError(f"Unsupported fuel type: {fuel_type}")
            
        return carbon_contents[fuel_type]
    
    def get_fuel_lower_calorific_value(self, fuel_type: TypeOfFuel) -> float:
        """
        Gets the lower calorific value for a given fuel type in accordance with MEPC.364(79)
        
        Args:
            - fuel_type: The fuel type to get the lower calorific value for
            
        Returns:
            - float: The lower calorific value for the given fuel type
            
        Raises:
            - ValueError: If an unsupported fuel type is provided
            
        Note:
            Fuel calorific values are outlined in IMO MEPC.364(79)
            https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf
        """
        calorific_values = {
            TypeOfFuel.DIESEL_OR_GASOIL: 42700,
            TypeOfFuel.LIGHTFUELOIL: 41200,
            TypeOfFuel.HEAVYFUELOIL: 40200,
            TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE: 46300,
            TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE: 45700,
            TypeOfFuel.ETHANE: 46400,
            TypeOfFuel.LIQUIFIEDNATURALGAS: 48000,
            TypeOfFuel.METHANOL: 19900,
            TypeOfFuel.ETHANOL: 26800
        }
        
        if fuel_type not in calorific_values:
            raise ValueError(f"Unsupported fuel type: {fuel_type}")
            
        return calorific_values[fuel_type]