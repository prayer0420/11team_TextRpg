using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    public class QuestScene : Scene
    {

        int Scene.Start()
        {

            Creature player = GameManager.Instance.Player;

            Console.Clear();

            Console.WriteLine("어서오세요. 여기는 퀘스트 의뢰소 입니다.\n 무엇을 하시겠나요?");
            Console.WriteLine("1. 진행 가능한 퀘스트 보기");
            Console.WriteLine("2. 진행 중인 퀘스트 보기");
            Console.WriteLine("3. 퀘스트 완료 하기");

            Console.Write(">> ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    QuestManager.GetInstance().ShowAvailableQuests(player);
                    Console.WriteLine("퀘스트를 선택하여 상세정보를 보거나 수락하세요:");
                    int questIndex = int.Parse(Console.ReadLine());
                    QuestManager.GetInstance().SelectQuest(questIndex, player);
                    break;
                case 2:
                    QuestManager.GetInstance().ShowActiveQuests();
                    break;
                case 3:
                    QuestManager.GetInstance().ShowActiveQuests();
                    Console.WriteLine("완료할 퀘스트를 선택하세요:");
                    int completeIndex = int.Parse(Console.ReadLine());
                    QuestManager.GetInstance().CompleteQuest(completeIndex, player);
                    break;
            }


            return 0;
        }
    }
}
