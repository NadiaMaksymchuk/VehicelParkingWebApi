using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Net;
using CoolParking.WebAPI.Extentions;

namespace CoolParking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public VehiclesController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        [HttpGet]
        public ActionResult<ReadOnlyCollection<Vehicle>> GetVehicles()
        {
            return _parkingService.GetVehicles();
        }

        [HttpGet("{id}")]
        public IActionResult GetVehicleById(string id)
        {
            return this.HandlerResponse(_parkingService.GetByIdVehicle(id));
        }

        [HttpPost]
        public IActionResult PostVehicle(Vehicle vehicle)
        {
            return this.HandlerResponse(_parkingService.AddVehicle(vehicle));
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveVehicle(string id)
        {
            return this.HandlerResponse(_parkingService.RemoveVehicle(id));
        }
    } 
}
