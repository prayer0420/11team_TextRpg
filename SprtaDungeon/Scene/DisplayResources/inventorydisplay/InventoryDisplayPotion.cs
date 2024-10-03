using System;
using System.Collections.Generic;
using System.Threading;

namespace SprtaDungeon
{
    public class InventoryDisplayPotion : Display
    {
        public Point DisplayPoint { get; set; }
        public string Input { get; set; }

        private bool isUsing;
        private bool isShop;
        private List<Potion> potions;

        public InventoryDisplayPotion(bool isUsing, bool isShop, List<Potion> potions)
        {
            DisplayPoint = new Point(0, 0);
            this.isUsing = isUsing;
            this.isShop = isShop;
            this.potions = potions;
        }

        void Display.Display()
        {
            for (int i = 0; i < potions.Count; i++)
            {
                if (isUsing || isShop) Console.Write((i + 1) + ". ");

                Potion potion = potions[i];
                string recoveryType = potion.PotionType == PotionType.Health ? "체력" : "마나";
                Console.WriteLine($"{potion.Name} : {potion.RecoveryAmount} {recoveryType} 회복");
                Console.WriteLine("개수 : " + potion.PotionCount);

                if (isShop) Console.WriteLine("가격 : " + potion.Price);
            }

            if (isUsing) Console.WriteLine("0. 돌아가기\n\n사용할 포션 번호를 입력해주세요.");
        }

        int Display.Select()
        {
            while (true)
            {
                Console.Write(">> ");
                Input = Console.ReadLine();

                if (int.TryParse(Input, out int result) && result > -1 && result < potions.Count + 1) return result;

                Console.Write("다시 입력해 주십시오...");
                Thread.Sleep(500);

                DisplayPoint.Set(0, Console.CursorTop - 1);
                Console.Write("                                                                                                                \n                       ");
                DisplayPoint.Set(0, Console.CursorTop - 1);
            }
        }
    }
}
