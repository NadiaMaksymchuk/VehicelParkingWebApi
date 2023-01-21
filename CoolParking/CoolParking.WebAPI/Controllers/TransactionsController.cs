using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.WebAPI.Extentions;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public TransactionsController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet("last")]
        public ActionResult<TransactionInfo[]> GetLastTransactions()
        {
            return _parkingService.GetLastParkingTransactions();
        }

        [HttpGet("all")]
        public ActionResult<string> GetAllTransactions()
        {
            return _parkingService.ReadFromLog();
        }

        [HttpPut("topUpVehicle")]
        public IActionResult TopUpVehicle(TopUpVehicle topUpVehicle)
        {
            return this.HandlerResponse(_parkingService.TopUpVehicle(topUpVehicle.Id, topUpVehicle.Sum));
        }
    }
}
