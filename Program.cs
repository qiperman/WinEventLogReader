using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace WinEventLogReader
{
    class Program
    {
        static void Main()
        {
            Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ConfigurationSettings.AppSettings.Get("ConsoleColor"));
            //Вызов главного меню
            ApplicationMainMenu.Print();
        }

    }

    //Класс главного меню
    class ApplicationMainMenu
    {
        //Элементы меню
        static IEnumerable<string> items { get; set; } = new string[]{"Выбрать журнал","Настройки","Выход"};
        //Меню
        static ConsoleMenu menu { get; set; } = menu = new ConsoleMenu(items);

        static public void Print()
        {
            //Методы меню 
            method[] methods = new method[] { ShowEnentLogs, Settings ,Exit };

            //Выбранное меню
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                //Выполняем действие меню
                methods[menuResult]();
                Console.ReadKey();
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

    //Меню настроек
    class SettingsMenu
    {
        static IEnumerable<string> items { get; set; } = new string[] { "Цвет выделения", "Цвет консоли" };
        static ConsoleMenu menu { get; set; } = new ConsoleMenu(items);
        delegate void method();

        static public void Print()
        {
            //Методы меню 
            method[] methods = new method[] { HighlightedColor, ChangeConsoleColor };

            //Выбранное меню
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                //Выполняем действие меню
                methods[menuResult]();
                Console.ReadKey();
            } while (true);
        }

        static void HighlightedColor()
        {
            ConfigurationSettings.AppSettings.Set("HighlightedColor", ColorMenu());
        }

        static void ChangeConsoleColor()
        {
            ColorMenu();
        }

        private static string ColorMenu()
        {
            string[] colors = Enum.GetNames(typeof(ConsoleColor));
            ConsoleMenu colorsMenu = new ConsoleMenu(colors);

            int menuResult;
            do
            {
                menuResult = colorsMenu.PrintMenu();
                return colors[menuResult];
            } while (true);

        }

    }

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

    //Класс меню
    class ConsoleMenu
    {
        string[] menuItems;
        int counter = 0;

        public ConsoleMenu(IEnumerable<string> menuItems)
        {
            this.menuItems = menuItems.ToArray() ;
        }

        public int PrintMenu()
        {
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();

                for (int i = 0; i < menuItems.Length; i++)
                {

                    if (counter == i)
                    {
                        Type type = typeof(ConsoleColor);
                        Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationSettings.AppSettings.Get("HighlightedColor"));
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationSettings.AppSettings.Get("HiglightedForegroundColor"));
                        Console.WriteLine(menuItems[i]);
                        Console.BackgroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationSettings.AppSettings.Get("ConsoleColor"));
                        Console.ForegroundColor = (ConsoleColor)Enum.Parse(type, ConfigurationSettings.AppSettings.Get("TextColor"));
                    }
                    else
                        Console.WriteLine(menuItems[i]);

                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1) counter = menuItems.Length - 1;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == menuItems.Length) counter = 0;
                }
            }
            while (key.Key != ConsoleKey.Enter);


            return counter;
        }

    }



}
