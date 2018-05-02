using System;
using System.Collections.Generic;
using HaloSharp.Model;
using HaloSharp.Model.Metadata;
using HaloSharp.Model.Stats;

namespace HaloHistory.Business.Models.Stats
{
    public class Result
    {
        public string GameBaseVariantIconUrl { get; set; }
        public string GameBaseVariantName { get; set; }

        public string GameVariantInfoDescription { get; set; }
        public string GameVariantInfoName { get; set; }
        public string GameVariantInfoIconUrl { get; set; }

        public Guid MatchId { get; set; }
        public Enumeration.GameMode GameMode { get; set; }
        public string PlaylistDescription { get; set; }
        public string PlaylistImageUrl { get; set; }
        public string PlaylistName { get; set; }
        public bool IsTeamGame { get; set; }
        public string MapDescription { get; set; }
        public string MapImageUrl { get; set; }
        public string MapName { get; set; }
        //public string MapVariantInfoDescription { get; set; }
        //public string MapVariantInfoName { get; set; }
        //public string MapVariantInfoMapImageUrl { get; set; }
        public string CompletedDate { get; set; }
        public string MatchDuration { get; set; }
        public Player Player { get; set; }
        public TeamColor PlayerTeamColor { get; set; }
        public List<Team> Teams { get; set; }

    }
}
