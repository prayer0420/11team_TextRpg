using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class GameoverScene : Scene
    {
        int Scene.Start()
        {
            Environment.Exit(0);
            return 0;
        }
    }
}
