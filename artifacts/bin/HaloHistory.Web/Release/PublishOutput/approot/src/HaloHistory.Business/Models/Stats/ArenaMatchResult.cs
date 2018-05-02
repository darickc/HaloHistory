using System.Collections.Generic;
using HaloHistory.Business.Models.Stats.Common;

namespace HaloHistory.Business.Models.Stats
{
    public class ArenaMatchResult : BaseMatchResult
    {
        public List<Team> Teams { get; set; }
    }
}
