using System.Linq;
using System.Threading.Tasks;

namespace HaloHistory.Business.Entities
{
    public interface ICartographerContext
    {

        IQueryable<T> Get<T>() where T : class;

        Task<T> FindAsync<T>(object id) where T : class;

        void InsertAsync<T>(params T[] items) where T : class;

        Task DeleteAsync<T>() where T : class;

        Task CommitChanges();
    }
}