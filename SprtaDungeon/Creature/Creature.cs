using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class Creature
    {
        public int _Lv { get; protected set; }
        public string _Name { get; protected set; }
        public int _Atk { get; protected set; }
        public int _Def { get; protected set; }
        public float _Critical { get; protected set; }
        public float _Avoid { get; protected set; }
        public int _Speed { get; protected set; }
        public int _Hp { get; protected set; }
        public int _Mp { get; protected set; }
        public int _Exp { get; protected set; }
        public string _Job { get; protected set; }
        public int _Gold { get; set; }


        public int _ExtraAtk { get; protected set; }
        public int _ExtraDef { get; protected set; }

        public enum CreatureType    
        {
            Knight, 
            Archor,
            Mage,
            Minion, 
            Voidling, 
            CannonMinion
        }

        public static Creature CreatureFactory(CreatureType creaturetype, string name)      //CreatureType과 사용자 이름 받기  //나중에 몬스터 부모클래스 생성해서 따로 해야됨 지금은 붙여서 사용
        {
            switch (creaturetype)
            {
                case CreatureType.Knight:
                    return new Knight(name);
                case CreatureType.Archor:
                    return new Archor(name);
                case CreatureType.Mage:
                    return new Mage(name);
                case CreatureType.Minion:
                    return new Minion();
                case CreatureType.Voidling:
                    return new Voidling();
                case CreatureType.CannonMinion:
                    return new CannonMinion();
            }
            throw new System.NotSupportedException($"{creaturetype} 이라는 타입은 존재하지 않습니다");
        }

        public virtual int Attack()//hit = true -> Player 공격, hit = false -> Monster공격
        {
            return _Atk;
        }

        public virtual int Speed()
        {
            return _Speed;
        }
    }
}
