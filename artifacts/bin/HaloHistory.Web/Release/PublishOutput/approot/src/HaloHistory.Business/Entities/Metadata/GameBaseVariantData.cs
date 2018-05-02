using System;
using HaloSharp.Model.Metadata;

namespace HaloHistory.Business.Entities.Metadata
{
    public class GameBaseVariantData : BaseDataEntity<string, GameBaseVariant>
    {
        public GameBaseVariantData()
        {
        }

        public GameBaseVariantData(Guid id, GameBaseVariant data) : base(id.ToString(), data)
        {
        }
    }
}
