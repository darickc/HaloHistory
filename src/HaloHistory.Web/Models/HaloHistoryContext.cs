using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaloHistory.Business.Entities;
using HaloHistory.Business.Entities.Metadata;
using HaloHistory.Business.Entities.Profile;
using HaloHistory.Business.Entities.Stats;
using HaloHistory.Web.Services;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace HaloHistory.Web.Models
{
    public class HaloHistoryContext : DbContext, ICartographerContext
    {
        private readonly IDataCache _dataCache;

        public DbSet<CampaignMissionData> CampaignMissionDatas { get; set; }
        public DbSet<CommendationData> CommendationDatas { get; set; }
        public DbSet<CompetitiveSkillRankDesignationData> CompetitiveSkillRankDesignationDatas { get; set; }
        public DbSet<EnemyData> EnemyDatas { get; set; }
        public DbSet<FlexibleStatData> FlexibleStatDatas { get; set; }
        public DbSet<GameBaseVariantData> GameBaseVariantDatas { get; set; }
        public DbSet<GameVariantData> GameVariantDatas { get; set; }
        public DbSet<ImpulseData> ImpulseDatas { get; set; }
        public DbSet<MapData> MapDatas { get; set; }
        public DbSet<MapVariantData> MapVariantDatas { get; set; }
        public DbSet<MedalData> MedalDatas { get; set; }
        public DbSet<PlaylistData> PlaylistDatas { get; set; }
        public DbSet<SeasonData> SeasonDatas { get; set; }
        public DbSet<SkullData> SkullDatas { get; set; }
        public DbSet<SpartanRankData> SpartanRankDatas { get; set; }
        public DbSet<TeamColorData> TeamColorDatas { get; set; }
        public DbSet<VehicleData> VehicleDatas { get; set; }
        public DbSet<WeaponData> WeaponDatas { get; set; }
        public DbSet<EmblemImageData> EmblemImageDatas { get; set; }
        public DbSet<SpartanImageData> SpartanImageDatas { get; set; }
        //public DbSet<ArenaMatchData> ArenaMatchDatas { get; set; }
        //public DbSet<CampaignMatchData> CampaignMatchDatas { get; set; }
        //public DbSet<CustomMatchData> CustomMatchDatas { get; set; }
        //public DbSet<MatchEventsData> MatchEventsDatas { get; set; }
        //public DbSet<WarzoneMatchData> WarzoneMatchDatas { get; set; }

        public DbSet<MatchResultData> MatchResultDatas { get; set; }

        public HaloHistoryContext(IDataCache dataCache)
        {
            _dataCache = dataCache;
        }

        public HaloHistoryContext(DbContextOptions options, IDataCache dataCache) : base(options)
        {
            _dataCache = dataCache;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MatchResultData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<SpartanImageData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<EmblemImageData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<CampaignMissionData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<CommendationData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<FlexibleStatData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<GameBaseVariantData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<GameVariantData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<MapData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<MapVariantData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<PlaylistData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<SeasonData>()
              .Property(e => e.ItemId).HasMaxLength(36);

            builder.Entity<MatchResultData>()
                .HasIndex(b => b.ItemId);

            builder.Entity<SpartanImageData>()
                .HasIndex(b => b.ItemId);

            builder.Entity<EmblemImageData>()
                .HasIndex(b => b.ItemId);
        }

        public async Task<T> FindAsync<T>(object id) where T : class
        {
            return await Set<T>().Find(id);
        }

        public async Task<T3> FindAsync<T, T2, T3>(T2 id) where T : BaseDataEntity<T2, T3> where T3 : class
        {
            //if (_dataCache.Contains<T3, T2>(id))
            //{
            //     return _dataCache.Get<T3, T2>(id);
            //}

            if (!_dataCache.Contains<T, T2, T3>())
            {
                var items = await Set<T>().ToListAsync();
                _dataCache.Add<T,T2,T3>(items);
            }
            var dataItem = _dataCache.Get<T, T2, T3>(id);
            //if (dataItem != null)
            //{
            //    var item = dataItem.Deserialize();
            //    _dataCache.Add(id,item);
            //    return item;
            //}
            return dataItem?.Deserialize();
        }

        public void Add<T, T2, T3>(T2 id, T item) where T : BaseDataEntity<T2, T3> where T3 : class
        {
            _dataCache.Add<T, T2, T3>(id,item);
        }



        public IQueryable<T> Get<T>() where T : class
        {
            return Set<T>().AsQueryable();
        }

        public async Task<List<T>> GetAll<T>() where T : class
        {
            return await Set<T>().ToListAsync();
        }

        public void InsertAsync<T>(params T[] items) where T : class
        {
            Set<T>().AddRange(items);
            //await SaveChangesAsync();
        }

        public void InsertAsync<T, T2, T3>(T item, bool addToCache = false) where T : BaseDataEntity<T2, T3> where T3 : class
        {
            Set<T>().Add(item);
            if (addToCache)
            {
                _dataCache.Add<T, T2, T3>(item.ItemId, item);
            }
            //await SaveChangesAsync();
        }

        public async Task DeleteAsync<T>() where T : class
        {
            var set = Set<T>();
            var items = await set.ToListAsync();
            foreach (var item in items)
            {
                set.Remove(item);
            }
            //await SaveChangesAsync();
        }

        public async Task CommitChanges()
        {
            await SaveChangesAsync();
        }
    }
}
