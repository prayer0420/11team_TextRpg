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

        // 게임 시작, 시작하면 캐릭터 생성 신으로 넘어감. 캐릭터 생성 신이 끝나면 메인 신으로 진입
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
