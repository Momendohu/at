using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ateam {
    public class Echo : BaseBattleAISystem {
        private List<int> actorID = new List<int>();

        //---------------------------------------------------
        // InitializeAI
        //---------------------------------------------------
        override public void InitializeAI () {
            for(int i = 0;i < GetTeamCharacterDataList(TEAM_TYPE.PLAYER).Count;i++) {
                actorID.Add(GetTeamCharacterDataList(TEAM_TYPE.PLAYER)[i].ActorId);
            }
        }

        //---------------------------------------------------
        // UpdateAI
        //---------------------------------------------------
        System.Random Rand = new System.Random(4);

        override public void UpdateAI () {
            for(int i=0;i<actorID.Count;i++) {
                switch(Rand.Next(4)) {
                    case 0:
                    Move(actorID[i],Common.MOVE_TYPE.RIGHT);
                    break;

                    case 1:
                    Move(actorID[i],Common.MOVE_TYPE.LEFT);
                    break;

                    case 2:
                    Move(actorID[i],Common.MOVE_TYPE.UP);
                    break;

                    case 3:
                    Move(actorID[i],Common.MOVE_TYPE.DOWN);
                    break;
                }
            }

            Action(actorID[0],Define.Battle.ACTION_TYPE.ATTACK_LONG);
            Action(actorID[1],Define.Battle.ACTION_TYPE.ATTACK_LONG);
            Action(actorID[2],Define.Battle.ACTION_TYPE.ATTACK_LONG);
        }

        //---------------------------------------------------
        // ItemSpawnCallback
        //---------------------------------------------------
        override public void ItemSpawnCallback (ItemSpawnData itemData) {
        }
    }
}