using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Entities.Metadata;
using HaloSharp;
using HaloSharp.Extension;
using HaloSharp.Model.Metadata;
using HaloSharp.Query.Metadata;
using Newtonsoft.Json;

namespace HaloHistory.Business.Repositories.Metadata
{
    public class MetadataRepository : IMetadataRepository
    {
        private readonly ICartographerContext _db;
        private readonly IHaloSession _session;

        public MetadataRepository(ICartographerContext context, IHaloSession session)
        {
            _session = session;
            _db = context;
        }

        public async Task<Commendation> GetCommendation(Guid id)
        {
            var item = await _db.FindAsync<CommendationData>(id.ToString());
            return item?.Deserialize();
        }

        public async Task<CompetitiveSkillRankDesignation> GetCompetitiveSkillRankDesignation(int id)
        {
            var item = await _db.FindAsync<CompetitiveSkillRankDesignationData>(id);
            return item?.Deserialize();
        }

        public async Task<Enemy> GetEnemy(long id)
        {
            var item = await _db.FindAsync<EnemyData>(id);
            return item?.Deserialize();
        }

        public async Task<FlexibleStat> GetFlexibleStat(Guid id)
        {
            var item = await _db.FindAsync<FlexibleStatData>(id.ToString());
            return item?.Deserialize();
        }

        public async Task<GameBaseVariant> GetGameBaseVariant(Guid id)
        {
            if (id == Guid.Empty)
                return null;
            var item = await _db.FindAsync<GameBaseVariantData>(id.ToString());
            return item?.Deserialize();
        }

        public async Task<GameVariant> GetGameVariant(Guid id)
        {
            if (id == Guid.Empty)
                return null;
            var data = await _db.FindAsync<GameVariantData>(id.ToString());
            if (data == null)
            {
                var query = new GetGameVariant().ForGameVariantId(id);
                var item = await _session.Query(query);
                if (item == null)
                    return null;
                data = new GameVariantData(id, item);
                 _db.InsertAsync(data);
                return item;
            }
            return data.Deserialize();
        }

        public async Task<Impulse> GetImpulse(long id)
        {
            var item = await _db.FindAsync<ImpulseData>(id);
            return item?.Deserialize();
        }

        public async Task<Map> GetMap(Guid id)
        {
            if (id == Guid.Empty)
                return null;
            var map = await _db.FindAsync<MapData>(id.ToString());
            return map?.Deserialize();
        }

        public async Task<MapVariant> GetMapVariant(Guid id)
        {
            if (id == Guid.Empty)
                return null;
            var data = await _db.FindAsync<MapVariantData>(id.ToString());
            if (data == null)
            {
                var query = new GetMapVariant().ForMapVariantId(id);
                var item = await _session.Query(query);
                if (item == null)
                    return null;
                data = new MapVariantData(id, item);
                _db.InsertAsync(data);
                return item;
            }
            return data.Deserialize();
        }

        public async Task<Medal> GetMedal(long id)
        {
            var item = await _db.FindAsync<MedalData>(id);
            return item?.Deserialize();
        }

        public async Task<Playlist> GetPlaylist(Guid id)
        {
            if (id == Guid.Empty)
                return null;
            var item = await _db.FindAsync<PlaylistData>(id.ToString());
            if (item == null)
            {
                await Populate(new GetPlaylists(), d => new PlaylistData(d.Id, d), true);
                item = await _db.FindAsync<PlaylistData>(id.ToString());
            }
            return item?.Deserialize();
        }

        public async Task<Season> GetSeason(Guid? id)
        {
            if (!id.HasValue || id== Guid.Empty)
                return null;
            var item = await _db.FindAsync<SeasonData>(id.ToString());
            if (item == null)
            {
                await Populate(new GetSeasons(), d => new SeasonData(d.Id, d), true);
                item = await _db.FindAsync<SeasonData>(id.ToString());
            }
            return item.Deserialize();
        }

        public async Task<Skull> GetSkull(int id)
        {
            var item = await _db.FindAsync<SkullData>(id);
            return item?.Deserialize();
        }

        public async Task<SpartanRank> GetSpartanRank(int id)
        {
            var item = await _db.FindAsync<SpartanRankData>(id);
            return item?.Deserialize();
        }

        public async Task<TeamColor> GetTeamColor(int id)
        {
            var item = await _db.FindAsync<TeamColorData>(id);
            return item?.Deserialize();
        }

        public async Task<Vehicle> GetVehicle(long id)
        {
            var item = await _db.FindAsync<VehicleData>(id);
            return item?.Deserialize();
        }

        public async Task<Weapon> GetWeapon(long id)
        {
            var item = await _db.FindAsync<WeaponData>(id);
            return item?.Deserialize();
        }

        public async Task<Enemy> GetEnemyOrVehicl(long id)
        {
            var item = await _db.FindAsync<EnemyData>(id);
            if (item != null)
            {
                var enemy = item.Deserialize();
                return enemy;
            }
            var vehicleData = await _db.FindAsync<VehicleData>(id);
            if (vehicleData != null)
            {
                return JsonConvert.DeserializeObject<Enemy>(vehicleData.Data);
            }

            return null;
        }

        public async Task Initialize()
        {
            await Populate(new GetCampaignMissions(), d => new CampaignMissionData(d.Id, d));
            await Populate(new GetCommendations(), d => new CommendationData(d.Id, d));
            await Populate(new GetCompetitiveSkillRankDesignations(), d => new CompetitiveSkillRankDesignationData(d.Id, d));
            await Populate(new GetEnemies(), d => new EnemyData(d.Id, d));
            await Populate(new GetFlexibleStats(), d => new FlexibleStatData(d.Id, d));
            await Populate(new GetGameBaseVariants(), d => new GameBaseVariantData(d.Id, d));
            await Populate(new GetImpulses(), d => new ImpulseData(d.Id, d));
            await Populate(new GetMaps(), d => new MapData(d.Id, d));
            await Populate(new GetMedals(), d => new MedalData(d.Id, d));
            await Populate(new GetPlaylists(), d => new PlaylistData(d.Id, d));
            await Populate(new GetSeasons(), d => new SeasonData(d.Id, d));
            await Populate(new GetSkulls(), d => new SkullData(d.Id, d));
            await Populate(new GetSpartanRanks(), d => new SpartanRankData(d.Id, d));
            await Populate(new GetTeamColors(), d => new TeamColorData(d.Id, d));
            await Populate(new GetVehicles(), d => new VehicleData(d.Id, d));
            await Populate(new GetWeapons(), d => new WeaponData(d.Id, d));
        }

        private async Task Populate<T, T2>(IQuery<List<T2>> query, Func<T2, T> func, bool force = false) where T : class
        {
            bool exists = _db.Get<T>().Any();
            if (!exists || force)
            {
                await _db.DeleteAsync<T>();
                IEnumerable<T2> items = await _session.Query(query);

                var data = items.Select(func).ToArray();
                _db.InsertAsync(data);
            }
        }
    }
}
