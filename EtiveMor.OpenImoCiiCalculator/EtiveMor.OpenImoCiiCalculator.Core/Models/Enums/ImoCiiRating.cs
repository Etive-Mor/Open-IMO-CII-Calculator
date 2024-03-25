namespace EtiveMor.OpenImoCiiCalculator.Core.Models.Enums
{
    /// <summary>
    /// An enum describing the possible IMO Carbon Intensity Indicator (CII) ratings
    /// 
    /// 0 indicates an error
    /// 
    /// A indicates the best rating
    /// B indicates the second best rating
    /// C indicates the third best rating
    /// D indicates the fourth best rating
    /// E indicates the worst rating
    /// 
    /// </summary>
    public enum ImoCiiRating
    {
        ERR = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
    }
}
