using System;
using System.Collections.Generic;

namespace SprtaDungeon
{
    public class Inventory
    {
        private static Inventory _instance;
        public static Inventory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Inventory();
            }
            return _instance;
        }

        public List<Item> Items { get; private set; }
        public List<Item> EquippedItems { get; private set; }
        public List<Potion> potions { get; set; }

        public Inventory()
        {
            Items = new List<Item>();
            EquippedItems = new List<Item>();
            potions = new List<Potion>();
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void DisplayInventory(bool showIndex = true)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Item item = Items[i];
                string displayIndex = showIndex ? $"{i + 1}. " : "";
                string equippedText = IsEquipped(item) ? "[E] " : "";
                Console.WriteLine($"{displayIndex}{equippedText}{item.ItemInfoText()}");
            }
        }

        public void EquipItem(Item item, Player player) // player 매개변수 추가
        {
            if (IsEquipped(item))
            {
                // 장비 해제
                EquippedItems.Remove(item);
                if (item.Type == ItemType.Weapon)
                {
                    player.ExtraAtkDef(true, false,item.Value); // 공격력 감소
                }
                else if (item.Type == ItemType.Armor)
                {
                    player.ExtraAtkDef(false,false,item.Value); // 방어력 감소
                }
                Console.WriteLine($"{item.Name}을(를) 장비 해제하였습니다.");
            }
            else
            {
                // 장비 장착
                EquippedItems.Add(item);
                if (item.Type == ItemType.Weapon)
                {
                    player.ExtraAtkDef(true, true,item.Value); // 공격력 증가
                }
                else if (item.Type == ItemType.Armor)
                {
                    player.ExtraAtkDef(false, true,item.Value); // 방어력 증가
                }
                Console.WriteLine($"{item.Name}을(를) 장비하였습니다.");
            }
        }

        public bool IsEquipped(Item item)
        {
            return EquippedItems.Contains(item);
        }
    }
}
