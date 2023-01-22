using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking
{
    public static class MenuLogic
    {
        public static short Menu(short curItem, string[] MenuText, string menuName = "[ MENU ]")
        {
            ConsoleKeyInfo key;
            short c;
            do
            {
                Console.Clear();
                Console.WriteLine(menuName);

                for (c = 0; c < MenuText.Length; c++)
                {

                    if (curItem == c)
                    {
                        Console.Write(">>");
                        Console.WriteLine(MenuText[c]);
                    }
                    else
                    {
                        Console.WriteLine(MenuText[c]);
                    }
                }
                Console.Write("\nSelect and press {Enter}\n");

                key = Console.ReadKey(true);

                if (key.Key.ToString() == "DownArrow")
                {
                    curItem++;
                    if (curItem > MenuText.Length - 1) curItem = 0;
                }
                else
                {
                    if (key.Key.ToString() == "UpArrow")
                    {
                        curItem--;
                        if (curItem < 0) curItem = Convert.ToInt16(MenuText.Length - 1);
                    }
                }
            } while (key.Key != ConsoleKey.Enter);

            return curItem;

        }
    }
}
