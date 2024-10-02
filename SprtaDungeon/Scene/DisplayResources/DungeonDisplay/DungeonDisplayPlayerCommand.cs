using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayPlayerCommand : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        public DungeonDisplayPlayerCommand()
        {
            DisplayPoint = new Point(0, 0);
        }

        void Display.Display()
        {
            Console.WriteLine("1. 공격\n" +
                              "2. 스킬\n" +
                              "3. 포션\n" +
                              "4. 도망친다\n" +
                              "\n" + 
                              "원하시는 행동을 입력해주세요.");
        }

        int Display.Select()
        {
            while (true)
            {
                Console.Write(">> ");
                Input = Console.ReadLine();

                if (int.TryParse(Input, out int result) && result > 0 && result < 5) return result - 1;

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, Console.CursorTop - 1);
                Console.Write("                                                                                                                \n                       ");
                DisplayPoint.Set(0, Console.CursorTop - 1);
            }
        }
    }
}
