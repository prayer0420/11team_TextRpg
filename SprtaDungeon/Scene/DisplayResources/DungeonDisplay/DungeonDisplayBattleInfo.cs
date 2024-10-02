using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayBattleInfo : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }
        private int monsterAmount;
        private CreatureDisplay[] creatureDisplays;

        public DungeonDisplayBattleInfo(Creature[] creatures)
        {
            monsterAmount = creatures.Length - 1;

            creatureDisplays = new CreatureDisplay[creatures.Length];
            for(int i = 0; i < creatureDisplays.Length; i ++)
            {
                creatureDisplays[i] = new CreatureDisplay(creatures[i], true, true);
            }
        }

        void Display.Display()
        {
            Console.Clear();
            for(int i = 1; i < monsterAmount; i ++)
            {
                creatureDisplays[i].CreatureStatus();
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------------\n");
            creatureDisplays[0].CreatureStatus();
            Console.WriteLine();
        }

        int Display.Select()
        {
            return 0;
        }
    }
}
