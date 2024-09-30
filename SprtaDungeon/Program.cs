using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 1; i ++)
            {
                DungeonMap map = new DungeonMap(i);

                map.Debug();
            }

            Console.WriteLine("Complete");
    
        }
    }
}
