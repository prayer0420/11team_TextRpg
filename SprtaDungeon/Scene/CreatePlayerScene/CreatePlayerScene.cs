using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class CreatePlayerScene : Scene
    {
        private string                  playerName;

        private Display                 display;
        private GameManager             gameManager;

        public CreatePlayerScene()
        {
            gameManager = GameManager.Instance;
        }

        // 캐릭터 생성 신 시작
        public int Start()
        {
            if (Load()) return 0;

            // 캐릭터 이름 설정
            display = new CreatePlayerDisplayName();
            display.Display();
            display.Select();

            playerName = display.Input;

            // 캐릭터 직업 설정
            display = new CreatePlayerDisplay(playerName);
            display.Display();
            int job = display.Select();

            // Create Player by Inputs
            Creature player = Creature.CreatureFactory((Creature.CreatureType)job, playerName);

            // Init GameManager by Player
            gameManager.Init(player);

            // Go to Main Menu
            return 0;
        }

        private bool Load()
        {
            FileInfo info = new FileInfo(@"player.json");
            if (!info.Exists) return false;
            string playerJson = File.ReadAllText(@"player.json");

            info = new FileInfo(@"other.json");
            if (!info.Exists) return false;
            string otherJson = File.ReadAllText(@"other.json");

            gameManager.Init(JsonConvert.DeserializeObject<Player>(playerJson));

            QuestInvenSave save = JsonConvert.DeserializeObject<QuestInvenSave>(otherJson);

            Inventory.GetInstance().Items = save.items;
            Inventory.GetInstance().EquippedItems = save.EquippedItems;
            Inventory.GetInstance().potions = save.potions;
            QuestManager.GetInstance().availableQuests = save.availableQuests;
            QuestManager.GetInstance().activeQuests = save.activeQuests;

            return true;
        }
    }
}
