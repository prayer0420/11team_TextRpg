using SprtaDungeon.Scene.DisplayResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    internal class Game
    {
        private static Game _instance;

        public static Game Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Game();
                }
                return _instance;
            }
            private set { }
        }

        private Inventory _inventory;

        public void GameStart()
        {

        }

        public void Inventory()
        {

        }
    }
}
