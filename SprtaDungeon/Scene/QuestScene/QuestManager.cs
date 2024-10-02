using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    class QuestManager
    {
        public List<Quest> availableQuests; //진행 가능한 퀘스트 
        private List<Quest> activeQuests; //진행중인 퀘스트

        private static QuestManager instance;
        public static QuestManager GetInstance()
        {
            if (instance == null)
            {
                instance = new QuestManager();
            }
            return instance;
        }

        public QuestManager()
        {
            availableQuests = new List<Quest>();
            activeQuests = new List<Quest>();
            AddQuests();
        }

        public void AddQuest(Quest quest)
        {
            availableQuests.Add(quest);
        }

        //진행 가능한 퀘스트 보여주기
        public void ShowAvailableQuests(Creature player)
        {
            Console.WriteLine("진행 가능한 퀘스트");
            int questCount = 0;
            foreach (Quest quest in availableQuests)
            {
                if (quest != null)
                {
                    if (quest.RequiredLevel <= player._Lv
                    && (quest.RequiredQuest == null
                    || quest.RequiredQuest.Status == QuestStatus.Completed))
                    {
                        Console.WriteLine($"{(questCount++) + 1}. {quest.Name} (레벨 {quest.RequiredLevel} 이상)");
                    }
                }
                else
                {
                    return;
                }
            }
        }

        //진행중인 퀘스트의 진행량 보여주기
        public void ShowActiveQuests()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[진행중인 퀘스트]");
            Console.ResetColor();

            int questCount = 0;
            foreach (Quest quest in activeQuests)
            {
                if (quest != null)
                {
                    activeQuests[questCount++].ShowProgress();
                }
            }
            Console.WriteLine("\n0.나가기\n");
            string input = Console.ReadLine();
            if (input == "0")
                return;
        }

        //퀘스트 선택하기 
        public void SelectQuest(int index, Creature player)
        {
            //진행 가능한 퀘스트의 범위를 벗어날때
            if (index < 1 || index > availableQuests.Count)
            {
                Console.WriteLine("잘못된 선택입니다");
                return;
            }

            //플레이어 레벨이 요구 레벨보다 낮으면 X
            Quest quest = availableQuests[index - 1];
            if (quest.RequiredLevel > player._Lv)
            {
                Console.WriteLine($"레벨 {quest.RequiredLevel} 이상이어야 이 퀘스트를 수락할 수 있습니다.");
                return;
            }

            //선행 퀘스트가 있는데도 선행 퀘스트를 완료한 상태가 아니라면 X
            if (quest.RequiredQuest != null && quest.RequiredQuest.Status != QuestStatus.Completed)
            {
                Console.WriteLine($"이 퀘스트를 수락하려면 이전 퀘스트 '{quest.RequiredQuest.Name}'를 완료해야 합니다.");
                return;
            }

            Console.WriteLine($"퀘스트 선택됨: {quest.Name}");
            Console.WriteLine(quest.Description);

            //선택 한 후 
            foreach (var objective in quest.Objectives)
            {
                Console.WriteLine($"- {objective.Key} ({quest.Progress[objective.Key]}/{objective.Value})");
            }

            Console.WriteLine();

            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                activeQuests.Add(quest);
                availableQuests.RemoveAt(index - 1);
                quest.Status = QuestStatus.InProgress;
                Console.WriteLine("퀘스트 수락됨.");
            }
            else
            {
                Console.WriteLine("퀘스트 거절됨.");
            }
        }

        public void CompleteQuest(int index, Creature player)
        {
            //진행 중인 퀘스트의 범위를 벗어날때
            if (index < 1 || index > activeQuests.Count)
            {
                Console.WriteLine("잘못된 선택입니다.");
                return;
            }

            Quest quest = activeQuests[index - 1];
            //퀘스트가 완료 상태라면
            if (quest.Status == QuestStatus.Completed)
            {
                player._Gold += quest.GoldReward;
                Console.WriteLine($"골드 {quest.GoldReward}을(를) 받았습니다.");
                foreach (var reward in quest.Rewards)
                {
                    Console.WriteLine($"아이템 획득 : {reward.Name}");
                    Inventory.GetInstance().AddItem(reward);
                }
                //이제 진행중인 퀘스트 목록에서 제거
                activeQuests.RemoveAt(index - 1);
            }
            else
            {
                Console.WriteLine("퀘스트가 아직 완료되지 않았습니다.");
            }
        }

        // 퀘스트 진행 상태 업데이트
        public void UpdateKillCount(string monsterName)
        {
            foreach (var quest in activeQuests)
            {
                if (quest.Status == QuestStatus.InProgress)
                {
                    if (quest.Objectives.ContainsKey(monsterName))
                    {
                        if (!quest.Progress.ContainsKey(monsterName))
                            quest.Progress[monsterName] = 0;

                        quest.Progress[monsterName]++;
                        Console.WriteLine($"[퀘스트 진행] '{quest.Name}' 퀘스트의 '{monsterName}' 처치 수: {quest.Progress[monsterName]}/{quest.Objectives[monsterName]}");

                        if (quest.IsCompleted())
                        {
                            quest.Status = QuestStatus.Completed;
                            Console.WriteLine($"[퀘스트 완료] '{quest.Name}' 퀘스트를 완료하였습니다!");
                        }
                    }
                }
            }
        }
        // **추가된 부분 끝**

        //퀘스트 상태 업데이트
        public void UpdateQuestProgress(string objective, int amount)
        {
            foreach (var quest in activeQuests)
            {
                quest.UpdateProgress(objective, amount);
            }
        }

        private void AddQuests()
        {
            // 퀘스트 1: 미니언 처치
            Quest quest1 = new Quest
            (
                "미니언 퇴치",
                "마을 주민:\n마을 주변에 미니언들이 출몰하고 있습니다. 미니언 5마리를 처치해 주세요!",
                new Dictionary<string, int> { { "미니언", 5 } }, // **수정된 부분: 몬스터 이름**
                new List<Item> { new Potion(PotionType.Health, 2) }, // 보상: 체력 물약 2개
                10, // 경험치 보상
                QuestStatus.NotStarted,
                1 // 필요한 최소 레벨
            );

            // 퀘스트 2: 공허충 처치
            Quest quest2 = new Quest
            (
                "공허충 퇴치",
                "마을 장로:\n미니언들을 처치해 주셔서 감사합니다. 하지만 이제 공허충들이 나타나고 있습니다. 공허충 4마리를 처치해 주세요!",
                new Dictionary<string, int> { { "공허충", 4 } }, // **수정된 부분: 몬스터 이름**
                new List<Item> { new Item(ItemType.Weapon) },
                20,
                QuestStatus.NotStarted,
                2
            );

            // 퀘스트 3: 대포 미니언 처치
            Quest quest3 = new Quest
            (
                "대포 미니언 퇴치",
                "마을 장로:\n공허충들을 물리쳐 주셔서 감사합니다. 마지막으로 대포 미니언들이 남았습니다. 대포 미니언 3마리를 처치해 주세요!",
                new Dictionary<string, int> { { "대포 미니언", 3 } }, // **수정된 부분: 몬스터 이름**
                new List<Item> { new Item(ItemType.Armor) },
                30,
                QuestStatus.NotStarted,
                3
            );

            quest2.RequiredQuest = quest1;
            quest3.RequiredQuest = quest2;

            AddQuest(quest1);
            AddQuest(quest2);
            AddQuest(quest3);
        }
    }
}
