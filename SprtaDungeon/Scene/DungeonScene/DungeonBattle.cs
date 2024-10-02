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

        public DungeonBattle(Creature[] monsters)
        {
            Creature[] player = new Creature[1] { GameManager.Instance.Player };

            creatures = player.Concat(monsters).ToArray();
            battleInfoDisplay = new DungeonDisplayBattleInfo(creatures);
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

            //if (winner == 0)
            //{
            //    (creatures[0] as Player).LevelUp();
            //}

            return 0;
        }

        private bool BattleLoop(out int winner)
        {
            GetPlayerCommand();
            GetMonsterCommand();
            SetTurnOrder();

            for (int i = 0; i < 2; i++)
            {
                var action = actionQueue.Dequeue();

                int damage;

                // calculating damage
                switch (action.behavior)
                {
                    case Action.Behavior.BASIC_ATTACK:

                        //damage = creatures[(int)action.turn]

                        break;

                    case Action.Behavior.SKILL_ATTACK:

                        //damage = creatures[(int)action.turn]

                        break;

                    case Action.Behavior.POTION:

                        //creatures[(int)action.turn]

                        break;

                    case Action.Behavior.QUIT:

                        winner = -1;

                        return true;
                }

                // apply damage
                if (action.behavior != Action.Behavior.POTION)
                {
                    int defender;
                    int defense;

                    if (action.turn == Action.Creature.PLAYER) defender = (int)Action.Creature.MONSTER;
                    else defender = (int)Action.Creature.PLAYER;

                    /*
                     * ******* TODO : BATTLE DAMAGE CALCULATING LOGIC
                     */

                    //defense = creatures[defender];

                    //damage -= defense;

                    // TEMP CODE
                    damage = 0;

                    if (damage <= 0) damage = 1;
                    //creatures[defender].CurHP -= damage;


                    // IF DEFENDERS HP IS ZERO
                    if (true)//creatures[defender].CurHP <= 0)
                    {
                        //creatures[defender].CurHP = 0;

                        winner = (int)action.turn;
                        return true;
                    }

                }
            }

            actionQueue.Clear();

            winner = -1;
            return false;
        }

        private void SetTurnOrder()
        {
            List<Action> actionList = actions.ToList();

            for(int i = 0; i < actions.Length - 1; i++) 
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

                        display = new DungeonDisplayBasicAttack(creatures.Length - 1);
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

                        display = new DungeonDisplaySkill(Console.CursorTop, creatures[0]);
                        display.Display();
                        int skillNum = display.Select();

                        if (skillNum == 0) continue;

                        battleInfoDisplay.Display();

                        display = new DungeonDisplaySkill(creatures.Length - 1);
                        display.Display();
                        target = display.Select();

                        if (target == 0) continue;

                        actions[0] = new Action()
                        {
                            behavior = Action.Behavior.SKILL_ATTACK,
                            turn = Action.Creature.PLAYER,
                            creatureNum = 0,
                            speed = creatures[0].Speed(),
                            skillNum = skillNum,
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
                            itemNum = potion
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
                    creatureNum = 1,
                    speed = 0,
                    targetNum = 0
                };
            }
        }
    }
}
