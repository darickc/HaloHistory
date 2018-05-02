using System.Collections.Generic;
using HaloSharp.Model.Stats;

namespace HaloHistory.Business.Models.Stats
{
    public class MatchSet
    {
        public int Count { get; set; }
        public int ResultsCount { get; set; }
        public int Start { get; set; }

        public List<Result> Results { get; set; }
    }
}
