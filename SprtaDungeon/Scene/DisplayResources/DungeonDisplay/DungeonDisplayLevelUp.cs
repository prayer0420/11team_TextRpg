using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayLevelUp : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        public DungeonDisplayLevelUp() { }

        void Display.Display()
        {
            Console.WriteLine("방을 클리어 했습니다. 레벨이 올랐습니다!");
        }

        int Display.Select()
        {
            Console.ReadLine();
            return 0;
        }
    }
}
