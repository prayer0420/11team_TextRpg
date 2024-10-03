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

        private const string    EDGE_MIDDLE_STRAIGHT       = " ━━━━━━━ ";
        private const string    EDGE_MIDDLE_UP             = " ━━━┻━━━ ";
        private const string    EDGE_MIDDLE_CENTER         = " ━━━╋━━━ ";
        private const string    EDGE_MIDDLE_DOWN           = " ━━━┳━━━ ";
        private const string    EDGE_MIDDLE_DEST           = "    ┣━━━ ";
        private const string    EDGE_TOP_DEST              = "    ┏━━━ ";
        private const string    EDGE_TOP_START             = " ━━━┓    ";
        private const string    EDGE_BOTTOM_DEST           = "    ┗━━━ ";
        private const string    EDGE_BOTTOM_START          = " ━━━┛    ";
        private const string    EDGE_PASS                  = "    ┃    ";

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
            DisplayRooms();
            DisplayPassedEdges();
        }

        int Display.Select()
        {
            int currentFloor = DungeonMap.CurrentRoom == null ? 0 : DungeonMap.CurrentRoom.Value.floor + 1;

            int roomSelect;
            int roomConfirm;
            while (true)
            {
                DisplayPoint.Set(0, ROOM_HEIGHT * DungeonMap.FLOOR + 2);
                Console.WriteLine("들어갈 방을 입력해주십시오.");

                while (true)
                {
                    Console.Write(">> ");
                    Input = Console.ReadLine();

                    if (int.TryParse(Input, out roomSelect) && DungeonMap.GetNextRooms().Contains(roomSelect - 1)) break;

                    Console.Write("다시 입력해 주십시오...");
                    Thread.Sleep(500);

                    DisplayPoint.Set(0, Console.CursorTop - 1);
                    Console.Write("                                                                                                                \n                                                                     ");
                    DisplayPoint.Set(0, Console.CursorTop - 1);
                }

                if (roomSelect == 0) return 0;

                DisplayPoint.Set(0, Console.CursorTop - 2);
                Console.Write("                           \n");
                Console.Write("                                                                                                                                                                                     ");
                DisplayPoint.Set(0, Console.CursorTop - 2);

                var pointSave = (0, Console.CursorTop);

                DisplayCurrentEdge(currentFloor, roomSelect - 1);

                DisplayPoint.Set(pointSave.Item1, pointSave.CursorTop);
                var monsters = DungeonMap.DungeonRooms[currentFloor][roomSelect - 1].Monsters;
                Console.Write(roomSelect + "번 방에 있는 몬스터 : ");
                for(int i = 0; i < monsters.Length; i ++)
                {
                    Console.Write(monsters[i]._Name);
                    if(i != monsters.Length - 1) Console.Write(", ");
                }
                Console.WriteLine("\n\n1. 방에 들어간다.\n0. 돌아간다\n\n행동을 선택해 주십시오.");


                while (true)
                {
                    Console.Write(">> ");
                    Input = Console.ReadLine();

                    if (int.TryParse(Input, out roomConfirm) && (roomConfirm == 0 || roomConfirm == 1)) break;

                    Console.Write("다시 입력해 주십시오...");
                    Thread.Sleep(500);

                    DisplayPoint.Set(0, Console.CursorTop - 1);
                    Console.Write("                                                                                                                \n                       ");
                    DisplayPoint.Set(0, Console.CursorTop - 1);
                }

                if (roomConfirm == 1) return roomSelect - 1;

                DisplayPoint.Set(0, Console.CursorTop - 7);
                Console.Write("                                                                                                                   \n\n");
                Console.Write("                       \n                       \n\n");
                Console.Write("                       \n");
                Console.Write("                                                                                                                                                          ");
                DisplayPoint.Set(0, Console.CursorTop - 6);
            }
        }

        private void DisplayRooms()
        {
            int currentFloor = DungeonMap.CurrentRoom == null ? 0 : DungeonMap.CurrentRoom.Value.floor + 1;
            var clearedRooms = DungeonMap.ClearedRooms;

            for (int i = 0; i <= currentFloor + 1; i++)
            {
                if (i >= DungeonMap.FLOOR) return;

                for(int j = 0; j < DungeonMap.DungeonRooms[i].Length; j++)
                {
                    string enterable = (j + 1).ToString();
                    Console.ForegroundColor = Color(i, j, ref enterable);

                    DisplayPoint.Set(ROOM_SPACE * i, ROOM_HEIGHT * j);

                    if (i == currentFloor) DrawRoom(enterable, ROOM_SPACE * i);
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

            ConsoleColor Color(int floor, int roomNum, ref string enterableRoom)
            {
                if(floor == currentFloor)
                {
                    var nextRooms = DungeonMap.GetNextRooms();
                    if (nextRooms.Contains(roomNum)) return ConsoleColor.White;
                    else
                    {
                        enterableRoom = " ";
                        return ConsoleColor.DarkBlue;
                    }
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
            if (domainFloor >= DungeonMap.FLOOR - 1) return;

            DisplayPoint._x = ROOM_WIDTH + (domainFloor * ROOM_SPACE);
            for (int i = 0; i <= ROOM_HEIGHT * DungeonMap.FLOOR; i++)
            {
                DisplayPoint._y = i;
                DisplayPoint.Set();
                Console.Write("         ");
            }

            var nextEdges = DungeonMap.DungeonEdges[domainFloor];
            List<int> dests = new List<int>();
            List<(int room, string edge)> edges = new List<(int, string)>();

            for (int i = 0; i < nextEdges.Count; i++)
            {
                var edge = nextEdges[i];
                if (edge.domain == roomNum) dests.Add(edge.dest);
            }
            dests.Sort();

            bool up = false, down = false;

            for (int i = 0; i < dests.Count;i++)
            {


                int room = dests[i];

                if (room < roomNum)
                {
                    up = true;
                    if (i == 0) edges.Add((room, EDGE_TOP_DEST));
                    else edges.Add((room, EDGE_MIDDLE_DEST));
                }
                else if (room > roomNum)
                {
                    down = true;
                    if (i == dests.Count - 1) edges.Add((room, EDGE_BOTTOM_DEST));
                    else edges.Add((room, EDGE_MIDDLE_DEST));
                }
            }



            int? domainIndex = null;

            if (dests.Contains(roomNum))
            {
                domainIndex = dests.IndexOf(roomNum);
                dests.Remove(roomNum);
            }

            if (up && down) edges.Insert(domainIndex.Value, (roomNum, EDGE_MIDDLE_CENTER));
            else if (!up && down)
            {
                if(domainIndex != null) edges.Insert(0, (roomNum, EDGE_MIDDLE_DOWN));
                else edges.Insert(0, (roomNum, EDGE_TOP_START));
            }
            else if (up && !down)
            {
                if (domainIndex != null) edges.Add((roomNum, EDGE_MIDDLE_UP));
                else edges.Add((roomNum, EDGE_BOTTOM_START));
            }
            else edges.Add((roomNum, EDGE_MIDDLE_STRAIGHT));





            DisplayPoint._x = ROOM_WIDTH + (domainFloor * ROOM_SPACE);

            foreach (var edge in edges)
            {
                DisplayPoint._y = ROOM_MIDDLE + (edge.room * ROOM_HEIGHT);
                DisplayPoint.Set();
                Console.Write(edge.edge);
            }

            for(int i = 0; i < edges.Count - 1; i++)
            {
                int topCoord = ROOM_MIDDLE + (edges[i].room * ROOM_HEIGHT);
                int bottomCoord = ROOM_MIDDLE + (edges[i + 1].room * ROOM_HEIGHT);

                for(int j = topCoord + 1; j < bottomCoord; j++)
                {
                    DisplayPoint._y = j;
                    DisplayPoint.Set();
                    Console.Write(EDGE_PASS);
                }
            }
        }
    }
}