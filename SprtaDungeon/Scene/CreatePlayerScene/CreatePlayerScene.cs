using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class CreatePlayerScene : Scene
    {
        private string                  playerName;

        private Display                 display;
        private GameManager             gameManager;

        public CreatePlayerScene()
        {
            gameManager = GameManager.Instance;
        }

        public int Start()
        {
            display = new CreatePlayerDisplayName();
            display.Display();
            display.Select();

            playerName = display.Input;

            display = new CreatePlayerDisplay(playerName);
            display.Display();
            int job = display.Select();

            // Create Player by Inputs
            Creature player = Creature.CreatureFactory((Creature.CreatureType)job, playerName);

            // Init GameManager by Player
            gameManager.Init(player);

            // Go to Main Menu
            return 0;
        }
    }
}
