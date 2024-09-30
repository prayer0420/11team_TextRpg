using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SprtaDungeon
{
    public class Monster : Creature
    {
        public override int Attack()
        {
            return _Atk;
        }
    }
    public class Minion : Monster
    {
        public Minion()
        {
            _Name = "미니언";
            _Lv = 2;
            _Hp = 15;
            _Atk = 5;
            _Def = 3;
        }
    }
    public class Voidling : Monster
    {
        public Voidling()
        {
            _Name = "공허충";
            _Lv = 3;
            _Hp = 10;
            _Atk = 9;
            _Def = 5;
        }
    }
    public class CannonMinion : Monster
    {
        public CannonMinion()
        {
            _Name = "대포미니언";
            _Lv = 5;
            _Hp = 25;
            _Atk = 8;
            _Def = 8;
        }
    }
}
