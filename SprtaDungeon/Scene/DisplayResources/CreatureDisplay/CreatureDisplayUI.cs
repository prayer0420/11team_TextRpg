using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class CreatureDisplayUI : Display
    {
        
        private Creature creature;
        private int _inputNum;
        private int count = 1;

        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        public CreatureDisplayUI(Creature creature)
        {
            this.creature = creature;
            DisplayPoint = new Point(0, 0);
        }

        void Display.Display() //케릭터 상태보기 씬
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {creature._Lv:D2}");
            Console.WriteLine($"Chad ( {creature._Job} )");
            Console.WriteLine((creature._ExtraAtk == 0) ? $"공격력 : {creature._Atk}" : $"공격력 : {creature._Atk + creature._ExtraAtk} (+{creature._ExtraAtk})");
            Console.WriteLine((creature._ExtraDef == 0) ? $"방어력 : {creature._Def}" : $"공격력 : {creature._Def + creature._ExtraDef} (+{creature._ExtraDef})");
            Console.WriteLine($"체력 : {creature._Hp}");
            Console.WriteLine($"Gold : {creature._Gold}");
            Console.WriteLine("\n0.나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            if (int.TryParse(Console.ReadLine(), out _inputNum) && _inputNum == 0)
            {
                //게임 시작 화면 함수 실행할 곳
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(500);
            }
        }

        public void DisplayMonsterStatUI(int num) //0을 입력받으면 정보만 보여주는것  //1을 입력받으면 몬스터앞에 숫자를 표시해주는것
        {
            string monsterStat = $"Lv.{creature._Lv} {creature._Name.PadRight(4)} HP {creature._Hp}";
            Console.WriteLine(num == 0 ? monsterStat : $"{count++} " + monsterStat);
        }

        int Display.Select()
        {
            throw new NotImplementedException();
        }
    }
}
