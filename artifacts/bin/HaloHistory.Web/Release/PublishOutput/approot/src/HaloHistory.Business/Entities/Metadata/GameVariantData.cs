using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class GameVariantData : BaseDataEntity<string, GameVariant>
    {
        public GameVariantData()
        {
        }

        public GameVariantData(Guid id, GameVariant data) : base(id.ToString(), data)
        {
        }
    }
}
