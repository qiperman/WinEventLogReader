using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace WinEventLogReader
{
    class ConsoleMenu
    {
        protected string[] menuItems;
        protected int counter = 0;
        protected Type type = typeof(ConsoleColor);

        public ConsoleMenu(IEnumerable<string> menuItems)
        {
            this.menuItems = menuItems.ToArray();
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ConfigurationSettings.AppSettings.Get("ConsoleColor"));
        }

        public int PrintMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();

                PrintElementsMenu();
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1) counter = menuItems.Length - 1;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == menuItems.Length) counter = 0;
                }
            }
            while (key.Key != ConsoleKey.Enter);


            return counter;
        }

        protected virtual void PrintElementsMenu()
        {
            for (int i = 0; i < menuItems.Length; i++)
            {

                if (counter == i)
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationSettings.AppSettings.Get("HighlightedColor"));
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationSettings.AppSettings.Get("HiglightedForegroundColor"));
                    Console.WriteLine(menuItems[i]);
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationSettings.AppSettings.Get("ConsoleColor"));
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationSettings.AppSettings.Get("TextColor"));
                }
                else
                    Console.WriteLine(menuItems[i]);

            }
        }

    }
}
