using System.Collections.Generic;

namespace CoolParking.BL.Models
{
    public static class Settings
    {
        public const int ParkingBalace = 0;
        public const decimal FineRate = 2.5m;
        public const int CapacityOfParking = 10;
        public const int PaymentWriteOffPeriod = 5000;
        public const int PeriodOfWritingToTheLog = 60000;

        public static Dictionary<VehicleType, decimal> Tariffes = new Dictionary<VehicleType, decimal>()
        {   { VehicleType.PassengerCar, 2 },
            { VehicleType.Truck, 5 },
            { VehicleType.Bus, 3.5m },
            { VehicleType.Motorcycle, 1 }
        };
    }
}