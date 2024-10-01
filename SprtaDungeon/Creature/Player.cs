using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SprtaDungeon
{
    public class Player : Creature
    {
        private int _inputNum;

        Random random = new Random();

        public Player(string name)
        {
           _Name = name;
        }


        public void ExtraAtkDef()     //매개변수값 받아 추가 공격력, 방어력 더해주기
        {

        }

        public override int Attack() //공격력 출력, 공격력 출력할때 치명타 계산이 들어간 데미지가 나간다.
        {
            if (Critical())
            {
                return (int)Math.Round(_Atk * 1.6f);
                Console.WriteLine("치명타 공격");
            }
            else
            {
                return _Atk;
            }
        }

        public bool Critical()    //치명타 결과메서드 실행 시 true반환되면 치명타가 터진거다.
        {
            int randomvalue = random.Next(0, 100);
            int criticalnum = (int)(_Critical * 100);
            return Percent(randomvalue, criticalnum);
        }

        public bool Avoid()         //회피 결과 메서드 실행 시 true로 반환되면 회피한거다.
        {
            int randomvalue = random.Next(0, 100);
            int avoidnum = (int)(_Avoid * 100);
            return Percent(randomvalue, avoidnum);
        }

        public bool Percent(int min, int max)   //치명타, 회피 계산 메서드
        {
            if (min < max)
            {
                return true;
            }
            else
            {
                return false;
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

        public void LvUp()  //레벨업 할때 계산되는 함수
        {
            _Lv++;
            _Atk += 1;
            _Def += 2;
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
            _Hp = 100;
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
            _Hp = 80;
        }
    }

    public class Mage : Player
    {
        public Mage(string name) : base(name)   //유저 이름
        {
            _Lv = 1;
            _Job = "Mage";
            _Atk = 10;
            _Def = 5;
            _Hp = 70;
        }
    }
}

