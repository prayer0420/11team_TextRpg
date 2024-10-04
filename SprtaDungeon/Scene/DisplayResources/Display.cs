using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public interface Display
    {
        string Input { get; set; }
        Point DisplayPoint { get; set; }

        void Display();
        int Select();
    }
}
