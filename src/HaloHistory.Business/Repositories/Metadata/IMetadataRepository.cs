using System;
using System.Threading.Tasks;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Repositories.Metadata
{
    public interface IMetadataRepository
    {
        Task<Commendation> GetCommendation(Guid id);
        Task<CompetitiveSkillRankDesignation> GetCompetitiveSkillRankDesignation(int id);
        Task<Enemy> GetEnemy(long id);
        Task<FlexibleStat> GetFlexibleStat(Guid id);
        Task<Impulse> GetImpulse(long id);
        Task<GameBaseVariant> GetGameBaseVariant(Guid id);
        Task<GameVariant> GetGameVariant(Guid id);
        Task<Map> GetMap(Guid id);
        Task<MapVariant> GetMapVariant(Guid id);
        Task<Medal> GetMedal(long id);
        Task<Playlist> GetPlaylist(Guid id);
        Task<Season> GetSeason(Guid? id);
        Task<Skull> GetSkull(int id);
        Task<SpartanRank> GetSpartanRank(int id);
        Task<TeamColor> GetTeamColor(int id);
        Task<Vehicle> GetVehicle(long id);
        Task<Weapon> GetWeapon(long id);
        Task<Enemy> GetEnemyOrVehicle(long id);
        Task<Weapon> GetWeaponOrEnemyOrVehicle(long id);
        Task Initialize();
    }
}