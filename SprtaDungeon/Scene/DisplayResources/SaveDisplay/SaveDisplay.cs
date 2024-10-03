using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    class SaveDisplay : Display
    {
        public string Input { get; set; }
        public Point DisplayPoint { get; set; }

        void Display.Display()
        {
            Console.WriteLine("저장되었습니다.");
        }

        int Display.Select()
        {
            Console.ReadLine();
            return 0;
        }
    }
}
