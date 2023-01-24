using System;

namespace CoolParking.BL.Models
{
    public struct TransactionInfo
    {
        public decimal Sum { get; private set; }
        public string VehicleId { get; private set; }
        public DateTime Created { get; private set; }

        public TransactionInfo(Vehicle vehicle, decimal payment)
        {
            VehicleId = vehicle.Id;
            Created = DateTime.Now;
            Sum = payment;
        }

        public override string ToString()
        {
            return $"Created: {Created}; Vehicle Id:  {VehicleId};  Summa: {Sum}\n";
        }
    }
}