namespace HaloHistory.Business.Models.Stats.Common
{
    public class XpInfo : HaloSharp.Model.Stats.CarnageReport.Common.XpInfo
    {
        public int XpEarned { get; set; }
        public int XpToNextRank { get; set; }
        public int CurrentRankXp { get; set; }
        public int PreviousPercent { get; set; }
        public int CurrentPercent { get; set; }
    }
}
