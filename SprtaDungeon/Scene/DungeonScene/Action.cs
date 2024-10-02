using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public struct Action
    {
        public enum Behavior
        {
            BASIC_ATTACK,
            SKILL_ATTACK,
            POTION,
            QUIT
        }

        public enum Creature
        {
            PLAYER = 0,
            MONSTER
        }

        public Behavior         behavior;
        public Creature         turn;
        public int              creatureNum; // 1~3 if Monster
        public int              speed;

        public int              targetNum;
        public int              skillNum;
        public int              itemNum;
    }
}
