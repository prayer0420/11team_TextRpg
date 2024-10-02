using System;
using System.Collections.Generic;

namespace SprtaDungeon
{
    public class ItemDataBase
    {
        private static ItemDataBase _instance;
        private Random random = new Random();

        public static ItemDataBase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ItemDataBase();
            }
            return _instance;
        }

        private List<Tuple<string, string>> weaponList = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("용맹의 대검", "전사의 용맹함을 상징하는 강력한 대검, 적을 단번에 베어낼 수 있는 힘을 가진다."),
            new Tuple<string, string>("격노의 전투 도끼", "전투의 격노를 담아내어 강력한 공격력을 발휘하는 양손 도끼."),
            new Tuple<string, string>("전설의 창", "전사의 기백을 담아 먼 거리의 적도 제압할 수 있는 강력한 창."),
            new Tuple<string, string>("불멸의 대검", "전설 속에서 불사의 힘을 얻은 대검, 전사의 힘을 극대화한다."),
            new Tuple<string, string>("폭풍의 망치", "폭풍의 힘을 담아 적을 강타하는 전사의 망치.")
        };

        private List<Tuple<string, string>> armorList = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("철갑 판금 갑옷", "전투에서 최고의 방어력을 제공하는 튼튼한 판금 갑옷."),
            new Tuple<string, string>("용가죽 갑옷", "전설적인 용의 가죽으로 만들어져 마법 저항력을 강화하는 갑옷."),
            new Tuple<string, string>("불사의 갑옷", "전사의 생명력을 극대화시키는 고대의 마법이 깃든 갑옷."),
            new Tuple<string, string>("황금 판금 갑옷", "전투에서 전사의 위엄을 드러내는 황금으로 만든 판금 갑옷."),
            new Tuple<string, string>("암흑의 갑옷", "어둠의 힘을 이용해 전사의 방어력을 극대화하는 갑옷.")
        };

        public Tuple<string, string> GetRandomWeapon()
        {
            int index = random.Next(weaponList.Count);
            return weaponList[index];
        }

        public Tuple<string, string> GetRandomArmor()
        {
            int index = random.Next(armorList.Count);
            return armorList[index];
        }
    }
}
