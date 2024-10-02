using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class MainDisplay : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        public MainDisplay()
        {
            DisplayPoint = new Point(0, 0);
        }

        void Display.Display()
        {
            Console.Clear();
            Console.Write("스파르타 마을에 오신 여러분 환영합니다.\n" +
                          "이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n" +
                          "\n" +
                          "1. 상태 보기\n" +
                          "2. 인벤토리\n" +
                          "4. 상점\n" +
                          "5. 던전 입장\n" +
                          "6. 퀘스트\n" +
                          "7. 저장하기\n" +
                          "8. 게임 종료\n" +
                          "\n" +
                          "원하시는 행동을 입력해주세요.\n");
            // 입력 라인 = 12
        }

        int Display.Select()
        {
            while(true)
            {
                Console.Write(">> ");
                Input = Console.ReadLine();

                if (int.TryParse(Input, out int result) && result > -1 && result < 8) return result;

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, Console.CursorTop - 1);
                Console.Write("                                                                                                                \n                       ");
                DisplayPoint.Set(0, Console.CursorTop - 1);
            }
        }
    }
}
