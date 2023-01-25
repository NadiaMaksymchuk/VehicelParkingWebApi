using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService, IDisposable
    {
        readonly ITimerService _withdrawTimer;
        readonly ITimerService _logTimer;
        readonly ILogService _logService;
        static List<TransactionInfo> _transactionInfos = new List<TransactionInfo>();
        private static Parking _parking;

        private bool IsDisposed = false;

        public ParkingService(ITimerService withdrawTimer, ITimerService logTimer, ILogService logService)
        {
            _withdrawTimer = withdrawTimer;
            _logTimer = logTimer;
            _logService = logService;
            _withdrawTimer.Interval = Settings.PaymentWriteOffPeriod;
            _logTimer.Interval = Settings.PeriodOfWritingToTheLog;
            _withdrawTimer.Elapsed += new ElapsedEventHandler(GetBerthage);
            _logTimer.Elapsed += new ElapsedEventHandler(WritteAllTransactionInLog);
            _logTimer.Start();
            _withdrawTimer.Start();
        }

        public static Parking Parking
        {
            get
            {
                return _parking ??= new Parking();
            }
        }

        public ResponseHendler<Vehicle> AddVehicle(Vehicle vehicle)
        {
            if (!ValidateVehicleId(vehicle.Id) || vehicle.Balance <= 0)
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "Not added! Incorect vehicle data!",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            if (Parking.Vehicles.Count >= 10)
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "The parking lot is full!",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            if (Parking.Vehicles.Any(x => x.Id == vehicle.Id))
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "A vehicle with this id is already in the parking lot!",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            Parking.Vehicles.Add(vehicle);
            return new ResponseHendler<Vehicle>()
            {
                StatusCode = HttpStatusCode.Created,
                Data = vehicle
            };
        }

        public ResponseHendler<Vehicle> GetByIdVehicle(string id)
        {
            if (!ValidateVehicleId(id))
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "Incorect vehicle id format",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            Vehicle vehicle = Parking.Vehicles.FirstOrDefault(x => x.Id == id);

            if (vehicle is null)
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "Vehicle not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new ResponseHendler<Vehicle>()
            {
                StatusCode = HttpStatusCode.OK,
                Data = vehicle
            };
        }

        private bool ValidateVehicleId(string id)
        {
            Regex regex = new Regex(Vehicle.RegexString);

            if (regex.IsMatch(id))
            {
                return true;
            }

            return false;
        }

        public void WritteAllTransactionInLog(Object source, ElapsedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var transaction in _transactionInfos.ToList())
            {
                stringBuilder.Append(transaction.ToString());
            }
            _transactionInfos.Clear();
            _logService.Write(stringBuilder.ToString());
        }

        public static void GetBerthage(Object source, ElapsedEventArgs e)
        {
            if (Parking.Vehicles.Count != 0)
            {
                foreach (var vehicle in Parking.Vehicles)
                {
                    if (vehicle.Balance >= Settings.Tariffes[vehicle.VehicleType])
                    {
                        var summa = Settings.Tariffes[vehicle.VehicleType];
                        Parking.Balance += summa;
                        vehicle.Balance -= summa;
                        TransactionInfo transactionInfo = new TransactionInfo(vehicle, summa);
                        _transactionInfos.Add(transactionInfo);

                    }
                    else if (vehicle.Balance <= 0)
                    {
                        var summa = Settings.Tariffes[vehicle.VehicleType] * Settings.FineRate;
                        Parking.Balance += summa;
                        vehicle.Balance -= summa;
                        TransactionInfo transactionInfo = new TransactionInfo(vehicle, summa);
                        _transactionInfos.Add(transactionInfo);
                    }
                    else
                    {
                        var lastPositiveSummaOnVechicleBalance = vehicle.Balance;

                        var summa = (Settings.Tariffes[vehicle.VehicleType] - lastPositiveSummaOnVechicleBalance) * Settings.FineRate + lastPositiveSummaOnVechicleBalance;
                        Parking.Balance += summa;
                        vehicle.Balance -= summa;

                        TransactionInfo transactionInfo = new TransactionInfo(vehicle, summa);
                        _transactionInfos.Add(transactionInfo);

                    }
                }
            }
        }

        public decimal GetBalance()
        {
            return Parking.Balance;
        }

        public int GetCapacity()
        {
            return Settings.CapacityOfParking;
        }

        public int GetFreePlaces()
        {
            return (Settings.CapacityOfParking - Parking.Vehicles.Count);
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return _transactionInfos.ToArray();
        }

        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            return Parking.Vehicles.AsReadOnly();
        }

        public string ReadFromLog()
        {
            return _logService.Read();
        }

        public ResponseHendler<Vehicle> RemoveVehicle(string vehicleId)
        {
            if (!ValidateVehicleId(vehicleId))
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "Incorect vehicle id format",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var removeVehicle = Parking.Vehicles.FirstOrDefault(x => x.Id == vehicleId);

            if (removeVehicle is null)
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "Vehicle not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            if (removeVehicle.Balance <= 0)
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "The balance vehicle is negative, pay off the debt",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            Parking.Vehicles.Remove(removeVehicle);
            return new ResponseHendler<Vehicle>()
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        public ResponseHendler<Vehicle> TopUpVehicle(string vehicleId, decimal sum)
        {
            if (sum < 0)
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "Negative amount summa",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            if (!ValidateVehicleId(vehicleId) || sum < 0)
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "Incorect vehicle id format",
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            var topUpVehicle = Parking.Vehicles.FirstOrDefault(x => x.Id == vehicleId);

            if (topUpVehicle is null)
            {
                return new ResponseHendler<Vehicle>()
                {
                    Error = "Vehicle not found",
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            topUpVehicle.Balance += sum;
            return new ResponseHendler<Vehicle>()
            {
                StatusCode = HttpStatusCode.OK,
                Data = topUpVehicle
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                _parking = null;
                _transactionInfos.Clear();
                _withdrawTimer.Elapsed -= new ElapsedEventHandler(GetBerthage);
                _logTimer.Elapsed -= new ElapsedEventHandler(WritteAllTransactionInLog);
                _withdrawTimer.Dispose();
                _logTimer.Dispose();
            }

            IsDisposed = true;
        }
    }
}
