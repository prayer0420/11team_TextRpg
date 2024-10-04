using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    internal class SkillDisplay : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        private Player              player;
        private bool                isBattle;
        private List<Skill>         skills;

        public SkillDisplay(int cursorY, Creature player, bool isBattle)
        {
            DisplayPoint = new Point(0, cursorY);
            DisplayPoint.Set();

            this.player = player as Player;
            this.isBattle = isBattle;
            skills = this.player.skills;
        }

        void Display.Display()
        {
            for(int i = 0; i < skills.Count; i ++)
            {
                if (isBattle) Console.Write((i + 1) + ". ");

                Skill skill = skills[i];
                Console.WriteLine(skill._SkillName + " : " + skill._SkillExplanation);
                Console.WriteLine("위력 : " + skill._SkillAtk + ", 소모 마나 : " + skill._SkillMp + "\n");
            }
        }

        int Display.Select()
        {
            throw new NotImplementedException();
        }
    }
}
