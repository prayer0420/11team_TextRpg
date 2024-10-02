using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    class Inventory
    {

        private List<Item> inventory = new List<Item>();
        private List<Item> EquipList = new List<Item>();

        Player player = (Player)GameManager.Instance.Player;

        public int InventoryCount
        {
            get
            {
                return inventory.Count;
            }
        }

        public void DisplayInventory(bool showIdx)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                Item targetItem = inventory[i];

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
                {
                    /*ExtraAtk*/
                    player.ExtraAtkDef(item.Value);
                }
                else
                    /*ExtraDef*/
                    player.ExtraAtkDef(item.Value);

            }
            else
            {
                EquipList.Add(item);
                if (item.Type == 0)
                    /*ExtraAtk*/
                    player.ExtraAtkDef(item.Value);

                else
                    /*ExtraDef*/
                    player.ExtraAtkDef(item.Value);
            }
        }

        public bool IsEquipped(Item item)
        {
            return EquipList.Contains(item);
        }

        public void BuyItem(Item item)
        {
            GameManager.Instance.Player._Gold -= item.Price;
            inventory.Add(item);
        }

        public bool HasItem(Item item)
        {
            return inventory.Contains(item);
        }
    }
}
