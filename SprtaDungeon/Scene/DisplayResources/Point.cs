using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class Point
    {
        public int _x;
        public int _y;

        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void Set()
        {
            Console.SetCursorPosition(_x, _y);
        }
    }
}
