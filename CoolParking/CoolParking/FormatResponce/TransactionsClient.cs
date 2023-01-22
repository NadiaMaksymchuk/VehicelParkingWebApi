using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CoolParking.FormatResponce.AbstractClass;
using CoolParking.BL.Models;

namespace CoolParking.FormatResponce
{
    internal class TransactionsClient : BaseClass
    {
        private const string BaseURL = "https://localhost:7177/api/transactions";

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
                    Console.WriteLine($"Created: {transaction.Created} | Vehicle id: {transaction.VehicleId} | Summa: {transaction.Sum}");
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
                var transactions = Deserializer<string>(data);

                Console.WriteLine(transactions);
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
