using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;

namespace EtiveMor.OpenImoCiiCalculator.Core.Services.Impl
{
    internal class ShipMassOfCo2EmissionsCalculatorService : IShipMassOfCo2EmissionsCalculatorService
    {
        /// <summary>
        /// Gets the mass of CO2 emissions in grams (g) for a given fuel type and fuel consumption mass.
        /// </summary>
        /// <param name="fuelType">
        /// The fuel type in use by the ship's engine
        /// </param>
        /// <param name="fuelConsumptionMassInGrams">
        /// the cumulative mass of consumed fuel across the calendar year in grams (g)
        /// </param>
        /// <returns>
        /// A double representing the mass of CO2 emissions for a ship in a calendar year in grams (g)
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if an unsupported fuel type is provided
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if fuelConsumptionMassInGrams is less than or equal to zero
        /// </exception>
        public double GetMassOfCo2Emissions(TypeOfFuel fuelType, double fuelConsumptionMassInGrams)
        {
            if (fuelConsumptionMassInGrams < 0)
            {
                throw new ArgumentOutOfRangeException("Fuel consumption mass must be a positive value",
                    nameof(fuelConsumptionMassInGrams));
            }
            double fuelMassConversionFactor = GetFuelMassConversionFactor(fuelType);

            var massOfCo2Emissions = fuelConsumptionMassInGrams * fuelMassConversionFactor;

            return massOfCo2Emissions;
        }


        /// <summary>
        /// Gets a fuel type's mass conversion factor in accordance with MEPC.364(79)
        /// </summary>
        /// <param name="fuelType">
        /// The fuel type to get the conversion factor for
        /// </param>
        /// <returns>
        /// A double representing the fuel mass conversion factor for the given fuel type
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if an unsupported fuel type is provided
        /// </exception>
        /// <remarks>
        /// Emissions mass conversion factors are outlined in IMO MEPC.364(79)
        /// 
        /// https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf
        /// </remarks>
        public double GetFuelMassConversionFactor(TypeOfFuel fuelType)
        {
            return fuelType switch
            {
                TypeOfFuel.DIESEL_OR_GASOIL => 3.206,
                TypeOfFuel.LIGHTFUELOIL => 3.151,
                TypeOfFuel.HEAVYFUELOIL => 3.114,
                TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE => 3.000,
                TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE => 3.030,
                TypeOfFuel.ETHANE => 2.927,
                TypeOfFuel.LIQUIFIEDNATURALGAS => 2.750,
                TypeOfFuel.METHANOL => 1.375,
                TypeOfFuel.ETHANOL => 1.913,
                _ => throw new ArgumentException("Unsupported fuel type", nameof(fuelType))
            };
        }


        /// <summary>
        /// Gets a fuel type's carbon content in accordance with MEPC.364(79)
        /// </summary>
        /// <param name="fuelType">
        /// The fuel type to get the carbon content for
        /// </param>
        /// <returns>
        /// A double representing the carbon content for the given fuel type
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if an unsupported fuel type is provided
        /// </exception>
        /// <remarks>
        /// carbon content data are outlined in IMO MEPC.364(79)
        /// 
        /// https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf
        /// </remarks>
        public double GetFuelCarbonContent(TypeOfFuel fuelType)
        {
            return fuelType switch
            {
                TypeOfFuel.DIESEL_OR_GASOIL => 0.8744,
                TypeOfFuel.LIGHTFUELOIL => 0.8594,
                TypeOfFuel.HEAVYFUELOIL => 0.8493,
                TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE => 0.8182,
                TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE => 0.8264,
                TypeOfFuel.ETHANE => 0.7989,
                TypeOfFuel.LIQUIFIEDNATURALGAS => 0.7500,
                TypeOfFuel.METHANOL => 0.3750,
                TypeOfFuel.ETHANOL => 0.5217,
                _ => throw new ArgumentException("Unsupported fuel type", nameof(fuelType))
            };
        }

        /// <summary>
        /// Gets the lower calorific value for a given fuel type in accordance with MEPC.364(79)
        /// </summary>
        /// <param name="fuelType">
        /// The fuel type to get the lower calorific value for
        /// </param>
        /// <returns>
        /// A double representing the lower calorific value for the given fuel type
        /// </returns>
        /// <exception cref="ArgumentException"></exception>
        /// <remarks>
        /// Fuel calorific values are outlined in IMO MEPC.364(79)
        /// 
        /// https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf
        /// </remarks>
        public double GetFuelLowerCalorificValue(TypeOfFuel fuelType)
        {
            return fuelType switch
            {
                TypeOfFuel.DIESEL_OR_GASOIL => 42700,
                TypeOfFuel.LIGHTFUELOIL => 41200,
                TypeOfFuel.HEAVYFUELOIL => 40200,
                TypeOfFuel.LIQUIFIEDPETROLEUM_PROPANE => 46300,
                TypeOfFuel.LIQUIFIEDPETROLEUM_BUTANE => 45700,
                TypeOfFuel.ETHANE => 46400,
                TypeOfFuel.LIQUIFIEDNATURALGAS => 48000,
                TypeOfFuel.METHANOL => 19900,
                TypeOfFuel.ETHANOL => 26800,
                _ => throw new ArgumentException("Unsupported fuel type", nameof(fuelType))
            };
        }

    }
}
