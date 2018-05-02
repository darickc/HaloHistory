using System.Collections.Generic;
using HaloSharp.Model.Stats.CarnageReport.Common;

namespace HaloHistory.Business.Models.Stats.Common
{
    public class ArenaPlayer : BasePlayer
    {
        public CreditsEarned CreditsEarned { get; set; }
        public CompetitiveSkillRanking CurrentCsr { get; set; }
        public CompetitiveSkillRanking PreviousCsr { get; set; }
        public List<Enemy> Enemies { get; set; }
        
        public int MeasurementMatchesLeft { get; set; }
        public XpInfo XpInfo { get; set; }

        public ArenaPlayer()
        {
            Enemies = new List<Enemy>();
        }
    }
}
