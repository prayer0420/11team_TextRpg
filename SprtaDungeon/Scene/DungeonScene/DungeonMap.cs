using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonMap
    {
        public const int                            FLOOR = 5;
        private const int                           MIN_ROOM_AMOUNT = 2;
        private const int                           MAX_ROOM_AMOUNT = 5;

        private int                                 seed;

        private DungeonRoom[][]                     rooms;
        private List<(int domain, int dest)>[]      edges;

        private Random                              random;
        private (int floor, int roomNum)?           currentRoom;
        private List<(int floor, int roomNum)>      clearedRooms;

        public DungeonRoom[][]                      DungeonRooms { get { return rooms; } }
        public List<(int domain, int dest)>[]       DungeonEdges { get { return edges; } }
        public (int floor, int roomNum)?            CurrentRoom { get { return currentRoom; } }
        public List<(int floor, int roomNum)>       ClearedRooms { get { return clearedRooms; } }



        public DungeonMap(int seed)
        {
            this.seed = seed;
            random = new Random(this.seed);

            CreateRoom();
            CreateEdges();
        }

        // Create Room
        private void CreateRoom()
        {
            rooms = new DungeonRoom[FLOOR][];

            for(int i = 0; i < FLOOR; i++)
            {
                rooms[i] = new DungeonRoom[random.Next(MIN_ROOM_AMOUNT, MAX_ROOM_AMOUNT + 1)];

                for(int j = 0; j < rooms[i].Length; j++)
                {
                    rooms[i][j] = new DungeonRoom(seed);
                }
            }
        }

        // Create Edges
        private void CreateEdges()
        {
            edges = new List<(int, int)>[FLOOR - 1];
            int rand;


            for (int i = 0; i < edges.Length; i++)
            {
                edges[i] = new List<(int, int)>();
                int domain = rooms[i].Length;
                int destination = rooms[i + 1].Length;
                int currentDestination = 0;

                int totalEdgeCount;
                int[] edgeCounts;
                bool[] isStartingFromLastNode;

                // If Edge of Domain Floor Links Last Range Floor
                isStartingFromLastNode = StartingNode(domain, destination, out totalEdgeCount);

                // Setting Amount of Edge Departing from Domain Floor
                edgeCounts = EdgeDivde(domain, totalEdgeCount);

                for (int j = 0; j < domain; j ++)
                {
                    // If Starting From Last Node, Set CurrentRange to Last Node(Room)
                    if (isStartingFromLastNode[j]) { currentDestination--; }

                    // Pushing Edges To List
                    for (int k = 0; k < edgeCounts[j]; k ++)
                    {
                        edges[i].Add((j, currentDestination));
                        currentDestination++;
                    }
                }
            }

            bool[] StartingNode(int domainCount, int destinationCount, out int EdgeCount)
            {
                bool[] isStartingFromLastNode = new bool[domainCount];
                isStartingFromLastNode[0] = false;

                // isStartingFromLastNode must have at least 'minNodeSet' trues.
                int minNodeSet = domainCount - destinationCount;
                int nodeSetCount;

                do
                {
                    nodeSetCount = 0;

                    for (int i = 1; i < isStartingFromLastNode.Length; i++)
                    {
                        rand = random.Next(0, 2);

                        if (isStartingFromLastNode[i] == false) isStartingFromLastNode[i] = (rand == 0);

                        if (isStartingFromLastNode[i]) nodeSetCount++;
                    }

                } while (nodeSetCount < minNodeSet);

                // return EdgeCount too.
                EdgeCount = destinationCount + nodeSetCount;

                return isStartingFromLastNode;
            }

            int[] EdgeDivde(int domainNodes, int edgeCount)
            {
                int[] edges = new int[domainNodes];
                int count = edgeCount - domainNodes;

                for(int i = 0; i < domainNodes; i++)
                {
                    edges[i] = 1;
                }

                while(count > 0)
                {
                    List<int> currentStep = new List<int>();

                    int randMax = count <= domainNodes ? count : domainNodes;
                    int currentStepEdgeCount = random.Next(1, randMax + 1);
                    count -= currentStepEdgeCount;


                    while (currentStepEdgeCount > 0)
                    {
                        rand = random.Next(0, domainNodes);
                        if(currentStep.Contains(rand)) continue;

                        currentStepEdgeCount--;
                        currentStep.Add(rand);
                    }

                    for (int i = 0; i < currentStep.Count; i++)
                    {
                        if (edges[currentStep[i]] == 3)
                        {
                            count++;
                            continue;
                        }

                        edges[currentStep[i]]++;
                    }
                }

                return edges;
            }
        }

        public List<int> GetNextRooms()
        {
            List<int> nextRooms = new List<int>();

            if (currentRoom == null)
            {
                for (int i = 0; i < rooms[0].Length; i++)
                {
                    nextRooms.Add(i);
                }
                return nextRooms;
            }

            for (int i = 0; i < edges[currentRoom.Value.floor].Count; i++)
            {
                if (edges[currentRoom.Value.floor][i].domain == currentRoom.Value.roomNum) nextRooms.Add(i);
            }

            return nextRooms;
        }

        public RoomResult EnterRoom(int floor, int roomNum)
        {
            currentRoom = (floor, roomNum);

            RoomResult roomResult = (RoomResult)rooms[currentRoom.Value.floor][currentRoom.Value.roomNum].EnterRoom();
            if (roomResult == RoomResult.WIN) clearedRooms.Add(currentRoom.Value);

            return roomResult;
        }

        public void Debug()
        {
            CreateRoom();
            CreateEdges();

            for (int i = 0; i < rooms.Length; i++)
            {
                Console.WriteLine((i + 1) + "번째 층의 방 개수 : " + rooms[i].Length);
            }

            for (int i = 0; i < edges.Length; i++)
            {
                Console.WriteLine((i + 1) + "번째 층과 다음 층을 잇는 엣지");

                for (int j = 0; j < edges[i].Count; j++)
                {
                    Console.WriteLine("(" + edges[i][j].domain + ", " + edges[i][j].dest + ")");
                }
            }
        }

        public enum RoomResult
        {
            ERROR = -1,
            WIN,
            LOSE
        }
    }
}
