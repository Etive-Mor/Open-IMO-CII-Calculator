namespace EtiveMor.OpenImoCiiCalculator.Core.Extensions
{
    public static class ReductionFactorExtensions
    {
        /// <summary>
        /// Gets an annual reduction factor for a given year, according to MEPC.338(76)
        /// </summary>
        /// <param name="year">the calendar year being analysed</param>
        /// <returns>the reduction factor</returns>
        /// <exception cref="NotSupportedException">
        /// Thrown if a year outside of the range 2019-2030 (inclusive) is provided
        /// </exception>
        public static double GetAnnualReductionFactor(this int year)
        {
            switch (year)
            {
                case 2019:
                    return 0.00;
                case 2020:
                    return 0.01;
                case 2021:
                    return 0.02;
                case 2022:
                    return 0.03;
                case 2023:
                    return 0.05;
                case 2024:
                    return 0.07;
                case 2025:
                    return 0.09;
                case 2026:
                    return 0.11;
                case 2027:
                    return 0.13;
                case 2028:
                    return 0.15;
                case 2029:
                    return 0.17;
                case 2030:
                    return 0.19;
                default:
                    throw new NotSupportedException($"Year {year} is not supported");
            }
        }

        public static double ApplyAnnualReductionFactor(this double value, int year)
        {
            return value * (1 - year.GetAnnualReductionFactor());
        }
    }
}
