using System;
using System.Collections.Generic;
using System.Configuration;

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
                if (menuResult!=-1) methods[menuResult]();
                
            } while (menuResult !=-1);
            ApplicationMainMenu.Print();
        }

        static void HighlightedColor()
        {
            ChangeAppSettings("HighlightedColor");

        }

        static void HiglightedForegroundColor()
        {
            ChangeAppSettings("HiglightedForegroundColor");

        }

        static void TextColor()
        {
            ChangeAppSettings("TextColor");
        }

        static void ConsoleColor()
        {
            ChangeAppSettings("ConsoleColor");
        }

        static private void ChangeAppSettings(string name)
        {
            string color = ColorMenu();
            if (color != null)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[name].Value = color;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                Print();
            }
        }

        static private string ColorMenu()
        {
            string[] colors = Enum.GetNames(typeof(ConsoleColor));
            ColorMenu colorsMenu = new ColorMenu(colors);

            int menuResult;
            do
            {
                menuResult = colorsMenu.PrintMenu();
                if (menuResult != -1) return colors[menuResult];

                Print();
                return null;
                
            } while (true);

        }

    }
}
