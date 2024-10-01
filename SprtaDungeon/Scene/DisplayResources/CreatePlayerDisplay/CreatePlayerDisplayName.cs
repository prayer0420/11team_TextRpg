using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class CreatePlayerDisplayName : Display
    {
        public string Input { get; set; }
        public Point DisplayPoint { get; set; }

        void Display.Display()
        {
            Console.Clear();
            Console.Write("스파르타 던전에 오신 것을 환영합니다.\n당신의 이름을 입력해주십시오.\n>> ");
        }

        int Display.Select()
        {
            Input = Console.ReadLine();
            return 0;
        }
    }
}
