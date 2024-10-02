using System;

namespace SprtaDungeon
{

    public enum ItemType
    {
        Weapon,
        Armor,
        Potion 
    }

    public class Item
    {
        private static int _nextId = 0;
        private static Random random = new Random();

        public int ItemId { get; private set; }
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int Value { get; set; } 
        public string Desc { get; set; }
        public int Price { get; set; }

        public Item(ItemType type)
        {
            ItemId = _nextId++;
            Type = type;
            GenerateItem();
        }

        private void GenerateItem()
        {
            ItemDataBase db = ItemDataBase.GetInstance();
            if (Type == ItemType.Weapon)
            {
                var itemData = db.GetRandomWeapon();
                Name = itemData.Item1;
                Desc = itemData.Item2;
                Value = random.Next(10, 30);
                Price = random.Next(1000, 3000);
            }
            else if (Type == ItemType.Armor)
            {
                var itemData = db.GetRandomArmor();
                Name = itemData.Item1;
                Desc = itemData.Item2;
                Value = random.Next(10, 30);
                Price = random.Next(1000, 3000);
            }
        }

        public string DisplayTypeText
        {
            get
            {
                return Type == ItemType.Weapon ? "공격력" : "방어력";
            }
        }

        public virtual string ItemInfoText()
        {
            return $"{Name} | {DisplayTypeText} +{Value}";
        }

        public void PrintInfo()
        {
            Console.WriteLine($"{Name} | {DisplayTypeText} +{Value} | {Desc} | {Price}G");
        }
    }
}
