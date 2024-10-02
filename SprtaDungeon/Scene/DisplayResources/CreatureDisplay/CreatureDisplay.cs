using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class CreatureDisplay : Display
    {
        private Creature                creature;
        private bool                    isPlayer;
        private bool                    isDisplayNumber;
        private bool                    isBattle;
        private int                     count = 1;

        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        public CreatureDisplay(Creature creature, bool isDisplayNumber, bool isBattle)
        {
            DisplayPoint = new Point(0, 0);
            this.creature = creature;
            this.isDisplayNumber = isDisplayNumber;
            this.isBattle = isBattle;

            isPlayer = creature is Player;
        }

        void Display.Display() //케릭터 상태보기 씬
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            CreatureStatus();
            Console.WriteLine("\n0.나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }

        public void CreatureStatus()
        {
            if (isPlayer)
            {
                Console.WriteLine($"Lv. {creature._Lv:D2}");
                Console.WriteLine($"Chad ( {creature._Job} )");
                Console.WriteLine($"HP : {creature._Hp}");
            }
            else
            {
                string monsterStat = $"Lv.{creature._Lv} {creature._Name.PadRight(4)} HP {creature._Hp}";
                Console.WriteLine(isDisplayNumber ? monsterStat : $"{count++} " + monsterStat);
            }

            Console.WriteLine((creature._ExtraAtk == 0) ? $"공격력 : {creature._Atk}" : $"공격력 : {creature._Atk + creature._ExtraAtk} (+{creature._ExtraAtk})");
            Console.WriteLine((creature._ExtraDef == 0) ? $"방어력 : {creature._Def}" : $"공격력 : {creature._Def + creature._ExtraDef} (+{creature._ExtraDef})");

            if (isPlayer && !isBattle)
            {
                Console.WriteLine($"Gold : {creature._Gold}");
            }

            // 여기까지 13
        }

        int Display.Select()
        {
            while (true)
            {
                Console.Write(">> ");
                Input = Console.ReadLine();

                if (int.TryParse(Input, out int result) && result == 0) return result;

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, 13);
                Console.Write("                                                                                                                \n                       ");
                DisplayPoint.Set(0, 13);
            }
        }
    }
}
