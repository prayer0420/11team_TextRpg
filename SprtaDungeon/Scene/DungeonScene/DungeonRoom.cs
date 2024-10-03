using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonRoom
    {
        private DungeonBattle           battle;
        private Random                  random;
        private int                     seed;

        public Creature[] Monsters { get; private set; }
        public int MonsterAmount { get; set; }

        public DungeonRoom(int seed)
        {
            random = new Random(seed);
            this.seed = seed;
            CreateMonsters();
        }

        public int EnterRoom()
        {
            battle = new DungeonBattle(Monsters, random.Next());

            return battle.StartBattle();
        }

        private void CreateMonsters()
        {
            MonsterAmount = random.Next(1, 4);

            Monsters = new Creature[MonsterAmount];

            for(int i = 0; i < Monsters.Length; i++) 
            {
                int monsterType = random.Next(3, 6);
                Monsters[i] = Creature.CreatureFactory((Creature.CreatureType)monsterType, "");
            }
        }
    }
}
