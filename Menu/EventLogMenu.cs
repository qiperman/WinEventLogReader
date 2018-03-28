using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace WinEventLogReader
{
    //Список журналов
    class EventLogMenu:Menu
    {
        public EventLogMenu()
            : base(EventLog.GetEventLogs().Select(n => n.Log),new method[] { EventChangeLog })
        {}

        //Переопределяем метод, который выполняет действие в меню
        public override void DoMethod(int menuResult)
        {
            if (menuResult != -1)
            {
                //Выполняем действие
                EventChangeLog(items.ToArray()[menuResult]);
                Console.ReadKey();
            }
        }

        
        static void EventChangeLog(){} //Метод "заглушка" для базового класса

        //Выводим информацию о журнале за последние сутки
        static void EventChangeLog(string LogName)
        {
            Console.Clear();
            EventLogReader eventLogReader = new EventLogReader(LogName);
            eventLogReader.PrintDayEvents();

        }
    }
}
