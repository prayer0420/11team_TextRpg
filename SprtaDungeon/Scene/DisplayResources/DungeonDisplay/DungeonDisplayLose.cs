using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayLose : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        public DungeonDisplayLose() { }

        void Display.Display()
        {
            Console.WriteLine("패배했습니다. 메인으로 돌아갑니다...");
        }

        int Display.Select()
        {
            Console.ReadLine();
            return 0;
        }
    }
}
