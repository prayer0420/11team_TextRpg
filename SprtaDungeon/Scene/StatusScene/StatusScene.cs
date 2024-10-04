using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class StatusScene : Scene
    {
        private Display display;
        private Creature player;

        int Scene.Start()
        {
            player = GameManager.Instance.Player;
            display = new CreatureDisplay(player, false, false);

            display.Display();
            display.Select();

            return 0;
        }
    }
}
