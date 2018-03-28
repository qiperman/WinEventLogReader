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
            Console.Title = "Event Log Reader";


            //Вызов главного меню
            ApplicationMainMenu.Print();
        }

    }  

}
