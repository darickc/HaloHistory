using System.Collections.Generic;
using HaloSharp.Model.Metadata;

namespace HaloSharp.Model.Stats.CarnageReport.Common
{
    public interface ICompetetivePlayer : IPlayer
    {
        CreditsEarned CreditsEarned { get; set; }
        List<MetaCommendationDelta> MetaCommendationDeltas { get; set; }
        List<ProgressiveCommendationDelta> ProgressiveCommendationDeltas { get; set; }
        List<RewardSet> RewardSets { get; set; }
        SpartanRank CurrentSpartanRank { get; set; }
        SpartanRank NextSpartanRank { get; set; }
        XpInfo XpInfo { get; set; }
    }
}
