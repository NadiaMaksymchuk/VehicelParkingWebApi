using Newtonsoft.Json;
using System.Text;

namespace CoolParking.FormatResponce.AbstractClass
{
    internal abstract class BaseClass
    {
        protected HttpClient _httpClient;

        public BaseClass()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:5001/api/");
        }

        protected static T Deserializer<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects
            });
        }

        protected static StringContent Serializer<T>(T entity)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(entity);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
