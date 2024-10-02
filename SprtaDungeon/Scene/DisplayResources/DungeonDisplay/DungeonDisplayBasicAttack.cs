using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayBasicAttack : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        private int                 monsterAmount;
        private Creature[]          creatures;

        public DungeonDisplayBasicAttack(int monsterAmount, Creature[] creatures)
        {
            DisplayPoint = new Point(0, 0);
            this.monsterAmount = monsterAmount;
            this.creatures = creatures;
        }

        void Display.Display()
        {
            Console.WriteLine("0. 돌아가기\n\n공격할 몬스터의 번호를 입력해주세요.");
        }

        int Display.Select()
        {
            while (true)
            {
                Console.Write(">> ");
                Input = Console.ReadLine();

                if (int.TryParse(Input, out int result) && result > -1 && result <= monsterAmount)
                {
                    if (creatures[result]._CurHp > 0) return result;
                    Console.Write("이미 쓰러진 적입니다. ");
                }

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, Console.CursorTop - 1);
                Console.Write("                                                                                                                \n                       ");
                DisplayPoint.Set(0, Console.CursorTop - 1);
            }
        }
    }
}
