using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class EnemyData : BaseDataEntity<long, Enemy>
    {
        public EnemyData()
        {
        }

        public EnemyData(long id, Enemy data) : base(id, data)
        {
        }
    }
}
