﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SprtaDungeon.Action;

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
        private Creature[]      creatures;

        public DungeonDisplaySkill(int cursorY, Creature[] creatures)
        {
            DisplayPoint = new Point(0, 0);
            targetSelect = false;
            monsterAmount = 0;
            this.creatures = creatures;

            skillCount = (creatures[0] as Player).skills.Count;
            skillDisplay = new SkillDisplay(cursorY, creatures[0], true);
        }
        public DungeonDisplaySkill(Creature[] creatures)
        {
            DisplayPoint = new Point(0, 0);
            this.monsterAmount = creatures.Length - 1;
            this.creatures = creatures;

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

                if (int.TryParse(Input, out int result) && result > -1 && result < resultMax)
                {
                    if (targetSelect)
                    {
                        if (creatures[result]._CurHp > 0)
                        {
                            return result;
                        }
                        Console.Write("이미 쓰러진 적입니다. ");
                    }
                    else
                    {
                        if (result == 0) return 0;

                        if ((GameManager.Instance.Player as Player).skills[result - 1]._SkillMp <= (GameManager.Instance.Player as Player)._CurMp)
                        {
                            return result;
                        }
                        Console.Write("마나가 부족합니다.");
                    }
                }

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, Console.CursorTop - 1);
                Console.Write("                                                                                                                \n                                                                     ");
                DisplayPoint.Set(0, Console.CursorTop - 1);
            }
        }
    }
}
