using CoolParking.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/parking")]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet("balance")]
        public ActionResult<decimal> GetBalance()
        {
            return _parkingService.GetBalance();
        }

        [HttpGet("capacity")]
        public ActionResult<int> GetCapacity()
        {
            return _parkingService.GetCapacity();
        }

        [HttpGet("freePlaces")]
        public ActionResult<int> GetFreePlaces()
        {
            return _parkingService.GetFreePlaces();
        }
    }
}
