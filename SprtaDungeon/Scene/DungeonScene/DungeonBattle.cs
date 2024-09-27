using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon.Scene.DungeonScene
{
    internal class DungeonBattle
    {
        private Creature[] creatures;
        private Queue<Action> actionQueue;
        private Action playerAction;
        private Action monsterAction;

        public DungeonBattle()
        {

        }

        public int StartBattle()
        {
            bool end;
            int winner;
            actionQueue.Clear();

            //do
            //{
            //    end = BattleLoop(out winner);
            //} while (!end);

            //if (winner == 0)
            //{
            //    SetNarration("승리했습니다! 레벨이 올랐습니다.");
            //    Console.Clear();
            //    BattleDisplay();
            //    Console.ReadLine();

            //    (creatures[0] as Player).LevelUp();
            //}
            //else
            //{
            //    SetNarration("패배했습니다... 던전 입구로 돌아갑니다. 체력을 회복하고 정비한 뒤 다시 시도해 주십시오.");
            //    Console.Clear();
            //    BattleDisplay();
            //    Console.ReadLine();
            //}

            return 0;
        }

        private bool BattleLoop(out int winner)
        {
            Console.Clear();
            //playerAction = GetPlayerCommand();
            //monsterAction = GetEnemyCommand();

            //if (creatures[0].Speed > creatures[1].Speed)
            //{
            //    actionQueue.Enqueue(playerAction);
            //    actionQueue.Enqueue(monsterAction);
            //}
            //else
            //{
            //    actionQueue.Enqueue(monsterAction);
            //    actionQueue.Enqueue(playerAction);
            //}

            int damage = 0;

            for (int i = 0; i < 2; i++)
            {
                var action = actionQueue.Dequeue();

                //// calculating damage
                //switch (action.behavior)
                //{
                //    case Action.Behavior.BASIC_ATTACK:
                //        damage = creatures[action.character].BasicAttack();

                //        SetNarration(creatures[action.character].Name + "의 공격!");
                //        Console.Clear();
                //        BattleDisplay();
                //        Console.ReadLine();
                //        break;

                //    case Action.Behavior.ITEM:
                //        creatures[action.character].UseItem(action.num);
                //        break;

                //    case Action.Behavior.QUIT:
                //        winner = -1;
                //        return true;
                //}

                //// apply damage
                //if (action.behavior != Action.Behavior.ITEM)
                //{
                //    int defender;
                //    int defense;

                //    if (action.character == 0) defender = 1;
                //    else defender = 0;

                    //defense = creatures[defender].Defense;

                    //damage -= defense;

                    //if (damage <= 0) damage = 1;
                    //creatures[defender].CurHP -= damage;

                    //if (creatures[defender].CurHP <= 0)
                    //{
                    //    creatures[defender].CurHP = 0;

                    //    SetNarrationDamage(damage, action.character);
                    //    Console.Clear();
                    //    BattleDisplay();
                    //    Console.ReadLine();

                    //    winner = action.character;
                    //    return true;
                    //}

                //    SetNarrationDamage(damage, action.character);
                //    Console.Clear();
                //    BattleDisplay();
                //    Console.ReadLine();
                //}
                //else
                //{
                //    SetNarrationItem();
                //    Console.Clear();
                //    BattleDisplay();
                //    Console.ReadLine();
                //}
            }

            actionQueue.Clear();


            //if (characters[0].CurHP <= 0)
            //{
            //    winner = 1;
            //    return true;
            //}
            //else if (characters[1].CurHP <= 0)
            //{
            //    winner = 0;
            //    return true;
            //}
            //else
            //{
            //    winner = -1;
            //    return false;
            //}

            winner = -1;
            return false;
        }

        //private Action GetEnemyCommand()
        //{
        //    Action action = new Action
        //    {
        //        behavior = Action.Behavior.BASIC_ATTACK,
        //        character = 1
        //    };
        //    return action;
        //}
    }
}
