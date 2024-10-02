using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class SceneManager
    {
        private static SceneManager _instance;

        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneManager();
                }
                return _instance;
            }
            private set { }
        }

        private Scene[] scenes;

        private SceneManager()
        {
            scenes = new Scene[Enum.GetValues(typeof(SceneType)).Length];

            for(int i = 0; i < scenes.Length; i++)
            {
                switch((SceneType)i)
                {
                    case SceneType.MAIN_SCENE:
                        scenes[i] = new MainScene(); break;

                    case SceneType.STATUS_SCENE:
                        scenes[i] = new StatusScene(); break;

                    case SceneType.INVENTORY_SCENE:
                        scenes[i] = new InventoryScene(); break;

                    case SceneType.SHOP_SCENE:
                        scenes[i] = new ShopScene(); break;

                    case SceneType.DUNGEON_SCENE:
                        scenes[i] = new DungeonScene(); break;

                    case SceneType.QUEST_SCENE:
                        scenes[i] = new QuestScene(); break;
                }
            }
        }

        public Scene GetCreateScene()
        {
            return new CreatePlayerScene();
        }

        public Scene GetScene(SceneType type)
        {
            return scenes[(int)type];
        }

        public enum SceneType
        {
            MAIN_SCENE = 0,
            STATUS_SCENE,
            INVENTORY_SCENE,
            SHOP_SCENE,
            DUNGEON_SCENE,
            QUEST_SCENE
        }
    }
}