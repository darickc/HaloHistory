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
        public DbSet<ArenaMatchData> ArenaMatchDatas { get; set; }
        public DbSet<CampaignMatchData> CampaignMatchDatas { get; set; }
        public DbSet<CustomMatchData> CustomMatchDatas { get; set; }
        public DbSet<MatchEventsData> MatchEventsDatas { get; set; }
        public DbSet<WarzoneMatchData> WarzoneMatchDatas { get; set; }

        public HaloHistoryContext()
        {

        }

        public HaloHistoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public async Task<T> FindAsync<T>(object id) where T : class
        {
            return await Set<T>().Find(id);
        }

        public IQueryable<T> Get<T>() where T : class
        {
            return Set<T>().AsQueryable();
        }

        public void InsertAsync<T>(params T[] items) where T : class
        {
            Set<T>().AddRange(items);
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
