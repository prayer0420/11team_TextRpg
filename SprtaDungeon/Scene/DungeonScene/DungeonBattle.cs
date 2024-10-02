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
        private Action              playerAction;
        private Action              monsterAction;

        public DungeonBattle(Creature[] monsters)
        {
            Creature[] player = new Creature[1] { GameManager.Instance.Player };

            creatures = player.Concat(monsters).ToArray();
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
            int damage;

            playerAction = GetPlayerCommand();
            monsterAction = GetMonsterCommand();

            /*
             * 
             ********** Battle Logic By Speed ************ 
             * 
             */

            if (true)
            {
                actionQueue.Enqueue(playerAction);
                actionQueue.Enqueue(monsterAction);
            }
            else
            {
                actionQueue.Enqueue(monsterAction);
                actionQueue.Enqueue(playerAction);
            }

            for (int i = 0; i < 2; i++)
            {
                var action = actionQueue.Dequeue();

                // calculating damage
                switch (action.behavior)
                {
                    case Action.Behavior.BASIC_ATTACK:

                        //damage = creatures[(int)action.turn]

                        break;

                    case Action.Behavior.SKILL_ATTACK:

                        //damage = creatures[(int)action.turn]

                        break;

                    case Action.Behavior.ITEM:

                        //creatures[(int)action.turn]

                        break;

                    case Action.Behavior.QUIT:

                        winner = -1;

                        return true;
                }

                // apply damage
                if (action.behavior != Action.Behavior.ITEM)
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

        private Action GetPlayerCommand()
        {
            throw new NotImplementedException();
        }

        private Action GetMonsterCommand()
        {
            Action action = new Action
            {
                behavior = Action.Behavior.BASIC_ATTACK,
                turn = Action.Creature.MONSTER
            };
            return action;
        }
    }
}
