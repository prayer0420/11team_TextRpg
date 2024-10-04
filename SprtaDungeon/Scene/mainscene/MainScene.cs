using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class MainScene : Scene
    {
        private int             sceneSelect;
        private Display         display;

        // 메인 신 시작
        int Scene.Start()
        {
            display = new MainDisplay();

            display.Display();
            sceneSelect = display.Select();

            return sceneSelect;
        }
    }
}
