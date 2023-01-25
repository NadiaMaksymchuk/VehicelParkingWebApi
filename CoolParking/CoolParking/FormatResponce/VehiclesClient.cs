using CoolParking.BL.Models;
using CoolParking.FormatResponce.AbstractClass;
using System.Net;

namespace CoolParking.FormatResponce
{
    internal class VehiclesClient : BaseClass
    {
        private const string ControllerName = "vehicles";

        public VehiclesClient() : base() { }

        public void GetVehicles()
        {
            var response = _httpClient.GetAsync(ControllerName).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var vehicles = Deserializer<List<Vehicle>>(content);

                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine(vehicle.ToString());
                }
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(content);
            }
        }

        public void GetVehicleById(string id)
        {
            var response = _httpClient.GetAsync($"{ControllerName}/{id}").Result;

            var content = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var vehicle = Deserializer<Vehicle>(content);

                Console.WriteLine(vehicle.ToString());
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(content);
            }
        }

        public void AddVehicle(Vehicle vehicle)
        {
            var data = Serializer<Vehicle>(vehicle);
            var response = _httpClient.PostAsync(ControllerName, data).Result;

            if (response.StatusCode == HttpStatusCode.Created)
            {
                Console.WriteLine("Added");
            }
            
            if(response.StatusCode != HttpStatusCode.Created)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
            }
        }

        public void RemoveVehicle(string id)
        {
            var response = _httpClient.DeleteAsync($"{ControllerName}/{id}").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode == HttpStatusCode.NoContent)
            {
                Console.WriteLine("Successfully removed");
            }

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                Console.WriteLine(content);
            }
        }
    }
}
