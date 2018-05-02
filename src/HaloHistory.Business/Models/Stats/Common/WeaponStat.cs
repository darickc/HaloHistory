using HaloHistory.Business.Utilities;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Models.Stats.Common
{
    public class WeaponStat : HaloSharp.Model.Stats.Common.WeaponStat
    {
        public new Weapon Weapon { get; set; }
        public int KillPercent { get; set; }
        public int HeadShotPercent { get; set; }
        public int TotalDamagePercent { get; set; }
        public int ShotsFiredPercent { get; set; }
        public int ShotsLandedPercent { get; set; }
        public int AccuracyPercent { get; set; }

        public int Accuracy => TotalShotsLanded.Percent(TotalShotsFired);
    }
}
