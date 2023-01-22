using CoolParking.BL.Models;
using System;
using System.Text;
using System.Text.Json;

namespace CoolParking.FormatResponce.AbstractClass
{
    internal abstract class BaseClass
    {
        protected HttpClient _httpClient;

        public BaseClass()
        {
            _httpClient = new HttpClient();
        }

        protected static T Deserializer<T>(string data)
        {
            return JsonSerializer.Deserialize<T>(data);
        }

        protected static StringContent Serializer<T>(T entity)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(entity);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected static string VehicleFormat(Vehicle vehicle)
        {
            return $"Id: {vehicle.Id} | Balance: {vehicle.Balance} | Vehucle type: {vehicle.VehicleType}";
        }
    }
}
