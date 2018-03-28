using System;


namespace WinEventLogReader
{
    class Program
    {
        static void Main()
        {

            //Заголовок консоли
            Console.Title = "Event Log Reader";

            //Вызов главного меню
            ApplicationMainMenu appMenu = new ApplicationMainMenu();
            appMenu.Print();
        }

    }  

}
