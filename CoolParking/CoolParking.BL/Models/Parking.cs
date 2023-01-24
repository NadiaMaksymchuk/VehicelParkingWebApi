using System.Collections.Generic;

namespace CoolParking.BL.Models
{
    public class Parking
    {
        public decimal Balance { get; set; }

        public List<Vehicle> Vehicles = new List<Vehicle>(Settings.CapacityOfParking);
    }
}