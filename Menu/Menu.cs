
using System.Collections.Generic;


namespace WinEventLogReader
{
    abstract class Menu
    {
        public IEnumerable<string> items { get; set; }
        protected ConsoleMenu menu { get; set; }
        public delegate void method();
        protected method[] methods;

        public Menu(IEnumerable<string> items, method[] methods)
        {
            this.items = items;
            menu = new ConsoleMenu(items);
            this.methods = methods;
        }

        public void Print()
        {
            //Выбранное меню
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                DoMethod(menuResult);

            } while (menuResult != -1);
            ApplicationMainMenu appMenu = new ApplicationMainMenu();
            appMenu.Print();
        }

        public virtual void DoMethod(int menuResult)
        {
            //Выполняем действие меню
            if (menuResult != -1) methods[menuResult]();
        }

    }
}
