
using System.Collections.Generic;


namespace WinEventLogReader
{
    //абстрактный класс для всех меню
    abstract class Menu
    {
        public IEnumerable<string> items { get; set; } //элементы меню

        protected ConsoleMenu menu { get; set; } //меню
        public delegate void method(); 
        protected method[] methods;//действия меню

        public Menu(IEnumerable<string> items, method[] methods)
        {
            this.items = items;
            menu = new ConsoleMenu(items);
            this.methods = methods;
        }

        //Выводим меню
        public void Print()
        {
            //Выбранное меню
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu(); //получаем номер выбранного пункта меню
                DoMethod(menuResult); //выполняем метод

            } while (menuResult != -1); //выполняем пока не нажат esc
            ApplicationMainMenu appMenu = new ApplicationMainMenu(); //выходим в главное меню 
            appMenu.Print();
        }

        public virtual void DoMethod(int menuResult)
        {
            //Выполняем действие меню если не нажат esc
            if (menuResult != -1) methods[menuResult]();
        }

    }
}
