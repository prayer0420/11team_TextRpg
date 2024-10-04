using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class InventoryScene : Scene
    {
        private Display display;

        public InventoryScene()
        {
        }

        int Scene.Start()
        {
            display = new InventoryDisplayItem();
            display.Display();
            int item = display.Select();

            if (item == 0) return 0;

            Inventory.GetInstance().EquipItem(Inventory.GetInstance().Items[item - 1] ?? null, GameManager.Instance.Player as Player);

            return 0;
        }
    }
}
