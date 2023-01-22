using CoolParking.FormatResponce.AbstractClass;
using System.Net;

namespace CoolParking.FormatResponce
{
    internal class ParkingClient : BaseClass
    {
        private const string BaseURL = "https://localhost:7177/api/parking";

        public ParkingClient() : base() { }

        public void GetBalance()
        {
            string url = $"{BaseURL}/balance";
            var responce = _httpClient.GetAsync(url).Result;

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                var data = _httpClient.GetStringAsync(url).Result;
                Console.WriteLine($"Balance: {Deserializer<decimal>(data)}");
            }
            else
            {
                Console.WriteLine(responce.ReasonPhrase);
            }
        }

        public void GetFreePlaces()
        {
            string url = $"{BaseURL}/freePlaces";

            var responce = _httpClient.GetAsync(url).Result;

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                var data = _httpClient.GetStringAsync(url).Result;
                Console.WriteLine($"Free plces: {Deserializer<int>(data)}");
            }
            else
            {
                Console.WriteLine(responce.ReasonPhrase);
            }
        }

        public void GetCapacity()
        {
            string url = $"{BaseURL}/capacity";

            var responce = _httpClient.GetAsync(url).Result;

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                var data = _httpClient.GetStringAsync(url).Result;
                Console.WriteLine($"Capacity: {Deserializer<int>(data)}");
            }
            else
            {
                Console.WriteLine(responce.ReasonPhrase);
            }
        }
    }
}
