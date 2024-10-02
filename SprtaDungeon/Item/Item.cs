using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    internal class Item
    {
        public string Name { get; }
        public int Type { get; }
        public int Value { get; }
        public string Desc { get; }
        public int Price { get; }

        public string DisplayTypeText
        {
            get
            {
                return Type == 0 ? "공격력" : "방어력";
            }
        }

        public Item(string name, int type, int value, string desc, int price)
        {
            Name = name;
            Type = type;
            Value = value;
            Desc = desc;
            Price = price;
        }

        public string ItemInfoText()
        {
            return $"{Name}  |  {DisplayTypeText} +{Value}  |  {Desc}";
        }

        public static Item[] itemDb;

        static void ItemDataBase()
        {
            itemDb = new Item[]
            {
                new Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),
                new Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),
                new Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),
                new Item("수련자의 갑옷", 1, 5,"수련에 도움을 주는 갑옷입니다. ",1000),

            };
        }
    }
}
