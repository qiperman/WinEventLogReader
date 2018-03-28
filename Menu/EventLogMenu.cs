using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace WinEventLogReader
{
    //Список журналов
    class EventLogMenu
    {
        static IEnumerable<string> items { get; set; } = EventLog.GetEventLogs().Select(n => n.Log);
        static ConsoleMenu menu { get; set; } = new ConsoleMenu(items);

        //Выводим список журналов как пункты меню
        static public void PrintEventMenu()
        {
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                EventChangeLog(items.ToArray()[menuResult]);
                Console.WriteLine("Нажмите Enter для выхода в главное меню");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    ApplicationMainMenu.Print();
                }

            } while (true);
        }

        //Делаем тут что то с выбранным журналом
        static void EventChangeLog(string LogName)
        {
            Console.Clear();
            EventLogReader eventLogReader = new EventLogReader(LogName);
            eventLogReader.PrintDayEvents();

        }
    }
}
