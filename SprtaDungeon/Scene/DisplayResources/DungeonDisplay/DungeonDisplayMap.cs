using SprtaDungeon.Scene.DisplayResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class DungeonDisplayMap : Display
    {
        public Point        DisplayPoint { get; set; }

        private DungeonMap  DungeonMap { get; set; }
        public int          CurrentFloor { get; set; }

        public DungeonDisplayMap(DungeonMap dungeonMap)
        {
            this.DungeonMap = dungeonMap;
            DisplayPoint = new Point(0, 0);
        }

        void Display.Display()
        {
            throw new NotImplementedException();
        }

        int Display.Select()
        {
            throw new NotImplementedException();
        }
    }
}