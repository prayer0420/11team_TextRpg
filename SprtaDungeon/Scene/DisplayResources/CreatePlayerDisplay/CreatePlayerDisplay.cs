using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class CreatePlayerDisplay : Display
    {
        public string Input { get; set; }
        public Point DisplayPoint { get; set; }

        private string playerName;

        public CreatePlayerDisplay(string playerName)
        {
            this.playerName = playerName;
            DisplayPoint = new Point(0, 0);
        }

        public void Display()
        {
            Console.Clear();
            Console.Write("당신의 이름은 " + playerName + "입니다.\n다음은 직업을 정해주십시오.\n\n1. 기사\n2. 궁수\n3. 마법사\n\n");
            // 여기까지 6줄
        }

        int Display.Select()
        {
            while(true)
            {
                Console.Write("직업 번호를 입력해 주십시오.\n>> ");
                Input = Console.ReadLine();

                if (int.TryParse(Input, out int result) && result > 0 && result < 4) return result;

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, 8);
                Console.Write("                                                                                                                \n                       ");
                DisplayPoint.Set(0, 7);
            }
        }
    }
}
