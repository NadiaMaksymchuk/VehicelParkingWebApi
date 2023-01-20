using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using Fare;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        readonly ITimerService _withdrawTimer;
        readonly ITimerService _logTimer;
        readonly ILogService _logService;
        static List<TransactionInfo> _transactionInfos = new List<TransactionInfo>();
        public static Parking _parking { get; private set; }

        public bool IsDisposed { get; private set; }

        public ParkingService(ITimerService withdrawTimer, ITimerService logTimer, ILogService logService)
        {
            _withdrawTimer = withdrawTimer;
            _logTimer = logTimer;
            _logService = logService;
            _parking = new Parking();
            _withdrawTimer.Interval = (double)Settings.SettingsData["paymentWriteOffPeriod"];
            _logTimer.Interval = (double)Settings.SettingsData["thePeriodOfWritingToTheLog"];
            _withdrawTimer.Elapsed += new ElapsedEventHandler(GetBerthage);
            _logTimer.Elapsed += new ElapsedEventHandler(WritteAllTransactionInLog);
            _logTimer.Start();
            _withdrawTimer.Start();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            var count = _parking.Vehicles.Where(x => x != null).ToList().Count;
            if (count < 10)
            {
                if (_parking.Vehicles.Any(x => x.Id == vehicle.Id))
                {
                    throw new ArgumentException();
                }
                else
                {
                    _parking.Vehicles.Add(vehicle);
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public void WritteAllTransactionInLog(Object source, ElapsedEventArgs e)
        {
            if (_transactionInfos.Count != 0)
            {
                string writte = "";
                foreach (var transaction in _transactionInfos.ToList())
                {
                    writte += transaction.ToString();
                }
                _transactionInfos.Clear();
                _logService.Write(writte);
            }
        }

        public static void GetBerthage(Object source, ElapsedEventArgs e)
        {
            var existenVehicle = _parking.Vehicles.Where(x => x != null).ToList();
            if(existenVehicle.Count != 0)
            {
                foreach (var vehicle in existenVehicle)
                {
                    if (vehicle.Balance >= Settings.Tariffes[vehicle.VehicleType])
                    {
                        var summa = Settings.Tariffes[vehicle.VehicleType];
                        _parking.Balance += summa;
                        vehicle.Balance -= summa;
                        TransactionInfo transactionInfo = new TransactionInfo(vehicle, summa);
                        _transactionInfos.Add(transactionInfo);

                    }
                    else if (vehicle.Balance <= 0)
                    {
                        var summa = Settings.Tariffes[vehicle.VehicleType] * Settings.SettingsData["fineRate"];
                        _parking.Balance += summa;
                        vehicle.Balance -= summa;
                        TransactionInfo transactionInfo = new TransactionInfo(vehicle, summa);
                        _transactionInfos.Add(transactionInfo);
                    }
                    else
                    {
                        var lastPositiveSummaOnVechicleBalance = vehicle.Balance;

                        var summa = (Settings.Tariffes[vehicle.VehicleType] - lastPositiveSummaOnVechicleBalance) * Settings.SettingsData["fineRate"] + lastPositiveSummaOnVechicleBalance;
                        _parking.Balance += summa;
                        vehicle.Balance -= summa;

                        TransactionInfo transactionInfo = new TransactionInfo(vehicle, summa);
                        _transactionInfos.Add(transactionInfo);

                    }
                }
            }
        }

        public decimal GetBalance()
        {
            return _parking.Balance;
        }

        public int GetCapacity()
        {
            return (int)Settings.SettingsData["sizeOfParking"];
        }

        public int GetFreePlaces()
        {
            var count = _parking.Vehicles.Where(x => x != null).ToList();
            return (int)(Settings.SettingsData["sizeOfParking"] - count.Count);
        }

        public string GetFreePlacesForClient()
        {
            var count = _parking.Vehicles.Where(x => x != null).ToList();
            return $"{count.Count+1}";
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return _transactionInfos.ToArray();
        }

        public void GetLastParkingTransactionsToString()
        {
            foreach (var transactionInfo in _transactionInfos)
            {
                Console.WriteLine($"Created: {transactionInfo.Created}; Vehicle Id:  {transactionInfo.VehicleId};  Summa: {transactionInfo.Sum}\n");
            }
        }

        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            ReadOnlyCollection<Vehicle> readOnlyVehicles =
            new ReadOnlyCollection<Vehicle>(_parking.Vehicles.Where(x => x != null).ToList());
            return readOnlyVehicles;
        }

        public string ReadFromLog()
        {
            return _logService.Read();
        }

        public void RemoveVehicle(string vehicleId)
        {
            var removeVehicle = _parking.Vehicles.FirstOrDefault(x => x.Id == vehicleId);

            if (removeVehicle is null)
            {
                throw new ArgumentException();
            }
            else if (removeVehicle.Balance <= 0)
            {
                throw new InvalidOperationException();
            }
            else
            {
                _parking.Vehicles.Remove(removeVehicle);
            }
        }

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            var topUpVehicle = _parking.Vehicles.FirstOrDefault(x => x.Id == vehicleId);
            if (sum < 0 || topUpVehicle == null)
            {
                throw new ArgumentException();
            }
            else
                topUpVehicle.Balance += sum;
        }

        public void GetAllVehicle()
        {
            foreach (var vehicle in _parking.Vehicles)
            {
                Console.WriteLine($"Id: {vehicle.Id}; Type: {vehicle.VehicleType}; Balance: {vehicle.Balance}");
            }
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (!this.IsDisposed)
                {
                    if (disposing)
                    {
                        _withdrawTimer.Dispose();
                        _logTimer.Dispose();
                        _parking = null;
                        _withdrawTimer.Stop();
                        _logTimer.Stop();
                    }
                }
            }
            finally
            {
                this.IsDisposed = true;
            }

        }
    }
}
