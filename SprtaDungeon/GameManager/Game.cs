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

        private Game()
        {
            sceneManager = SceneManager.Instance;
        }

        private SceneManager sceneManager;

        public void GameStart()
        {
            int input = sceneManager.GetCreateScene().Start();

            while (true)
            {
                input = sceneManager.GetScene((SceneManager.SceneType)input).Start();

                if (input == -1) Environment.Exit(0);
            }
        }
    }
}
