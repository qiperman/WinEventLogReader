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
        {

        }

        public override void DoMethod(int menuResult)
        {
            if (menuResult != -1)
            {
                EventChangeLog(items.ToArray()[menuResult]);
                Console.ReadKey();
            }
        }

        static void EventChangeLog()
        {
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
