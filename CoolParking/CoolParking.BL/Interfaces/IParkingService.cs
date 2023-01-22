using System;
using System.Collections.ObjectModel;
using CoolParking.BL.Models;
using CoolParking.BL.Utility;

namespace CoolParking.BL.Interfaces
{
    public interface IParkingService : IDisposable
    {
        decimal GetBalance();
        int GetCapacity();
        int GetFreePlaces();
        ReadOnlyCollection<Vehicle> GetVehicles();
        ResponseHendler<Vehicle> AddVehicle(Vehicle vehicle);
        ResponseHendler<Vehicle> RemoveVehicle(string vehicleId);
        ResponseHendler<Vehicle> TopUpVehicle(string vehicleId, decimal sum);
        TransactionInfo[] GetLastParkingTransactions();
        string ReadFromLog();
        ResponseHendler<Vehicle> GetByIdVehicle(string id);
    }
}
