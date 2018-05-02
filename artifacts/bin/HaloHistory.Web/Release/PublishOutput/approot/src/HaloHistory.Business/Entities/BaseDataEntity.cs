using Newtonsoft.Json;

namespace HaloHistory.Business.Entities
{
    public class BaseDataEntity<T,T2>
    {
        public int Id { get; set; }
        public T ItemId { get; set; }
        public string Data { get; set; }

        public BaseDataEntity()
        {
            
        }

        public BaseDataEntity(T id, T2 data)
        {
            ItemId = id;
            Data = Serialize(data);
        }

        public string Serialize(T2 data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T2 Deserialize()
        {
            return JsonConvert.DeserializeObject<T2>(Data);
        }
    }
}
