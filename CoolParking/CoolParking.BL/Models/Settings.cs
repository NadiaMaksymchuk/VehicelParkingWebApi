using System.Collections.Generic;

namespace CoolParking.BL.Models
{
    public static class Settings
    {
        public static Dictionary<string, decimal> SettingsData = new Dictionary<string, decimal>()
        {
            { "startBalanceParcing", 0},
            { "sizeOfParking", 10},
            { "paymentWriteOffPeriod", 5000},
            { "thePeriodOfWritingToTheLog", 60000 },
            { "fineRate",  2.5m },

        };

        public static Dictionary<VehicleType, decimal> Tariffes = new Dictionary<VehicleType, decimal>()
        {   { VehicleType.PassengerCar, 2 },
            { VehicleType.Truck, 5 },
            { VehicleType.Bus, 3.5m },
            { VehicleType.Motorcycle, 1 }
        };
    }
}