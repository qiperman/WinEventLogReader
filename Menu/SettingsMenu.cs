using System;
using System.Collections.Generic;
using System.Configuration;

namespace WinEventLogReader
{
    //Меню настроек
    class SettingsMenu
    {
        static IEnumerable<string> items { get; set; } = new string[] { "Цвет выделения", "Цвет консоли" };
        static ConsoleMenu menu { get; set; } = new ConsoleMenu(items);
        delegate void method();

        static public void Print()
        {
            //Методы меню 
            method[] methods = new method[] { HighlightedColor, ChangeConsoleColor };

            //Выбранное меню
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                //Выполняем действие меню
                methods[menuResult]();
                Console.ReadKey();
            } while (true);
        }

        static void HighlightedColor()
        {
            ConfigurationSettings.AppSettings.Set("HighlightedColor", ColorMenu());
        }

        static void ChangeConsoleColor()
        {
            ColorMenu();
        }

        private static string ColorMenu()
        {
            string[] colors = Enum.GetNames(typeof(ConsoleColor));
            ColorMenu colorsMenu = new ColorMenu(colors);

            int menuResult;
            do
            {
                menuResult = colorsMenu.PrintMenu();
                return colors[menuResult];
            } while (true);

        }

    }
}
