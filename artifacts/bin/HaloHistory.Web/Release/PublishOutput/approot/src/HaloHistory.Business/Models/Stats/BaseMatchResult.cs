using System;
using HaloHistory.Business.Models.Stats.Common;
using HaloSharp.Model;
using HaloSharp.Model.Metadata;
using static HaloSharp.Model.Enumeration;

namespace HaloHistory.Business.Models.Stats
{
    public abstract class BaseMatchResult
    {
        public GameMode GameMode { get; set; }
        public Guid Id { get; set; }

        public string GameBaseVariantIconUrl { get; set; }
        public string GameBaseVariantName { get; set; }

        public string GameVariantInfoDescription { get; set; }
        public string GameVariantInfoName { get; set; }
        public string GameVariantInfoIconUrl { get; set; }
        public bool IsTeamGame { get; set; }
        public string MapDescription { get; set; }
        public string MapImageUrl { get; set; }
        public string MapName { get; set; }
        public string PlaylistDescription { get; set; }
        public string PlaylistImageUrl { get; set; }
        public string PlaylistName { get; set; }
        public string SeasonName { get; set; }
        public string MatchDuration { get; set; }
        public bool IsMatchOver { get; set; }
        public Enumeration.ResultType Result { get; set; }
        public TeamColor PlayerTeamColor { get; set; }
    }
}
