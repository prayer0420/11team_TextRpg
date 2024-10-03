using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonScene : Scene
    {
        private const int           GAIN_GOLD = 3000;
        private DungeonMap          map;
        private Display             mapDisplay;
        private Display             resultDisplay;
        private Random              random;

        public DungeonScene()
        {
            random = new Random();
        }

        int Scene.Start()
        {
            map = new DungeonMap(random.Next());
            return DungeonStart();
        }

        private int DungeonStart()
        {
            int result;
            Player player = GameManager.Instance.Player as Player;
            mapDisplay = new DungeonDisplayMap(map);

            do
            {
                result = DungeonLoop();
            }while (result == 1);

            if (result == 0)
            {
                player._Gold += GAIN_GOLD;
                resultDisplay = new DungeonDisplayClear(GAIN_GOLD);
                resultDisplay.Display();
                resultDisplay.Select();
            }

            player.Heal();
            player.ChargeMp();

            return 0;
        }

        private int DungeonLoop()
        {
            int roomNum;

            mapDisplay.Display();
            roomNum = mapDisplay.Select();

            int currentFloor = map.CurrentRoom == null ? 0 : map.CurrentRoom.Value.floor + 1;
            DungeonMap.RoomResult result = map.EnterRoom(currentFloor, roomNum);

            if (currentFloor >= DungeonMap.FLOOR - 1) return 0;

            if (result == DungeonMap.RoomResult.WIN) 
            {
                resultDisplay = new DungeonDisplayLevelUp();
                resultDisplay.Display();
                resultDisplay.Select();
                return 1;
            }
            else if(result == DungeonMap.RoomResult.LOSE)
            {
                resultDisplay = new DungeonDisplayLose();
                resultDisplay.Display();
                resultDisplay.Select();
                return -1; 
            }

            throw new Exception("Dungeon Logic Error");
        }
    }
}
