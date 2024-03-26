# Open IMO Carbon Intensity Indicator (CII) Calculator 🚢

## What is this?

An unofficial open source implementation of the International Maritime Organisation (IMO)'s Carbon Intensity Indicator (CII). The specification for this software can be found in [IMO's resoluton MEPC.337(76)](https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.337(76).pdf), adopted in June 2021.


# Methodology

## Ship $CO_2$ Emissions Methodology

The sum of a ship's $CO_2$ emissions over a given year are calculated by multiplying the mass of consumed fuel by the fuel's emissions factor. 

**Method Accepts**:
- `fuelType`, an enum derrived from [Table 2](#table-2-mepc36479-mass-conversion-between-fuel-consumption-and-co_2-emissions)'s *Fuel Type* column
- `fuelConsumptionMass`, a `long` representing the mass of fuel consumed in grams (g) over the given year


**Method Returns**:

- A `decimal` representing the $M$ mass of $CO_2$ emitted by the ship across one calendar year

**Implementation**:

The sum of $CO_2$ emissions $M$ from fuel consumption in a given calendar year is 

$M = FC_j \times C_{f_j}$

Where: 
- $j$ is the fuel type
- $FC_j$ is the mass in grams of the consumed fuel type `j` in one calendar year
- $C_{f_j}$ is the fuel oil mass to CO2 mass conversion factor, given in Table 2's $C_F$ column



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

$Capacity$ of a given ship is calculated using the following rules:

- If the ship is a `Bulk Carrier`, and its DWT is 279,000 or above, its capacity is capped at 279,000
- If the ship is a `Bulk Carrier`, and its DWT is below 279,000, its capacity is equal to its DWT
- If the ship is a `Ro-ro cargo ship (vehicle carrier)`, a `Ro-ro passenger ship` or a `Cruise passenger ship`, its capacity is equal to its Gross Tonnage
- Otherwise, the ships capacity is equal to its DWT

The full implementation detail can be found in **[Table 1](#table-1-mepc33776-shipping-capacity)**'s *Ship Type*, *Ship weight*, and *Capacity* columns.

**Exceptions**:

- `ArgumentOutOfRangeException` is thrown if the DWT is set to 0, when ship type is set to anything other than `Ro-ro cargo ship (vehicle carrier)`, `Ro-ro passenger ship` or `Cruise passenger ship`
- `ArgumentOutOfRangeException` is thrown if the GT is set to 0, when ship type is set to `Ro-ro cargo ship (vehicle carrier)`, `Ro-ro passenger ship` or `Cruise passenger ship`


---

# Reference Tables

## Table 1: MEPC.337(76) Shipping Capacity

The following table describes how to determine a given ship type's *Capacity*.

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

Table source: [IMO: MEPC.337(76)](https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.337(76).pdf)

---

## Table 2: MEPC.364(79) Mass Conversion between fuel consumption and $CO_2$ emissions

The following table describes how to convert from the fuel used by a ship's main engine $ME_{(i)}$ to the amount of $CO_2$ produced. Fuel consumption is measured in grams (g), as is the output $CO_2$ emission

| ID | Fuel Type | Carbon Content | $C_F (\frac{t-CO_2}{t-Fuel})$ | Lower calorific value (kJ/kg) | Source/Reference |
| ------------- | ------------- | -------------- | ------------- | -------------- | -------------- |
| 1  | Diesel / Gas Oil | 0.8744 | 3.206 | 42,700 | ISO 8217 Grade DMX to DMB |
| 2 | Light Fuel Oil (LFO) | 0.8594 | 3.151 | 41,200 | ISO 8217 Grade RMA to RMD |
| 3 | Heavy Fuel Oil (HFO) | 0.8493 | 3.114 | 40,200 | ISO 8217 Grade RME to RMK |
| 4a | Liquified Petroleum (Propane) | 0.8182 | 3.000 | 46,300 | Propane |
| 4b | Liquified Petroleum (Butane) | 0.8264 | 3.030 | 45,700 | Butane |
| 5 | Ethane | 0.7989 | 2.927 | 46,400 |  |
| 6 | Liquified Natural Gas (LNG) | 0.7500 | 2.750 | 48,000 | n/a |
| 7 | Methanol | 0.3750 | 1.375 | 19,900 | n/a |
| 8 | Ethanol | 0.5217 | 1.913 | 26,800 | n/a |

Table source: [IMO: MEPC.364(79)](https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf)

---


## Table 3: Common shipping measurement conversions

Often in shipping, non-metric measurements are used. Conversions are detailed below

| Measure | Metric Measure | Notes |
| ------------- | ------------- | ------------- |
| Deadweight Tonne (DWT) | $1016.0469088kg$ | DWT is a ship's total weight excluding boiler water, measured in Imperial long tons |
| Gross Tonne (GT) | $1016.0469088kg$ | GT is a ship's area, measured in Imperial long tons |


# Shipping Terminology & Glossary

| Term | Description | Notes |
| ------------- | ------------- | ------------- |
| Carbon Dioxide Equivalent (CO2eq, CO2, CO2e, $CO_2$) | A ship's carbon dioxide emissions | Expressed in this implementation in grams (metric) |
| Carbon Intensity Index (CII) | The relative measure of a ship's carbon dioxide emissions, taking distance travelled and fuel type used into account |  |
| Deadweight Tonnage (DWT) | The measure of a the total contents of a ship, including cargo, fuel, crew, passengers, and water (Excludes water in a ship's boiler) | Expressed in long tons (British Imperial) |
| Final Draft International Standard (FDIS) | A draft status for an ISO Standard, indicating the standard is in its final stage of approval |  |
| Gross Tonnage (GT) | A ship's internal volume | Expressed in long tons (British Imperial) |
| International Maritime Organisation (IMO) | A UN Agency responsible for regulating maritime transport rules & regulations |  |
| International Organization for Standardization (ISO) | Independent, non-governmental, international standard development organization |  |
| Liquefied natural gas (LNG) | Gas, compressed into liquid form for easier transport |  |
| Resolution MEPC.337(76) | Internationally standardised reference guide to shipping carbon intensity |  |
| Roll-on-roll-off (Ro-ro, Roro, Ro ro) | A ship designed to take cargo which can be wheeled (or rolled) in and out of a cargo hold |  |





# References & datasets

- IMO: MEPC.337(76) - Carbon Intensity Index (CII) spec: https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.337(76).pdf
- IMO: MEPC.364(79) - Energy Efficiency Design Index (EEDI) spec: https://wwwcdn.imo.org/localresources/en/KnowledgeCentre/IndexofIMOResolutions/MEPCDocuments/MEPC.364(79).pdf
- ISO 8217:2017 (Current standard) - Petroleum products, Fuels (class F), Specifications of marine fuels: https://www.iso.org/standard/64247.html
- ISO/FDIS 8217 (Standard under development) - Products from petroleum, synthetic and renewable sources, Fuels (class F), Specifications of marine fuel: https://www.iso.org/standard/80579.html

## Further Reading

- Society of Naval Architecture Students summary of CII Calculations: https://github.com/snascusat/CII-Calculator
- DNV's summary of EEXI and CII requirements: https://www.dnv.com/news/eexi-and-cii-requirements-taking-effect-from-1-january-2023-237817/


## Useful datasets (miced public and private)

- UNStats (public, non-commercial dataset): https://unstats.un.org/bigdata/task-teams/ttt-dashboards/
- Dataliastic (private commercial dataset): https://datalastic.com/pricing/
- Marine Traffic (private commercial dataset): https://servicedocs.marinetraffic.com/