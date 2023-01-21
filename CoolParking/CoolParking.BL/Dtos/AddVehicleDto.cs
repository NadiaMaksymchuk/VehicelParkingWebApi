using CoolParking.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.BL.Dtos
{
    public class AddVehicleDto
    {
        public string Id { get; set; }

        public VehicleType VehicleType { get; set; }

        public decimal Balance { get; set; }
    }
}
