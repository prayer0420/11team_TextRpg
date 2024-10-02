using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplaySkill : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        private int             skillCount;
        private int             monsterAmount;
        private bool            targetSelect;
        private Display         skillDisplay;


        public DungeonDisplaySkill(int cursorY, Creature player)
        {
            DisplayPoint = new Point(0, 0);
            targetSelect = false;
            monsterAmount = 0;

            skillCount = (player as Player).skills.Count;
            skillDisplay = new SkillDisplay(cursorY, player, true);
        }
        public DungeonDisplaySkill(int monsterAmount)
        {
            DisplayPoint = new Point(0, 0);
            this.monsterAmount = monsterAmount;

            targetSelect = true;
        }

        void Display.Display()
        {
            if(targetSelect)
            {
                Console.WriteLine("0. 돌아가기\n\n공격할 몬스터의 번호를 입력해주세요.");
                return;
            }

            skillDisplay.Display();

            Console.WriteLine("0. 돌아가기\n\n사용할 스킬 번호를 입력해주세요.");
        }

        int Display.Select()
        {
            int resultMax;
            if (targetSelect) resultMax = monsterAmount + 1;
            else resultMax = skillCount + 1;

            while (true)
            {
                Console.Write(">> ");
                Input = Console.ReadLine();

                if (int.TryParse(Input, out int result) && result > -1 && result < resultMax) return result;

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, Console.CursorTop - 1);
                Console.Write("                                                                                                                \n                       ");
                DisplayPoint.Set(0, Console.CursorTop - 1);
            }
        }
    }
}
