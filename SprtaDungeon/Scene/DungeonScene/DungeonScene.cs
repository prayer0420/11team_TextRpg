using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonScene : Scene
    {
        private DungeonMap          map;
        private int                 currentFloor;
        Display                     display;
        Random                      random;

        public DungeonScene()
        {
            random = new Random();
        }

        int Scene.Start()
        {
            var room = new DungeonRoom(random.Next());
            room.EnterRoom();


            //map = new DungeonMap(random.Next());
            //display = new DungeonDisplayMap(map);
            //display.Display();
            //Console.ReadLine();

            return 0;
            //map = new DungeonMap(random.Next());
            //return DungeonStart();
        }

        private void DungeonMain()
        {

        }

        private int DungeonStart()
        {
            int result;
            display = new DungeonDisplayMap(map);

            do
            {
                result = DungeonLoop();
            }while (result == 1);

            if (result == 0)
            {
                // 던전 클리어 보상
            }

            return 0;
        }

        private int DungeonLoop()
        {
            int roomNum;

            display.Display();
            roomNum = display.Select();

            DungeonMap.RoomResult result = map.EnterRoom(currentFloor, roomNum);

            switch(result)
            {
                case DungeonMap.RoomResult.WIN:

                    currentFloor++;
                    return 1;

                case DungeonMap.RoomResult.LOSE:
                    return -1;
            }

            if (currentFloor > DungeonMap.FLOOR) return 0;

            throw new Exception("Dungeon Logic Error");
        }
    }
}
