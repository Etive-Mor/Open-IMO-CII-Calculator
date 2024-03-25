# Open IMO Carbon Intensity Indicator (CII) Calculator

## What is this?

An unofficial open source implementation of the International Maritime Organisation (IMO)'s Carbon Intensity Indicator (CII). The specification for this software can be found in [IMO's resoluton MEPC.337(76)](https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.337(76).pdf), adopted in June 2021.


# Methodology

## Ship Capacity Methodology

A ships capacity is measured by either its Deadweight Tonnage (DWT) or Gross Tonnage (GT). The only exception is `Bulk Carriers`, which have a capacity capped at 279,000. 

To calculate a ship's Capacity in accordance with the IMO's MEPC337(76) guidelines:

**Method accepts**:
- `shipType`, an enum, derrived from *Table 1*'s *Ship Type* column
- `deadweightTonnage`, the *deadweight tonnage* of the ship, provided in long tons
- `grossTonnage`, the *gross tonnage* of the ship, provided in long tons

**Method returns**:
- a `double` representing the ship's capacity in imperial *long tons*

**Implementation**:

- If the ship is a `Bulk Carrier`, and its DWT is 279,000 or above, its capacity is capped at 279,000
- If the ship is a `Bulk Carrier`, and its DWT is below 279,000, its capacity is equal to its DWT
- If the ship is a `Ro-ro cargo ship (vehicle carrier)`, a `Ro-ro passenger ship` or a `Cruise passenger ship`, its capacity is equal to its Gross Tonnage
- Otherwise, the ships capacity is equal to its DWT

The full implementation detail can be found in **Table 1**'s *Ship Type*, *Ship weight*, and *Capacity* columns.

**Exceptions**:

- `ArgumentOutOfRangeException` is thrown if the DWT is set to 0, when ship type is set to anything other than `Ro-ro cargo ship (vehicle carrier)`, `Ro-ro passenger ship` or `Cruise passenger ship`
- `ArgumentOutOfRangeException` is thrown if the GT is set to 0, when ship type is set to `Ro-ro cargo ship (vehicle carrier)`, `Ro-ro passenger ship` or `Cruise passenger ship`


---



## Table 1: MEPC337(76) Shipping Capacity
Ship Type | Ship weight param (optional) | Capacity | $a$ | $c$
-- | -- | -- | -- | --
Bulk carrier | 279,000 DWT and above | 279,000 | 4,745 | 0.622
Bulk carrier | Less than 279,000 | DWT | 4,745 | 0.622
Gas carrier | 65,000 and above | DWT | 14405E7 | 2.071
Gas carrier | Less than 65,000 | DWT | 8,104 | 0.639
Tanker |  | DWT | 5,247 | 0.610
Container Ship |  | DWT | 1,984 | 0.489
General cargo ship | 20,000 DWT and above | DWT | 31,948 | 0.792
General cargo ship | Less than 20,000 DWT | DWT | 588 | 0.3885
Refrigerated cargo carrier | | DWT | 4,600 | 0.557
Combination carrier | | DWT | 40,853 | 0.812
LNG Carrier | 100,000 DWT and above | DWT | 9.827 | 0.000
LNG Carrier | 65,000 and above, less than 100,000 | DWT | 14479E10 | 2.673
LNG Carrier | less than 65,000 | DWT | 14479E10 | 2.673
Ro-ro cargo ship (vehicle carrier) |  | GT | 5,739 | 0.631
Ro-ro cargo ship |  | DWT | 10,952 | 0.637
Ro-ro passenger ship |  | GT | 7,540 | 0.587
Cruise passenger ship |  | GT | 930 | 0.383

Table source: [IMO: MEPC337(76)](https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.337(76).pdf)



## Conversion Factors

| Measure | Metric Measure | Notes |
| ------------- | ------------- | ------------- |
| Deadweight Tonne (DWT) | $1016.0469088kg$ |  |
| Gross Tonne (GT) | $1016.0469088kg$ |  |


## Shipping Terminology & Glossary

| Term | Description | Notes |
| ------------- | ------------- | ------------- |
| Carbon Dioxide Equivalent (CO2eq, CO2, CO2e) | A ship's carbon dioxide emissions | Expressed in this implementation in grams (metric) |
| Carbon Intensity Index (CII) | The relative measure of a ship's carbon dioxide emissions, taking distance travelled and fuel type used into account |  |
| Deadweight Tonnage (DWT) | The measure of a the total contents of a ship, including cargo, fuel, crew, passengers, and water (Excludes water in a ship's boiler) | Expressed in long tons (British Imperial) |
| Gross Tonnage (GT) | A ship's internal volume | Expressed in long tons (British Imperial) |
| International Maritime Organisation (IMO) | A UN Agency responsible for regulating maritime transport rules & regulations |  |
| Liquefied natural gas (LNG) | Gas, compressed into liquid form for easier transport |  |
| Resolution MEPC.337(76) | Internationally standardised reference guide to shipping carbon intensity |  |
| Roll-on-roll-off (Ro-ro, Roro, Ro ro) | A ship designed to take cargo which can be wheeled (or rolled) in and out of a cargo hold |  |

## References

- IMO: MEPC337(76): https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.337(76).pdf