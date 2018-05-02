using System.Collections.Generic;
using HaloHistory.Business.Entities;

namespace HaloHistory.Web.Services
{
    public interface IDataCache
    {
        void Add<T, T2>(T2 key, T item) where T : class;
        void Add<T, T2, T3>(List<T> items) where T : BaseDataEntity<T2, T3>;
        void Add<T, T2, T3>(T2 id, T item) where T : BaseDataEntity<T2, T3>;
        bool Contains<T, T2>(T2 id) where T : class;
        bool Contains<T, T2, T3>() where T : BaseDataEntity<T2, T3>;
        T Get<T, T2>(T2 id) where T : class;
        T Get<T, T2, T3>(T2 id) where T : BaseDataEntity<T2, T3>;
    }
}