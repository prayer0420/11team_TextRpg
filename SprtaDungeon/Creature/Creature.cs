﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class Creature
    {
        public int _Lv { get; set; }
        public string _Name { get; set; }
        public int _Atk { get; set; }
        public int _Def { get; set; }
        public float _Critical { get; set; }
        public float _Avoid { get; set; }
        public int _Speed { get; set; }
        public int _MaxHp { get; set; }
        public int _CurHp { get; set; }
        public int _MaxMp { get; set; }
        public int _CurMp { get; set; }
        public int _Exp { get; set; }
        public string _Job { get; set; }
        public int _Gold { get; set; }


        public int _ExtraAtk { get; set; }
        public int _ExtraDef { get; set; }

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

        public virtual int Attack(bool critical) //hit = true -> Player 공격, hit = false -> Monster공격
        {
            return _Atk;
        }

        public virtual bool Critical(int randomValue)    //치명타 결과메서드 실행 시 true반환되면 치명타가 터진거다.
        {
            return false;
        }

        public virtual bool Avoid(int randomValue)         //회피 결과 메서드 실행 시 true로 반환되면 회피한거다.
        {
            int avoidnum = (int)(_Avoid * 100);
            return randomValue < avoidnum;
        }

        public virtual int Speed()
        {
            return _Speed;
        }

        public void ApplyDamage(int damage)
        {
            _CurHp -= damage;
            if (_CurHp < 0) { _CurHp = 0; }
        }
    }
}
