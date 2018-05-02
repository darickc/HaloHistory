namespace HaloHistory.Business.Models.Stats.Common
{
    public class Enemy
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Kills { get; set; }
        public int Deaths { get; set; }
        public string ImageUrl { get; set; }
        public string EmpblemImageUrl { get; set; }

        public int KillPercent { get; set; }
        public int DeathPercent { get; set; }
        public bool IsPlayer { get; set; }
        public int Spread { get; set; }
        public int SpreadPercent { get; set; }
        public string SpreadSign { get; set; }
    }
}
