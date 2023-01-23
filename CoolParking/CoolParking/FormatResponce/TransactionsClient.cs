using CoolParking.BL.Models;
using CoolParking.FormatResponce.AbstractClass;
using System.Net;

namespace CoolParking.FormatResponce
{
    internal class TransactionsClient : BaseClass
    {
        private const string BaseURL = "https://localhost:5001/api/transactions";

        public TransactionsClient() : base() { }

        public void GetLastParkingTransactions()
        {
            string url = $"{BaseURL}/last";
            var responce = _httpClient.GetAsync(url).Result;

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                var data = _httpClient.GetStringAsync(url).Result;
                var transactions = Deserializer<List<TransactionInfo>>(data);

                foreach (var transaction in transactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
            }
            else
            {
                Console.WriteLine(responce.ReasonPhrase);
            }
        }

        public void GetAllParkingTransactions()
        {
            string url = $"{BaseURL}/all";
            var responce = _httpClient.GetAsync(url).Result;

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                var data = _httpClient.GetStringAsync(url).Result;

                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine(responce.ReasonPhrase);
            }
        }

        public void TopUpVehicle(TopUpVehicle topUpVehicle)
        {
            string url = $"{BaseURL}/topUpVehicle";
            var data = Serializer<TopUpVehicle>(topUpVehicle);
            var response = _httpClient.PutAsync(url, data).Result;
            Console.WriteLine(response.ReasonPhrase);
        }
    }
}
