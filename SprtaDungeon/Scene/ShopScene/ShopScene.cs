using System;
using System.Collections.Generic;
using System.Linq;

namespace SprtaDungeon
{
    public class ShopScene : Scene
    {
        private List<Item> shopItems;
        private Dictionary<int, bool> shopItemSold; // ItemId, IsSold

        public ShopScene()
        {
            shopItems = new List<Item>();
            shopItemSold = new Dictionary<int, bool>();
            ResetShopItems();
        }

        public void ShowShop(Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("[상점]\n");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player._Gold} G\n");

                Console.WriteLine("[아이템 목록]");
                ShowShopItems();

                Console.WriteLine("\n[1] 아이템 구매");
                Console.WriteLine("[2] 아이템 판매");
                Console.WriteLine("[3] 상점 리셋");
                Console.WriteLine("[0] 나가기");

                Console.WriteLine("\n원하시는 행동을 입력해주세요");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        return;
                    case "1":
                        BuyItem(player);
                        break;
                    case "2":
                        SellItem(player);
                        break;
                    case "3":
                        ResetShopItems();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;
                }
            }
        }

        private void ResetShopItems()
        {
            shopItems.Clear();
            shopItemSold.Clear();

            // 무기 추가
            for (int i = 0; i < 3; i++)
            {
                Item weapon = new Item(ItemType.Weapon);
                if (!shopItems.Any(item => item.Name == weapon.Name))
                {
                    shopItems.Add(weapon);
                    shopItemSold.Add(weapon.ItemId, false);
                }
            }

            // 방어구 추가
            for (int i = 0; i < 3; i++)
            {
                Item armor = new Item(ItemType.Armor);
                if (!shopItems.Any(item => item.Name == armor.Name))
                {
                    shopItems.Add(armor);
                    shopItemSold.Add(armor.ItemId, false);
                }
            }

            // 체력 회복 물약 2개 추가
            for (int i = 0; i < 2; i++)
            {
                Potion healthPotion = new Potion(PotionType.Health);
                shopItems.Add(healthPotion);
                shopItemSold.Add(healthPotion.ItemId, false);
            }

            // 마나 회복 물약 2개 추가
            for (int i = 0; i < 2; i++)
            {
                Potion manaPotion = new Potion(PotionType.Mana);
                shopItems.Add(manaPotion);
                shopItemSold.Add(manaPotion.ItemId, false);
            }
        }

        private void ShowShopItems()
        {
            for (int i = 0; i < shopItems.Count; i++)
            {
                Item item = shopItems[i];
                bool isSold = shopItemSold[item.ItemId];
                Console.Write($"{i + 1}. {item.ItemInfoText()} ");
                if (isSold)
                {
                    Console.WriteLine("| 구매 완료");
                }
                else
                {
                    Console.WriteLine($"| 가격: {item.Price}G");
                }
            }
        }

        private void BuyItem(Player player)
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("아이템 구매");

            Console.WriteLine("원하는 아이템의 번호를 입력하면 구매할 수 있습니다\n");

            ShowShopItems();

            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");

            string input = Console.ReadLine();
            if (input == "0")
            {
                return;
            }

            if (int.TryParse(input, out int number))
            {
                number -= 1; // Adjust for 0-based index
                if (number >= 0 && number < shopItems.Count)
                {
                    Item selectedItem = shopItems[number];
                    bool isSold = shopItemSold[selectedItem.ItemId];
                    if (isSold)
                    {
                        Console.WriteLine("이미 구매한 아이템입니다.");
                    }
                    else if (player._Gold >= selectedItem.Price)
                    {
                        player._Gold -= selectedItem.Price;
                        Inventory.GetInstance().AddItem(selectedItem);
                        shopItemSold[selectedItem.ItemId] = true;
                        Console.WriteLine($"{selectedItem.Name}을(를) 구매하셨습니다!");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족합니다.");
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 번호입니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
            Console.ReadLine();
        }

        private void SellItem(Player player)
        {
            Console.Clear();
            Console.WriteLine("[상점]");
            Console.WriteLine("아이템 판매");
            Console.WriteLine("팔기 원하는 아이템의 번호를 입력하면 판매할 수 있습니다\n");

            Inventory inventory = Inventory.GetInstance();
            if (inventory.Items.Count == 0)
            {
                Console.WriteLine("판매할 아이템이 없습니다.");
                Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
                Console.ReadLine();
                return;
            }

            inventory.DisplayInventory();

            Console.WriteLine("\n0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요");

            string input = Console.ReadLine();
            if (input == "0")
            {
                return;
            }

            if (int.TryParse(input, out int number))
            {
                number -= 1; // Adjust for 0-based index
                if (number >= 0 && number < inventory.Items.Count)
                {
                    Item selectedItem = inventory.Items[number];
                    int sellPrice = (int)(selectedItem.Price * 0.8);
                    player._Gold += sellPrice;
                    inventory.RemoveItem(selectedItem);
                    Inventory.GetInstance().EquippedItems.Remove(selectedItem);
                    (GameManager.Instance.Player as Player).ExtraAtkDef(selectedItem.Type == ItemType.Weapon, false, selectedItem.Value);
                    Console.WriteLine($"{selectedItem.Name}을(를) {sellPrice}G에 판매하셨습니다!");
                }
                else
                {
                    Console.WriteLine("잘못된 번호입니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine("엔터를 누르면 상점으로 돌아갑니다.");
            Console.ReadLine();
        }

        public int Start()
        {
            ShowShop(GameManager.Instance.Player as Player);
            return 0;
        }
    }
}
