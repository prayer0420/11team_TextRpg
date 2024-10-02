using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class Potion
    {
        public string PotionName { get; }
        public int PotionValue { get; }
        public int PotionCount { get; }

        // 상점에서 구매 가능?
        public int Price { get; }

        public Potion(int potionValue, int potionCount)
        {
            PotionValue = potionValue;
            PotionCount = potionCount;
        }
    }
}
