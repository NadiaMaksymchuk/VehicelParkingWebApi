using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;

namespace CoolParking
{
    public static class Menus
    {
        public static readonly ITimerService _timerService;
        public static readonly ITimerService _logTimeService;
        public static readonly ILogService _logService;
        public static readonly ParkingService _parkingService;
        public static bool Work = true;

        static Menus()
        {
            string currentDirectory = $"{Directory.GetCurrentDirectory()}/Transactions.log";
            _timerService = new TimerService();
            _logTimeService = new TimerService();
            _logService = new LogService(currentDirectory);
            _parkingService = new ParkingService(_timerService, _logTimeService, _logService);
        }

        public static void FirstMenu()
        {
            short curItem = 0;
            string[] menuItems = new string[] {
                "Balance of parking",
                "The amount of money earned for the current period",
                "The number of free parking spaces",
                "All Parking Transactions for the current period",
                "The transaction history",
                "The list of vechicles in the parking",
                "Put the vehicle in the parking",
                "Remove the Vehicle from the parking",
                "Top up the balance of a specific vechicles" };

            switch (MenuLogic.Menu(curItem, menuItems))
            {
                case 0: GetBalanceMenu(); break;
                case 1: SummMenu(); break;
                case 2: FreePlacesMenu(); break;
                case 3: GetLastParkingTransactionsMenu(); break;
                case 4: ReadFromLogMenu(); break;
                case 5: AllVehicleMenu(); break;
                case 6: AddVehicleMenu(); break;
                case 7: RemoveVehicleMenu(); break;
                case 8: TopUpVehicleMenu(); break;
                case 9: Work = false; return;
            }
        }

        public static void GetBalanceMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Balance of parking ", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: Console.WriteLine($"Blance: {_parkingService.GetBalance()}"); Console.ReadKey(); break;
                case 1: FirstMenu(); break;

            }
        }

        public static void SummMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Get Last Parking sum", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: Console.WriteLine($"Summa: {_parkingService.GetLastParkingTransactions().Sum(tr => tr.Sum)}"); Console.ReadKey(); break;
                case 1: FirstMenu(); break;
            }
        }

        public static void FreePlacesMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Get free places in parking", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: Console.WriteLine($"Free places: {_parkingService.GetFreePlacesForClient()} - 10"); Console.ReadKey(); break;
                case 1: FirstMenu(); break;
            }
        }

        public static void GetLastParkingTransactionsMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Get Last Parking Transactions", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: _parkingService.GetLastParkingTransactionsToString(); Console.ReadKey(); break;
                case 1: FirstMenu(); break;
            }
        }

        public static void ReadFromLogMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Read from log", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: Console.WriteLine(_parkingService.ReadFromLog()); Console.ReadKey(); break;
                case 1: FirstMenu(); break;
            }
        }

        public static void AllVehicleMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Get all vehicle", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: _parkingService.GetAllVehicle(); Console.ReadKey(); break;
                case 1: FirstMenu(); break;
            }
        }

        public static void AddVehicleMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Add a vehicle", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter id:");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter type of vehicle: bus, truck, motorcycle, passenger car:");
                    VehicleType vehicleType = VehicleType.PassengerCar;
                    switch (Console.ReadLine().ToLower())
                    {
                        case "bus":
                            vehicleType = VehicleType.Bus;
                            break;
                        case "truck":
                            vehicleType = VehicleType.Truck;
                            break;
                        case "motorcycle":
                            vehicleType = VehicleType.Motorcycle;
                            break;
                    }
                    Console.WriteLine("Enter balance:");
                    decimal balance = decimal.Parse(Console.ReadLine());
                    try
                    {
                        _parkingService.AddVehicle(new Vehicle(id, vehicleType, balance));
                        Console.WriteLine("Secssesfuly added");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    { Console.WriteLine(ex.ToString()); Console.ReadKey(); }
                    break;
                case 1: FirstMenu(); break;
            }
        }

        static public void RemoveVehicleMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Remove a vehicle", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter id:");
                    string id = Console.ReadLine();
                    try
                    {
                        _parkingService.RemoveVehicle(id);
                        Console.WriteLine("Secssesfuly removed");
                        Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString()); Console.ReadKey();
                    }
                    break;
                case 1: FirstMenu(); break;
            }
        }

        static public void TopUpVehicleMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Top up a vehicle", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter id:");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter summa:");
                    decimal sum = decimal.Parse(Console.ReadLine());
                    try
                    {
                        _parkingService.TopUpVehicle(id, sum); Console.WriteLine("Secssesfuly top uped"); Console.ReadKey();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString()); Console.ReadKey();
                    }
                    break;
                case 1: FirstMenu(); break;
            }
        }
    }
}
