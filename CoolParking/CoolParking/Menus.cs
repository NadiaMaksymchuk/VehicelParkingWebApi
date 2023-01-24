using CoolParking.BL.Models;
using CoolParking.FormatResponce;

namespace CoolParking
{
    internal static class Menus
    {
        static readonly ParkingClient _parkingClient;
        static readonly TransactionsClient _transactionsClient;
        static readonly VehiclesClient _vehiclesClient;
        public static bool Work = true;

        static Menus()
        {
            _parkingClient = new ParkingClient();
            _transactionsClient = new TransactionsClient();
            _vehiclesClient = new VehiclesClient();
        }

        public static void FirstMenu()
        {
            short curItem = 0;
            string[] menuItems = new string[] {
                "Balance of parking",
                "Get vehicle by id",
                "The number of free parking spaces",
                "All Parking Transactions for the current period",
                "The transaction history",
                "The list of vechicles in the parking",
                "Put the vehicle in the parking",
                "Remove the Vehicle from the parking",
                "Top up the balance of a specific vechicles",
                "Exit"
            };

            switch (MenuLogic.Menu(curItem, menuItems))
            {
                case 0: 
                    GetBalanceMenu(); 
                    break;
                case 1: 
                    GetById(); 
                    break;
                case 2: 
                    FreePlacesMenu(); 
                    break;
                case 3: 
                    GetLastParkingTransactionsMenu(); 
                    break;
                case 4: 
                    ReadFromLogMenu(); 
                    break;
                case 5: 
                    AllVehicleMenu(); 
                    break;
                case 6: 
                    AddVehicleMenu(); 
                    break;
                case 7: 
                    RemoveVehicleMenu(); 
                    break;
                case 8:
                    TopUpVehicleMenu();
                    break;
                case 9: 
                    Work = false; 
                    return;
            }
        }

        public static void GetBalanceMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Balance of parking ", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: 
                    _parkingClient.GetBalance(); 
                    Console.ReadKey(); 
                    break;
                case 1: 
                    FirstMenu(); 
                    break;
            }
        }

        public static void GetById()
        {
            short curItem = 0;
            string[] menuSelect = { "Get vehicle by id", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter vehicle id");
                    string id = Console.ReadLine();
                    _vehiclesClient.GetVehicleById(id);
                    Console.ReadKey();
                    break;
                case 1: 
                    FirstMenu(); 
                    break;
            }
        }

        public static void FreePlacesMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Get free places in parking", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: 
                    _parkingClient.GetFreePlaces(); 
                    Console.ReadKey(); 
                    break;
                case 1:
                    FirstMenu(); 
                    break;
            }
        }

        public static void GetLastParkingTransactionsMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Get Last Parking Transactions", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: 
                    _transactionsClient.GetLastParkingTransactions(); 
                    Console.ReadKey(); 
                    break;
                case 1: 
                    FirstMenu(); 
                    break;
            }
        }

        public static void ReadFromLogMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Read from log", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: 
                    _transactionsClient.GetAllParkingTransactions(); 
                    Console.ReadKey(); 
                    break;
                case 1: 
                    FirstMenu(); 
                    break;
            }
        }

        public static void AllVehicleMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Get all vehicle", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0: 
                    _vehiclesClient.GetVehicles(); 
                    Console.ReadKey(); 
                    break;
                case 1: 
                    FirstMenu(); 
                    break;
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

                    _vehiclesClient.AddVehicle(new Vehicle (id, vehicleType, balance));
                    Console.ReadKey();
                    break;
                case 1: 
                    FirstMenu(); 
                    break;
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

                    _vehiclesClient.RemoveVehicle(id);
                    Console.ReadKey();
                    break;
                case 1: 
                    FirstMenu(); 
                    break;
            }
        }

        static public void TopUpVehicleMenu()
        {
            short curItem = 0;
            string[] menuSelect = { "Top up a vehicle", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    TopUpVehicle topUpVehicle = new TopUpVehicle();
                    Console.WriteLine("Enter id:");
                    string id = Console.ReadLine();
                    Console.WriteLine("Enter summa:");
                    decimal sum = decimal.Parse(Console.ReadLine());
                    topUpVehicle.Sum = sum;
                    topUpVehicle.Id = id;
                    _transactionsClient.TopUpVehicle(topUpVehicle);
                    Console.ReadKey();
                    break;
                case 1: 
                    FirstMenu(); 
                    break;
            }
        }
    }
}
