using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace WinEventLogReader
{
    class EventLogReader
    {
        private EventLog eventLog { get; }  //объект,который обеспечивает взаимодействие с журналами событий Windows.

        public EventLogReader(string EventLogName)
        {
            eventLog = new EventLog();
            ChangeLog(EventLogName);

        }

        //Метод вывода на консоль, событий произошедших за сутки
        public void PrintDayEvents()
        {
            try
            {
                //Создаем массив и копируем туда Entries, для того чтобы работать с Linq
                EventLogEntry[] EventLogEntryArray = new EventLogEntry[eventLog.Entries.Count];
                eventLog.Entries.CopyTo(EventLogEntryArray, 0);

                //Выводим в консоль события за последние сутки(Время сейчас - 24 часа)
                Print(EventLogEntryArray.Where(d => d.TimeWritten >= DateTime.Now.AddHours(-24)));
            }
            catch
            {
                Console.WriteLine("Произошла ошибка");
            }

        }

        //Метод смены журнала
        public void ChangeLog(string EventLogName)
        {
            //Проверка на существование журнала в системе 
            if (!EventLog.SourceExists(EventLogName))
            {
                //Журнала нет
                Console.WriteLine("Журнала нет");
                //Обнуляем журнал, для того чтобы при выводе на консоль не выводился старый журнал
                eventLog.Log = "";
            }
            else
            {
                //Создаем объект типа EventLog с журналом EventLogName
                eventLog.Log = EventLogName;
            }
        }

        //Метод вывода на экран событий, переданных по параметру
        private void Print(IEnumerable<EventLogEntry> EventLogEntryDayArray)
        {
            Console.WriteLine($"Название журнала: {eventLog.Log}"); //Вывод названия журнала
            Console.WriteLine($"Количество записей:\n  Всего: {eventLog.Entries.Count}");//Вывод количества записей, всего и за последние сутки
            Console.WriteLine($"  За последние сутки({DateTime.Now.AddHours(-24)}-{DateTime.Now}): {EventLogEntryDayArray.Count()}\n");

            Console.WriteLine($" {"Тип записи",-15} | {"Количество",10} | {"Последняя запись"}");//Вывод заголовков таблицы таблицы
            Console.WriteLine(" ---------------------------------------------------");

            //Цикл по уникальный значениям типа события 
            foreach (EventLogEntryType ev in EventLogEntryDayArray.Select(t => t.EntryType).Distinct())
            {
                //Вывод типа события, количества, и время последней записи
                Console.WriteLine($" {ev,-15} | {EventLogEntryDayArray.Where(t => t.EntryType == ev).Count(),10} | {EventLogEntryDayArray.Where(t => t.EntryType == ev).Last().TimeWritten}");
            }
            Console.WriteLine("\n");
        }

    }
}
