using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class InventoryDisplayItem : Display
    {
        public string Input { get; set; }
        public Point DisplayPoint { get; set; }

        Inventory inventory;

        public InventoryDisplayItem()
        {
            inventory = Inventory.GetInstance();
            DisplayPoint = new Point(0, 0);
        }

        void Display.Display()
        {
            Console.Clear();
            Console.WriteLine("인벤토리\n");

            inventory.DisplayInventory();

            Console.WriteLine("장착할 장비를 입력해주세요... 0 : 돌아가기");
        }

        int Display.Select()
        {
            while (true)
            {
                Console.Write(">> ");
                Input = Console.ReadLine();

                if (int.TryParse(Input, out int result) && result > -1 && result < inventory.Items.Count + 1) return result;

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, Console.CursorTop - 1);
                Console.Write("                                                                                                                \n                       ");
                DisplayPoint.Set(0, Console.CursorTop - 1);
            }
        }
    }
}
