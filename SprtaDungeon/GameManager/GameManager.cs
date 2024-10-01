using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class GameManager
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
            private set { }
        }

        public Creature Player { get; private set; }

        private GameManager() { }

        public void Init(Creature player)
        {
            Player = player;
        }
    }
}
