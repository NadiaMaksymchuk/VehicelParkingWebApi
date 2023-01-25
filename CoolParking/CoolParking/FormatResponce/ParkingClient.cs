using CoolParking.FormatResponce.AbstractClass;
using System.Net;

namespace CoolParking.FormatResponce
{
    internal class ParkingClient : BaseClass
    {
        private const string ControllerName = "parking";

        public ParkingClient() : base() { }

        public void GetBalance()
        {
            var response = _httpClient.GetAsync($"{ControllerName}/balance").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"Balance: {Deserializer<decimal>(content)}");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(content);
            }
        }

        public void GetFreePlaces()
        {
            var response = _httpClient.GetAsync($"{ControllerName}/freePlaces").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"Free places: {Deserializer<int>(content)}");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(content);
            }
        }

        public void GetCapacity()
        {
            var response = _httpClient.GetAsync($"{ControllerName}/capacity").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine($"Capacity: {Deserializer<int>(content)}");
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(content);
            }
        }
    }
}
