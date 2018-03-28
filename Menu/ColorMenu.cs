using System;
using System.Collections.Generic;
using System.Configuration;

namespace WinEventLogReader
{

    class ColorMenu : ConsoleMenu
    {
        public ColorMenu(IEnumerable<string> menuItems) : base(menuItems)
        {
        }


        protected override void PrintElementsMenu()
        {
            for (int i = 0; i < menuItems.Length; i++)
            {

                if (counter == i)
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationManager.AppSettings.Get("HighlightedColor"));
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, menuItems[i]);
                    Console.WriteLine(menuItems[i]);
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationManager.AppSettings.Get("ConsoleColor"));
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, menuItems[i]);
                }
                else
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationManager.AppSettings.Get("ConsoleColor"));
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, menuItems[i]);
                    Console.WriteLine(menuItems[i]);

                }
            }
        }
    }
}
