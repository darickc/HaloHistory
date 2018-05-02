using System.Collections.Generic;
using HaloSharp.Model.Metadata;
using HaloSharp.Model.Stats.CarnageReport.Common;

namespace HaloHistory.Business.Models.Stats.Common
{
    public class Team : TeamStat
    {
        public List<ArenaPlayer> Players { get; set; }
        //public TeamColor TeamColor { get; set; }

        public Team()
        {
            Players = new List<ArenaPlayer>();
        }
    }
}
