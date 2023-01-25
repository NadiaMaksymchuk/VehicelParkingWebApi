using CoolParking.BL.Models;
using CoolParking.FormatResponce.AbstractClass;
using System.Net;

namespace CoolParking.FormatResponce
{
    internal class TransactionsClient : BaseClass
    {
        private const string ControllerName = "transactions";

        public TransactionsClient() : base() { }

        public void GetLastParkingTransactions()
        {
            var response = _httpClient.GetAsync($"{ControllerName}/last").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var transactions = Deserializer<List<TransactionInfo>>(content);

                foreach (var transaction in transactions)
                {
                    Console.WriteLine(transaction.ToString());
                }
            }
            else
            {
                Console.WriteLine(content);
            }
        }

        public void GetAllParkingTransactions()
        {
            var response = _httpClient.GetAsync($"{ControllerName}/all").Result;

            var content = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(content);
        }

        public void TopUpVehicle(TopUpVehicle topUpVehicle)
        {
            var data = Serializer<TopUpVehicle>(topUpVehicle);
            var response = _httpClient.PutAsync($"{ControllerName}/topUpVehicle", data).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            if(response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(content);
            }
            
            if(response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Successfully changed");
            }
        }
    }
}
