namespace HaloHistory.Business.Models.Stats.Common
{
    public class CompetitiveSkillRanking 
    {
        public string Name { get; set; }

        public string BannerImageUrl { get; set; }

        public string IconImageUrl { get; set; }

        public int? Tier { get; set; }

        public int? PercentToNextTier { get; set; }

        public int? Rank { get; set; }
        public int DesignationId { get; set; }
        public bool Increased { get; set; }
    }
}
