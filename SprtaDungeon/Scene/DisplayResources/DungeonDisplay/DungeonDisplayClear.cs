using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayClear : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        private int gold;

        public DungeonDisplayClear(int gold) { this.gold = gold; }

        void Display.Display()
        {
            Console.WriteLine("던전을 클리어 하셨습니다! 보상으로 " + gold + "G를 얻었습니다.\n메인으로 돌아갑니다...");
        }

        int Display.Select()
        {
            Console.ReadLine();
            return 0;
        }
    }
}
