using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace WinEventLogReader
{
    class Program
    {
        static void Main()
        {

            //Вызов главного меню
            ApplicationMainMenu.Print();
        }

    }

    //Класс главного меню
    class ApplicationMainMenu
    {
        //Элементы меню
        static IEnumerable<string> items { get; set; } = new string[]{"Выбрать журнал","Выход"};
        //Меню
        static ConsoleMenu menu { get; set; } = menu = new ConsoleMenu(items);

        static public void Print()
        {
            //Методы меню 
            method[] methods = new method[] { ShowEnentLogs, Exit };

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

        //Выход из приложения
        static void Exit()
        {
            Environment.Exit(0);
        }
    }

    //Список журналов
    class EventLogMenu
    {
        static IEnumerable<string> items { get; set; } = EventLog.GetEventLogs().Select(n => n.Log);
        static ConsoleMenu menu { get; set; } = menu = new ConsoleMenu(items);

        //Выводим список журналов как пункты меню
        static public void PrintEventMenu()
        {
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                EventChangeLog(items.ToArray()[menuResult]);
                ApplicationMainMenu.Print();
            } while (true);
        }

        //Делаем тут что то с выбранным журналом
        static void EventChangeLog(string LogName)
        {
            Console.Clear();
            Console.WriteLine($"Вы выбрали {LogName}");
            Console.ReadKey();

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
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
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
