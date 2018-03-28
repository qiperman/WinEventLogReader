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

        //Настройка цвета выделения текста 
        static void HighlightedColor()
        {
            ChangeAppSettings("HighlightedColor");
        }

        //Настройка цвета текста выделенного меню 
        static void HiglightedForegroundColor()
        {
            ChangeAppSettings("HiglightedForegroundColor");
        }

        //Настройка цвета текста 
        static void TextColor()
        {
            ChangeAppSettings("TextColor");
        }

        //Настройка цвета консоли
        static void ConsoleColor()
        {
            ChangeAppSettings("ConsoleColor");
        }

        //Сохранение изменений
        static private void ChangeAppSettings(string name)
        {
            //Выбранный цвет
            string color = ColorMenu();
            //Если цвет есть 
            if (color != null)
            {

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings[name].Value = color;    //устанавливаем цвет
                config.Save(ConfigurationSaveMode.Modified);        //сохраняем
                ConfigurationManager.RefreshSection("appSettings"); //Обновляем настройки
            }
        }

        //Выбор цвета 
        static private string ColorMenu()
        {
            string[] colors = Enum.GetNames(typeof(ConsoleColor)); //Массив доступных цветов
            ColorMenu colorsMenu = new ColorMenu(colors); //Цветное меню 

            int menuResult;
            do
            {
                menuResult = colorsMenu.PrintMenu();
                if (menuResult != -1) return colors[menuResult]; //Если выбрали цвет то возвращяем его
                return null; //Если нажали esc возвращаем null
                
            } while (true);

        }
        

    }
}
