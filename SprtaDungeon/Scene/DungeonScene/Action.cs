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
            ITEM,
            QUIT
        }

        public enum Creature
        {
            PLAYER,
            MONSTER
        }

        public Behavior behavior;
        public Creature turn;
        public int num;
    }
}
