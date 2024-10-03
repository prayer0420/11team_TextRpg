using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayApplyDamage : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        private Monster monster;
        private int damage;

        public DungeonDisplayApplyDamage(Monster monster, int damage)
        {
            DisplayPoint = new Point(0, 0);

            this.monster = monster;
            this.damage = damage;
        }

        void Display.Display()
        {
            Console.WriteLine(monster._Name + "에게 " +  damage + "데미지를 주었다!");
        }

        int Display.Select()
        {
            throw new NotImplementedException();
        }
    }
}
