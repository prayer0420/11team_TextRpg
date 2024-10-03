using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayMap : Display
    {
        private const int       ROOM_SPACE      = 14;
        private const int       ROOM_WIDTH      = 5;
        private const int       ROOM_HEIGHT     = 3;
        private const int       ROOM_MIDDLE     = 1;
        private const int       EDGE_WIDTH      = 9;

        private const string EDGE_MIDDLE_STRAIGHT       = " ━━━━━━━ ";
        private const string EDGE_MIDDLE_UP             = " ━━━┻━━━ ";
        private const string EDGE_MIDDLE_CENTER         = " ━━━╋━━━ ";
        private const string EDGE_MIDDLE_DOWN           = " ━━━┳━━━ ";
        private const string EDGE_MIDDLE_DEST           = " ┣━━━ ";
        private const string EDGE_TOP_DEST              = "    ┏━━━ ";
        private const string EDGE_TOP_START             = " ━━━┓    ";
        private const string EDGE_BOTTOM_DEST           = "    ┗━━━ ";
        private const string EDGE_BOTTOM_START          = " ━━━┛    ";
        private const string EDGE_PASS                  = "    ┃    ";

        public Point            DisplayPoint { get; set; }

        public string           Input { get; set; }
        private DungeonMap      DungeonMap { get; set; }


        public DungeonDisplayMap(DungeonMap dungeonMap)
        {
            this.DungeonMap = dungeonMap;
            DisplayPoint = new Point(0, 0);
        }

        void Display.Display()
        {
            Console.Clear();
            Console.CursorVisible = false;
            DisplayRooms();
            DisplayPassedEdges();
        }

        int Display.Select()
        {
            throw new NotImplementedException();
        }

        private void DisplayRooms()
        {
            var current = DungeonMap.CurrentRoom;
            var clearedRooms = DungeonMap.ClearedRooms;
            int currentFloor = current == null ? 0 : current.Value.floor;
            currentFloor = 1;

            for (int i = 0; i <= currentFloor + 1; i++)
            {
                for(int j = 0; j < DungeonMap.DungeonRooms[i].Length; j++)
                {
                    if (i >= DungeonMap.FLOOR) return;

                    Console.ForegroundColor = Color(i, j);

                    DisplayPoint.Set(ROOM_SPACE * i, ROOM_HEIGHT * j);

                    if (i == currentFloor) DrawRoom((j + 1).ToString(), ROOM_SPACE * i);
                    else DrawRoom(" ", ROOM_SPACE * i);

                    Console.ResetColor();
                }
            }

            void DrawRoom(string num, int coordX)
            {
                string[] room = new string[]{"┏━━━┓",
                                             "┃ " + num + " ┃",
                                             "┗━━━┛"};

                for(int i = 0; i < room.Length; i ++)
                {
                    Console.Write(room[i]);
                    DisplayPoint.Set(coordX, ++Console.CursorTop);
                }
            }

            ConsoleColor Color(int floor, int roomNum)
            {
                if(floor == currentFloor)
                {
                    var nextRooms = DungeonMap.GetNextRooms();
                    if (nextRooms.Contains(roomNum)) return ConsoleColor.White;
                    else return ConsoleColor.DarkBlue;
                }
                else if (floor == currentFloor + 1) return ConsoleColor.Cyan;

                foreach (var room in clearedRooms)
                {
                    if(room == (floor, roomNum)) return ConsoleColor.Yellow;
                }
                return ConsoleColor.DarkBlue;
            }
        }

        private void DisplayPassedEdges()
        {
            var clearedRooms = DungeonMap.ClearedRooms;

            for (int i = 0; i < clearedRooms.Count - 1; i++)
            {
                int domain = clearedRooms[i].roomNum;
                int dest = clearedRooms[i + 1].roomNum;

                int domainCoord = ROOM_MIDDLE + (domain * ROOM_HEIGHT);
                int destCoord = ROOM_MIDDLE + (dest * ROOM_HEIGHT);
                int coordDistance = destCoord - domainCoord;
                DisplayPoint._x = ROOM_WIDTH + (i * ROOM_SPACE);

                if (coordDistance == 0)
                {
                    DisplayPoint._y = ROOM_MIDDLE + (domain * ROOM_HEIGHT);
                    DisplayPoint.Set();
                    Console.Write(EDGE_MIDDLE_STRAIGHT);
                }
                else
                {
                    int startCoord, endCoord;
                    string startString, endString;

                    if (coordDistance <= 0)
                    {
                        startCoord = destCoord;
                        endCoord = domainCoord;

                        startString = EDGE_TOP_DEST;
                        endString = EDGE_BOTTOM_START;
                    }
                    else
                    {
                        startCoord = domainCoord;
                        endCoord = destCoord;

                        startString = EDGE_TOP_START;
                        endString = EDGE_BOTTOM_DEST;
                    }

                    DisplayPoint._y = startCoord;
                    DisplayPoint.Set();
                    Console.Write(startString);

                    DisplayPoint._y = endCoord;
                    DisplayPoint.Set();
                    Console.Write(endString);

                    for (int j = startCoord + 1; j < endCoord; j++)
                    {
                        DisplayPoint._y = j;
                        DisplayPoint.Set();
                        Console.Write(EDGE_PASS);
                    }
                }
            }
        }

        private void DisplayCurrentEdge(int domainFloor, int roomNum)
        {
            var nextEdges = DungeonMap.DungeonEdges[domainFloor];
        }
    }
}