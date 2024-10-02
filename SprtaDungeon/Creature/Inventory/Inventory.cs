using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class Inventory
    {
        public List<Item> inventory = new List<Item>();
        public List<Item> equipList = new List<Item>();

        public List<Potion> potions = new List<Potion>();

        Player player = GameManager.Instance.Player as Player;

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
                equipList.Remove(item);
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
                equipList.Add(item);
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
            return equipList.Contains(item);
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
