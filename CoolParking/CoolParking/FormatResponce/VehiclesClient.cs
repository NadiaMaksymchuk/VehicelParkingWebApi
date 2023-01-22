using CoolParking.BL.Models;
using CoolParking.FormatResponce.AbstractClass;
using System.Net;

namespace CoolParking.FormatResponce
{
    internal class VehiclesClient : BaseClass
    {
        private const string BaseURL = "https://localhost:7177/api/vehicles";

        public VehiclesClient() : base() { }

        public void GetVehicles()
        {
            string url = $"{BaseURL}";
            var responce = _httpClient.GetAsync(url).Result;

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                var data = _httpClient.GetStringAsync(url).Result;
                var vehicles = Deserializer<List<Vehicle>>(data);

                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine(vehicle.ToString());
                }
            }
            else
            {
                Console.WriteLine(responce.ReasonPhrase);
            }
        }

        public void GetVehicleById(string id)
        {
            string url = $"{BaseURL}/{id}";
            var responce = _httpClient.GetAsync(url).Result;

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                var data = _httpClient.GetStringAsync(url).Result;
                var vehicle = Deserializer<Vehicle>(data);

                Console.WriteLine(vehicle.ToString());
            }
            else
            {
                Console.WriteLine(responce.ReasonPhrase);
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            string url = $"{BaseURL}";
            var data = Serializer<Vehicle>(vehicle);
            var response = _httpClient.PostAsync(url, data).Result;
            Console.WriteLine(response.ReasonPhrase);
        }

        public void RemoveVehicle(string id)
        {
            string url = $"{BaseURL}/{id}";
            var response = _httpClient.DeleteAsync(url).Result;
            Console.WriteLine(response.ReasonPhrase);
        }
    }
}
