using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprtaDungeon
{
    internal class DungeonBattle
    {
        private Creature[]          creatures;
        private Queue<Action>       actionQueue;
        private Action[]            actions;
        private Display             display;
        private Display             battleInfoDisplay;
        private Random              random;

        public DungeonBattle(Creature[] monsters, int seed)
        {
            Creature[] player = new Creature[1] { GameManager.Instance.Player };

            creatures = player.Concat(monsters).ToArray();
            battleInfoDisplay = new DungeonDisplayBattleInfo(creatures);
            random = new Random(seed);
            actions = new Action[creatures.Length];
            actionQueue = new Queue<Action>();
        }

        public int StartBattle()
        {
            bool end;
            int winner;
            actionQueue.Clear();

            do
            {
                end = BattleLoop(out winner);
            } while (!end);

            if (winner == 0)
            {
                (creatures[0] as Player).LvUp();
            }

            return winner;
        }

        private bool BattleLoop(out int winner)
        {
            GetPlayerCommand();
            GetMonsterCommand();
            SetTurnOrder();

            for (int i = 0; i < actions.Length; i++)
            {
                var action = actionQueue.Dequeue();

                Creature creature = creatures[action.creatureNum];
                int attackDamage = 0;
                bool critical = creature.Critical(random.Next(1, 101));
                Player player = creature as Player;

                // calculating damage
                switch (action.behavior)
                {
                    case Action.Behavior.BASIC_ATTACK:

                        attackDamage = creature.Attack(critical);

                        break;

                    case Action.Behavior.SKILL_ATTACK:

                        if(player == null) break;

                        attackDamage = player.SkillAttack(action.skillNum, critical);

                        break;

                    case Action.Behavior.POTION:

                        if(player == null) break;

                        int potionNum = action.itemNum;
                        Potion potion = player.inventory.potions[potionNum];
                        player.Heal(potion.Value);
                        potion.PotionCount--;
                        if(potion.PotionCount <= 0) player.inventory.potions.Remove(potion);

                        break;

                    case Action.Behavior.QUIT:

                        winner = 1;

                        return true;
                }

                // apply damage
                if (action.behavior != Action.Behavior.POTION)
                {
                    int defender;
                    int defense;

                    if (action.turn == Action.Creature.PLAYER) defender = action.targetNum;
                    else defender = (int)Action.Creature.PLAYER;

                    /*
                     * ******* TODO : BATTLE DAMAGE CALCULATING LOGIC
                     */

                    defense = creatures[defender]._Def;

                    int finalDamage = attackDamage -= defense;


                    if (finalDamage <= 0) finalDamage = 1;

                    //// 몬스터 처치 시
                    //if (creatures[defender]._CurHp<= 0 && creatures[defender] != GameManager.Instance.Player)
                    //{
                    //    Console.WriteLine($"{creatures[defender]._Name}을(를) 처치했습니다!");

                    //    // **퀘스트 진행 업데이트**
                    //    QuestManager.GetInstance().UpdateKillCount(creatures[defender]._Name);
                    //}


                    creatures[defender].ApplyDamage(finalDamage);


                    if (BattleFinished(out winner)) return true;
                }
            }

            actionQueue.Clear();

            winner = -1;
            return false;

            bool BattleFinished(out int win)
            {
                if (creatures[0]._CurHp <= 0)
                {
                    win = 1;

                    return true;
                }

                for (int i = 1; i < creatures.Length; i++)
                {
                    if (creatures[i]._CurHp > 0)
                    {
                        win = -1;
                        return false;
                    }
                }

                win = 0;
                return true;
            }
        }

        private void SetTurnOrder()
        {
            List<Action> actionList = actions.ToList();

            for (int i = 0; i < actions.Length; i++) 
            {
                int temp = 0;
                int tempSpeed = actionList[temp].speed;

                for(int j = 0; j < actionList.Count; j++)
                {
                    if (tempSpeed < actionList[j].speed)
                    {
                        tempSpeed = actionList[j].speed;
                        temp = j;
                    }
                }
                
                actionQueue.Enqueue(actionList[temp]);
                actionList.RemoveAt(temp);
            }
        }

        private void GetPlayerCommand()
        {
            while(true)
            {
                int target;

                battleInfoDisplay.Display();

                display = new DungeonDisplayPlayerCommand();
                display.Display();
                int playerBehavior = display.Select();

                battleInfoDisplay.Display();

                switch ((Action.Behavior)playerBehavior)
                {
                    case Action.Behavior.BASIC_ATTACK:

                        display = new DungeonDisplayBasicAttack(creatures.Length - 1, creatures);
                        display.Display();
                        target = display.Select();

                        if (target == 0) continue;

                        actions[0] = new Action()
                        {
                            behavior = Action.Behavior.BASIC_ATTACK,
                            turn = Action.Creature.PLAYER,
                            creatureNum = 0,
                            speed = creatures[0].Speed(),
                            targetNum = target
                        };

                        break;

                    case Action.Behavior.SKILL_ATTACK:

                        display = new DungeonDisplaySkill(Console.CursorTop, creatures);
                        display.Display();
                        int skillNum = display.Select();

                        if (skillNum == 0) continue;

                        battleInfoDisplay.Display();

                        display = new DungeonDisplaySkill(creatures);
                        display.Display();
                        target = display.Select();

                        if (target == 0) continue;

                        actions[0] = new Action()
                        {
                            behavior = Action.Behavior.SKILL_ATTACK,
                            turn = Action.Creature.PLAYER,
                            creatureNum = 0,
                            speed = creatures[0].Speed(),
                            skillNum = skillNum - 1,
                            targetNum = target
                        };

                        break;

                    case Action.Behavior.POTION:

                        display = new InventoryDisplayPotion(true, false, (creatures[0] as Player).inventory.potions);
                        display.Display();
                        int potion = display.Select();

                        if (potion == 0) continue;

                        actions[0] = new Action()
                        {
                            behavior = Action.Behavior.POTION,
                            turn = Action.Creature.PLAYER,
                            creatureNum = 0,
                            speed = creatures[0].Speed(),
                            itemNum = potion - 1
                        };

                        break;

                    case Action.Behavior.QUIT:

                        actions[0] = new Action()
                        {
                            behavior = Action.Behavior.QUIT,
                            turn = Action.Creature.PLAYER,
                            creatureNum = 0,
                            speed = int.MaxValue
                        };

                        break;
                }

                break;
            }
        }

        private void GetMonsterCommand()
        {
            for(int i = 1; i < creatures.Length; i++)
            {
                actions[i] = new Action
                {
                    behavior = Action.Behavior.BASIC_ATTACK,
                    turn = Action.Creature.MONSTER,
                    creatureNum = i,
                    speed = creatures[i].Speed(),
                    targetNum = 0
                };
            }
        }
    }
}
