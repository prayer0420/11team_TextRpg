using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class SaveScene : Scene
    {
        private Display display;

        int Scene.Start()
        {
            display = new SaveDisplay();

            Save();

            display.Display();
            display.Select();
            return 0;
        }

        private void Save()
        {
            Player player = GameManager.Instance.Player as Player;

            QuestInvenSave save = new QuestInvenSave()
            {
                items = Inventory.GetInstance().Items,
                EquippedItems = Inventory.GetInstance().EquippedItems,
                potions = Inventory.GetInstance().potions,
                availableQuests = QuestManager.GetInstance().availableQuests,
                activeQuests = QuestManager.GetInstance().activeQuests
            };

            string playerJson = JsonConvert.SerializeObject(player);
            string otherJson = JsonConvert.SerializeObject(save);

            File.WriteAllText(@"player.json", playerJson);
            File.WriteAllText(@"other.json", otherJson);
        }
    }

    public struct QuestInvenSave
    {
        public List<Item> items;
        public List<Item> EquippedItems;
        public List<Potion> potions;
        public List<Quest> availableQuests;
        public List<Quest> activeQuests;
    }
}
