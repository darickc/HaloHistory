using System;
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
        public string MapVariantInfoName { get; set; }
        public string MapVariantInfoMapImageUrl { get; set; }
        public string MapVariantInfoDescription { get; set; }
        public string PlaylistDescription { get; set; }
        public string PlaylistImageUrl { get; set; }
        public string PlaylistName { get; set; }
        public string SeasonName { get; set; }
        public string MatchDuration { get; set; }
        public bool IsMatchOver { get; set; }
        public bool IsCtf { get; set; }
        public bool IsStrongholds { get; set; }
        public ResultType Result { get; set; }
        public TeamColor PlayerTeamColor { get; set; }
    }
}
