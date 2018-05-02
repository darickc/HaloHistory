using System.Collections.Generic;
using HaloSharp.Model.Stats.CarnageReport.Common;

namespace HaloSharp.Model.Stats.CarnageReport
{
    public interface ITeamMatch
    {
        List<TeamStat> TeamStats { get; set; }
    }
}