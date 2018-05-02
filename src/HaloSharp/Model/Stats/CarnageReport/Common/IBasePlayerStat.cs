using System;
using HaloSharp.Model.Stats.Common;

namespace HaloSharp.Model.Stats.CarnageReport.Common
{
    public interface IBasePlayerStat : IBaseStat
    {
        TimeSpan AvgLifeTimeOfPlayer { get; set; }
        bool DNF { get; set; }
        FlexibleStats FlexibleStats { get; set; }
        Identity Player { get; set; }
        object PlayerScore { get; set; }
        object PostMatchRatings { get; set; }
        object PreMatchRatings { get; set; }
        int Rank { get; set; }
        int TeamId { get; set; }
    }
}