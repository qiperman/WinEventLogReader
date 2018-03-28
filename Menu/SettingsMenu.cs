using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;

namespace WinEventLogReader
{
    //Меню настроек
    class SettingsMenu
    {
        static IEnumerable<string> items { get; set; } = new string[] { "Цвет выделения", "Цвет консоли", "Цвет текста выделенного меню", "Цвет текста" };
        static ConsoleMenu menu { get; set; } = new ConsoleMenu(items);
        delegate void method();

        static public void Print()
        {
            //Методы меню 
            method[] methods = new method[] { HighlightedColor, ConsoleColor, HiglightedForegroundColor, TextColor };

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
            ChangeAppSettings("HighlightedColor", ColorMenu());

        }

        static void HiglightedForegroundColor()
        {
            ChangeAppSettings("HiglightedForegroundColor", ColorMenu());

        }

        static void TextColor()
        {
            ChangeAppSettings("TextColor", ColorMenu());
        }

        static void ConsoleColor()
        {
            ChangeAppSettings("ConsoleColor", ColorMenu());
        }

        static private void ChangeAppSettings(string name, string color)
        {

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings[name].Value = color;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            ApplicationMainMenu.Print();
        }

        static private string ColorMenu()
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
