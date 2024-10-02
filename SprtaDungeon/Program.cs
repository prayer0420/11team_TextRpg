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
            Game game = Game.Instance;

            game.GameStart();
    
        }
    }
}
