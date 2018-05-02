using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaloHistory.Business.Entities
{
    public interface ICartographerContext
    {

        IQueryable<T> Get<T>() where T : class;

        Task<List<T>> GetAll<T>() where T : class;

        Task<T> FindAsync<T>(object id) where T : class;

        Task<T3> FindAsync<T, T2, T3>(T2 id) where T : BaseDataEntity<T2, T3> where T3 : class;

        void InsertAsync<T>(params T[] items) where T : class;

        void InsertAsync<T, T2, T3>(T item, bool addToCache = false) where T : BaseDataEntity<T2, T3> where T3 : class;

        Task DeleteAsync<T>() where T : class;

        Task CommitChanges();
    }
}