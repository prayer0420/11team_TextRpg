using System;
using System.Collections.Generic;

namespace SprtaDungeon
{
    public enum PotionType
    {
        Health, // 체력 회복 물약
        Mana    // 마나 회복 물약
    }

    public class Potion : Item
    {
        public int RecoveryAmount { get; set; } // 회복량
        public PotionType PotionType { get; set; } // 물약 타입 (체력/마나)
        public int PotionCount { get; set; } // 물약의 수량
        private static Random random = new Random();

        // 생성자
        public Potion(PotionType potionType, int count = 1) : base(ItemType.Potion)
        {
            PotionType = potionType;
            PotionCount = count; // 기본 물약 수량은 1로 설정
            RecoveryAmount = 50; // 회복량 랜덤 설정

            if (PotionType == PotionType.Health)
            {
                Name = "회복 물약";
                Desc = $"{RecoveryAmount}의 체력을 회복하는 물약입니다.";
            }
            else if (PotionType == PotionType.Mana)
            {
                Name = "마나 물약";
                Desc = $"{RecoveryAmount}의 마나를 회복하는 물약입니다.";
            }

            Price = random.Next(500, 1000);
        }

        // 아이템 정보 표시
        public override string ItemInfoText()
        {
            string recoveryType = PotionType == PotionType.Health ? "체력 회복" : "마나 회복";
            return $"{Name} | {recoveryType} +{RecoveryAmount} | {Price}G | 수량: {PotionCount}";
        }

        // 물약 사용
        public void Use(Player player)
        {
            if (PotionCount > 0)
            {
                if (PotionType == PotionType.Health)
                {
                    player.Heal(RecoveryAmount); // 플레이어 체력 회복
                    Console.WriteLine($"{Name}을(를) 사용하여 체력을 {RecoveryAmount}만큼 회복하였습니다.");
                }
                else if (PotionType == PotionType.Mana)
                {
                    //player.RecoverMana(RecoveryAmount); // 플레이어 마나 회복
                    Console.WriteLine($"{Name}을(를) 사용하여 마나를 {RecoveryAmount}만큼 회복하였습니다.");
                }

                PotionCount--; // 물약 수량 감소
                if (PotionCount == 0)
                {
                    Console.WriteLine($"{Name}을 모두 사용하였습니다.");
                }
            }
            else
            {
                Console.WriteLine("사용할 수 있는 물약이 없습니다.");
            }
        }
    }
}
