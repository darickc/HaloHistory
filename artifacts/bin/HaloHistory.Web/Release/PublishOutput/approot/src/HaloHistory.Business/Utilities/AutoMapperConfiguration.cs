using AutoMapper;
using HaloHistory.Business.Models.Stats;
using HaloHistory.Business.Models.Stats.Common;
using HaloSharp.Model.Stats.CarnageReport;
using HaloSharp.Model.Stats.CarnageReport.Common;
using Team = HaloHistory.Business.Models.Stats.Common.Team;
using XpInfo = HaloHistory.Business.Models.Stats.Common.XpInfo;

namespace HaloHistory.Business.Utilities
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<TeamStat, Team>();
                //x.CreateMap<BaseMatchResult, BaseMatch>();
                x.CreateMap<ArenaMatch, ArenaMatchResult>()
                    .ForMember(d => d.MatchDuration, opt => opt.MapFrom(s => s.TotalDuration.ToString(@"hh\:mm\:ss")));
                x.CreateMap<CustomMatch, ArenaMatchResult>()
                    .ForMember(d => d.MatchDuration, opt => opt.MapFrom(s => s.TotalDuration.ToString(@"hh\:mm\:ss")));
                x.CreateMap<CampaignMatch, ArenaMatchResult>()
                    .ForMember(d => d.MatchDuration, opt => opt.MapFrom(s => s.TotalDuration.ToString(@"hh\:mm\:ss")));
                x.CreateMap<WarzoneMatch, ArenaMatchResult>()
                    .ForMember(d => d.MatchDuration, opt => opt.MapFrom(s => s.TotalDuration.ToString(@"hh\:mm\:ss")));
                x.CreateMap<ArenaMatchPlayerStat, ArenaPlayer>()
                    .ForMember(d => d.GamerTag, opt => opt.MapFrom(s => s.Player.Gamertag));
                x.CreateMap<WarzonePlayerStat, ArenaPlayer>()
                    .ForMember(d => d.GamerTag, opt => opt.MapFrom(s => s.Player.Gamertag));
                x.CreateMap<CustomMatchPlayerStat, ArenaPlayer>()
                    .ForMember(d => d.GamerTag, opt => opt.MapFrom(s => s.Player.Gamertag));
                x.CreateMap<CampaignMatchPlayerStat, ArenaPlayer>()
                   .ForMember(d => d.GamerTag, opt => opt.MapFrom(s => s.Player.Gamertag));
                x.CreateMap<HaloSharp.Model.Stats.Common.CompetitiveSkillRanking, CompetitiveSkillRanking>();
                x.CreateMap<HaloSharp.Model.Stats.Common.MedalAward, MedalAward>();
                x.CreateMap<HaloSharp.Model.Stats.Common.WeaponStat, WeaponStat>();
                x.CreateMap<HaloSharp.Model.Stats.MatchSet, MatchSet>();
                x.CreateMap<HaloSharp.Model.Stats.Result, Result>()
                    .ForMember(d=>d.CompletedDate,opt=>opt.MapFrom(s=>s.MatchCompletedDate.ISO8601Date.ToString("M/d/yyyy")))
                    .ForMember(d => d.MatchDuration, opt => opt.MapFrom(s => s.MatchDuration.ToString(@"hh\:mm\:ss")))
                    .ForMember(d => d.MatchId, opt => opt.MapFrom(s => s.Id.MatchId))
                    .ForMember(d => d.GameMode, opt => opt.MapFrom(s => s.Id.GameMode))
                    .ForMember(d => d.Player, opt => opt.MapFrom(s => s.Players[0]));
                x.CreateMap<HaloSharp.Model.Stats.CarnageReport.Common.XpInfo, XpInfo>();

            });
        }
    }
}
