using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            return JsonConvert.SerializeObject(data, GetSettings());
        }

        public T2 Deserialize()
        {
            return JsonConvert.DeserializeObject<T2>(Data, GetSettings());
        }

        private JsonSerializerSettings GetSettings()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            return settings;
        }
    }
}
