using System;
using System.Configuration;

namespace WinEventLogReader
{
    //Меню настроек
    class SettingsMenu:Menu
    {
        public SettingsMenu()
            :base(new string[] { "Цвет выделения", "Цвет консоли", "Цвет текста выделенного меню", "Цвет текста" }, new method[] { HighlightedColor, ConsoleColor, HiglightedForegroundColor, TextColor })
        {}

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
                return null;
                
            } while (true);

        }
        

    }
}
