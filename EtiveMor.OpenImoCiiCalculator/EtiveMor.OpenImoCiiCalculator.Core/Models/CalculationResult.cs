using EtiveMor.OpenImoCiiCalculator.Core.Models.Enums;
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
        /// </summary>
        public bool IsMeasuredYear { get; set; }

        /// <summary>
        /// Indicates if this year is an estimated year, 
        /// 
        /// if true, the CII rating, and all other values are estimates
        /// if false, the CII rating, and all other values are measured
        /// </summary>
        public bool IsEstimatedYear { get { return !IsMeasuredYear; } }
        public int Year { get; set; }
        public ImoCiiRating Rating { get; set; }
        public double RequiredCii { get; set; }
        public double AttainedCii { get; set; }

        /// <summary>
        /// This is the ratio of Attained:Required CII
        /// </summary>
        public double AttainedRequiredRatio { get 
            {
                return AttainedCii / RequiredCii;
            }
        }

        public required Dictionary<ImoCiiBoundary, double> Boundaries { get; set; }
    }
}
