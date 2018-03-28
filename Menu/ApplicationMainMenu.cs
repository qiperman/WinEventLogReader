using System;


namespace WinEventLogReader
{

    //Класс главного меню
    class ApplicationMainMenu:Menu
    {
        public ApplicationMainMenu()
            : base(new string[] { "Выбрать журнал", "Настройки", "Выход" }, new method[] { ShowEnentLogs, Settings, Exit })
        { }
       
        //Выводим список журналов на локальном компьютере
        static void ShowEnentLogs()
        {
            EventLogMenu evMenu = new EventLogMenu();
            evMenu.Print();
        }

        static void Settings()
        {
            SettingsMenu setMenu = new SettingsMenu();
             setMenu.Print();
        }

        //Выход из приложения
        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
