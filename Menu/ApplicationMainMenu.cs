using System;
using System.Collections.Generic;


namespace WinEventLogReader
{

    //Класс главного меню
    class ApplicationMainMenu
    {
        //Элементы меню
        static IEnumerable<string> items { get; set; } = new string[] { "Выбрать журнал", "Настройки", "Выход" };
        //Меню
        static ConsoleMenu menu { get; set; } = menu = new ConsoleMenu(items);

        static public void Print()
        {
            //Методы меню 
            method[] methods = new method[] { ShowEnentLogs, Settings, Exit };

            //Выбранное меню
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                //Выполняем действие меню
                if (menuResult != -1) methods[menuResult]();
            } while (true);
        }

        delegate void method();

        //Выводим список журналов на локальном компьютере
        static void ShowEnentLogs()
        {
            EventLogMenu.PrintEventMenu();
        }

        static void Settings()
        {
            SettingsMenu.Print();
        }

        //Выход из приложения
        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
