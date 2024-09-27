using SprtaDungeon.Scene.DisplayResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{

    /*
     *  몬스터 A에게 10 데미지를 입혔다!
     * 
     */
    public class DungeonDisplayApplyDamage : Display
    {
        public Point DisplayPoint { get; set; }

        private Monster monster;
        private int damage;

        public DungeonDisplayApplyDamage(int x, int y, Monster monster, int damage)
        {
            DisplayPoint = new Point(x, y);

            this.monster = monster;
            this.damage = damage;
        }

        void Display.Display()
        {
            throw new NotImplementedException();
        }

        int Display.Select()
        {
            throw new NotImplementedException();
        }
    }
}
