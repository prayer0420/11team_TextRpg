using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SprtaDungeon
{
    public class Player : Creature
    {
        public List<Skill> skills = new List<Skill>();
        public Inventory inventory;

        Random random = new Random();

        public Player(string name)
        {
           _Name = name;
            inventory = Inventory.GetInstance();
            _Gold = 3000;
        }

        public override int Attack(bool critical) //공격력 출력, 공격력 출력할때 치명타 계산이 들어간 데미지가 나간다.
        {
            if (critical)
            {
                return (int)Math.Round(_Atk * 1.6f);
            }
            else
            {
                return _Atk;
            }
        }

        public override bool Critical(int randomValue)    //치명타 결과메서드 실행 시 true반환되면 치명타가 터진거다.
        {
            int criticalnum = (int)(_Critical * 100);
            return randomValue < criticalnum;
        }

        public override int Speed()
        {
            return _Speed;
        }

        public void ExtraAtkDef(bool choice, bool plus, int value)     //choice = true = 추가 공격력, choice = false 방어력 더해주기
        {
            if (plus)
            {
                if (choice)
                {
                    _ExtraAtk += value;
                    _Atk += _ExtraAtk;
                }
                else {
                    _ExtraDef += value;
                    _Def += _ExtraDef;
                }
            }
            else
            {
                if (choice)
                {
                    _Atk -= _ExtraAtk;
                    _ExtraAtk -= value;
                }
                else
                {
                    _Def -= _ExtraDef;
                    _ExtraDef -= value;
                }
            }
        }

        public string ExpGet(int exp)                    //경험치를 던전이나 퀘스트에서 받아야한다.!!!
        {
            _Exp += exp;
            switch (_Lv)
            {
                case 1:
                    if(_Exp >= 10)
                    {
                        _Exp -= 10;
                        LvUp();
                        return "축하합니다. 1 -> 2 레벨업 하였습니다";
                    }
                    break;
                case 2:
                    if (_Exp >= 35)
                    {
                        _Exp -= 35;
                        LvUp();
                        return "축하합니다. 2 -> 3 레벨업 하였습니다";
                    }
                    break;
                case 3:
                    if (_Exp >= 65)
                    {
                        _Exp -= 65;
                        LvUp();
                        return "축하합니다. 3 -> 4 레벨업 하였습니다";
                    }
                    break;
                case 4:
                    if (_Exp >= 100)
                    {
                        _Exp -= 100;
                         LvUp();
                        return "축하합니다. 4 -> 5 레벨업 하였습니다";
                    }
                    break;
                default:
                    break;
            }
            return $"현재 경험치는 {_Exp} 입니다";
        }

        public void Heal()
        {
            _CurHp = _MaxHp;
        }
        public void Heal(int value)
        {
            _CurHp += value;
            if (_CurHp > _MaxHp) _CurHp = _MaxHp;
        }

        public void ChargeMp()
        {
            _CurMp = _MaxMp;
        }
        public void ChargeMp(int value)
        {
            _CurMp += value;
            if (_CurMp > _MaxMp) _CurMp = _MaxMp;
        }


        public void LvUp()  //레벨업 할때 계산되는 함수
        {
            _Lv++;
            _Atk += 1;
            _Def += 2;
        }

        public void SkillListAdd(Skill skill)
        {
            skills.Add(skill);
        }

        public void SkillUI(int indexnum)       //이거 어떻게 Display로 옮기지...?
        {
            int num = indexnum - 1;
            Console.WriteLine($"{indexnum}. {skills[num]._SkillName} -  Mp {skills[num]._SkillMp}");
            Console.WriteLine($"   {skills[num]._SkillExplanation}");
        }

        public int SkillAttack(int indexnum, bool critical)        //스킬 공격(치명타 적용됨)
        {
            _CurMp -= skills[indexnum]._SkillMp;

            if (critical)
            {
                return (int)Math.Round(skills[indexnum]._SkillAtk * _Atk * 1.6f);
            }
            else
            {
                return (int)Math.Round(skills[indexnum]._SkillAtk * _Atk);
            }
        }
    }

    public class Knight : Player
    {
        public Knight(string name) : base(name)  //유저 이름
        {
            _Lv = 1;
            _Job = "Knight";
            _Atk = 5;
            _Def = 10;
            _Speed = 5;
            _MaxHp = 100;
            _MaxMp = 50;
            _CurHp = _MaxHp;
            _CurMp = _MaxMp;

            SkillListAdd(new Skill("알파 스트라이크", "공격력 * 2 로 하나의 적을 공격합니다.", 10, 2.0f, 1));
            SkillListAdd(new Skill("더블 스트라이크", "공격력 * 3 로 하나의 적을 공격합니다.", 15, 1.5f, 1));
        }
    }

    public class Archor : Player
    {
        public Archor(string name) : base(name)   //유저 이름
        {
            _Lv = 1;
            _Job = "Archor";
            _Atk = 8;
            _Def = 7;
            _Speed = 10;
            _MaxHp = 80;
            _MaxMp = 50;
            _CurHp = _MaxHp;
            _CurMp = _MaxMp;

            SkillListAdd(new Skill("헤드샷", "공격력 * 2.5 로 하나의 적을 공격합니다.", 10, 2.5f, 1));
            SkillListAdd(new Skill("다중사격", "공격력 * 3 로 하나의 적을 공격합니다.", 15, 3.0f, 1));
        }
    }

    public class Mage : Player
    {
        public Mage(string name) : base(name)   //유저 이름
        {
            _Lv = 1;
            _Job = "Mage";
            _Atk = 10;
            _Def = 4;
            _Speed = 6;
            _MaxHp = 60;
            _MaxMp = 50;
            _CurHp = _MaxHp;
            _CurMp = _MaxMp;

            SkillListAdd(new Skill("화염 강타", "공격력 * 3.0 로 하나의 적을 공격합니다.", 10, 3.0f, 1));
            SkillListAdd(new Skill("화염 폭발", "공격력 * 4 로 하나의 적을 공격합니다.", 20, 4f, 1));
        }
    }
}

