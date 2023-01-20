using System;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using Fare;

namespace CoolParking.BL.Models
{
    public class Vehicle
    {
        public string Id { get; }

        public decimal Balance { get; internal set; }

        public VehicleType VehicleType { get; }

        private const string regexString = "^[A-Z]{2}-[0-9]{4}-[A-Z]{2}$";

        public Vehicle(string id,VehicleType vehicleType, decimal balance)
        {
            Regex regex = new Regex(regexString);
            if (regex.IsMatch(id) && balance > 0)
            {
                Id = id;
                VehicleType = vehicleType;
                Balance = balance;
            }
            else
                throw new ArgumentException();
        }

        public static string GenerateRandomRegistrationPlateNumber()
        {
            Xeger numberCar = new Xeger(regexString);

            return numberCar.Generate();
        }
    }
}
