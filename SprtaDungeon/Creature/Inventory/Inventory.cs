using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    internal class Inventory
    {

        private List<Item> Inventory = new List<Item>();
        private List<Item> EquipList = new List<Item>();
        public int InventoryCount
        {
            get
            {
                return Inventory.Count;
            }
        }

        public void DisplayInventory(bool showIdx)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                Item targetItem = Inventory[i];

                string displayIdx = showIdx ? $"{i + 1} " : "";
                string displayEquipped = IsEquipped(targetItem) ? "[E]" : "";
                Console.WriteLine($"- {displayIdx}{displayEquipped} {targetItem.ItemInfoText()}");
            }
        }

        public void EquipItem(Item item)
        {
            if (IsEquipped(item))
            {
                EquipList.Remove(item);
                if (item.Type == 0)
                    /*ExtraAtk*/ -= item.Value;
                else
                    /*ExtraDef*/ -= item.Value;
            }
            else
            {
                EquipList.Add(item);
                if (item.Type == 0)
                    /*ExtraAtk*/ += item.Value;
                else
                    /*ExtraDef*/ += item.Value;
            }
        }

        public bool IsEquipped(Item item)
        {
            return EquipList.Contains(item);
        }

        public void BuyItem(Item item)
        {
            /*Gold*/ -= item.Price;
            Inventory.Add(item);
        }

        public bool HasItem(Item item)
        {
            return Inventory.Contains(item);
        }
    }
}
