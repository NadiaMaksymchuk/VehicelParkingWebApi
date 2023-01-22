using Fare;

namespace CoolParking.BL.Models
{
    public class Vehicle
    {
        public string Id { get; }

        public VehicleType VehicleType { get; }

        public decimal Balance { get; internal set; }

        public const string RegexString = "^[A-Z]{2}-[0-9]{4}-[A-Z]{2}$";

        public Vehicle(string id, VehicleType vehicleType, decimal balance)
        {
            Id = id;
            VehicleType = vehicleType;
            Balance = balance;
        }

        public static string GenerateRandomRegistrationPlateNumber()
        {
            Xeger numberCar = new Xeger(RegexString);

            return numberCar.Generate();
        }

        public override string ToString()
        {
            return $"Id: {Id}; Vehicle type:  {VehicleType};  Balance: {Balance}\n";
        }
    }
}
