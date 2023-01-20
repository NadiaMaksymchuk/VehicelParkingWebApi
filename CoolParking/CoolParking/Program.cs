using System;

namespace CoolParking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Menus.FirstMenu();
            } while (Menus.Work);
            Console.ReadKey();
        }
    }
}