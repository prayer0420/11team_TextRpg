﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    class Item
    {
        public string Name { get; }
        public int Type { get; }
        public int Value { get; }
        public string Desc { get; }
        public int Price { get; }

        public string DisplayTypeText
        {
            get
            {
                return Type == 0 ? "공격력" : "방어력";
            }
        }
              public string ItemInfoText()
        {
            return $"{Name}  |  {DisplayTypeText} +{Value}  |  {Desc}";
        }
    }
}