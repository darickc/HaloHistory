using System.Collections.Generic;
using HaloSharp.Model;

namespace HaloHistory.Business.Models.Stats.Common
{
    public class MedalClassification
    {
        public Enumeration.MedalType Classification { get; set; }
        public List<MedalAward> MedalAwards { get; set; }
    }
}
