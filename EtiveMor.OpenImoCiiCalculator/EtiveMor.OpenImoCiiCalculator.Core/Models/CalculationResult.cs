using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
using EtiveMor.OpenImoCiiCalculator.Core.Models.MeasurementModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtiveMor.OpenImoCiiCalculator.Core.Models
{
    public class CalculationResult
    {

        public CalculationResult(IEnumerable<ResultYear> results)
        {
            Results = results;
            
        }

        /// <summary>
        /// Contains a collection of CII Ratings for each year
        /// between 2019 and 2030
        /// </summary>
        public IEnumerable<ResultYear> Results { get; set; }


    }


    public class ResultYear
    {
        /// <summary>
        /// Indicates if this year is a measured year,
        /// 
        /// if true, the CII rating, and all other values are measured
        /// if false, the CII rating, and all other values are estimates
        /// 
        /// If true, the <see cref="CalculatedCo2eEmissions"/>, <see cref="CalculatedShipCapacity"/>, and 
        /// <see cref="CalculatedTransportWork"/> will all be generated against this year. If false, these
        /// properties are equivalent to the most recent year data was provided for (for example, if this year is
        /// 2026, and data exists for 2020 and 2021, the properties will match the 2021 data).
        /// </summary>
        public bool IsMeasuredYear { get; set; }

        /// <summary>
        /// Indicates if this year is an estimated year, 
        /// 
        /// if true, the CII rating, and all other values are estimates
        /// if false, the CII rating, and all other values are measured
        /// </summary>
        public bool IsEstimatedYear { get { return !IsMeasuredYear; } }
        
        /// <summary>
        /// The year this result references
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// The ship's IMO CII Rating, from A to E
        /// </summary>
        public ImoCiiRating Rating { get; set; }

        /// <summary>
        /// The ship's required carbon intensity for this year
        /// </summary>
        public double RequiredCii { get; set; }

        /// <summary>
        /// The ship's attained Carbon Intensity Indicator for this year
        /// </summary>
        public double AttainedCii { get; set; }

        /// <summary>
        /// The Co2e Emissions calculated for this year
        /// </summary>
        public double CalculatedCo2eEmissions { get; set; }
        /// <summary>
        /// The Ship Capacity calculated for this year
        /// </summary>
        public double CalculatedShipCapacity { get; set; }
        /// <summary>
        /// The Transport Work calculated for this year
        /// </summary>
        public double CalculatedTransportWork { get; set; }

        /// <summary>
        /// This is the ratio of Attained:Required CII
        /// </summary>
        public double AttainedRequiredRatio { get 
            {
                return AttainedCii / RequiredCii;
            }
        }

        /// <summary>
        /// The VectorBoundaries for this ship/year
        /// </summary>
        public required ShipDdVectorBoundaries VectorBoundariesForYear { get; set; }
    }
}
