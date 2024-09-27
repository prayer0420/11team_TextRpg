﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon.Scene.DisplayResources
{
    internal interface Display
    {
        Point Point { get; set; }

        void Display();
        int Select();
    }
}
