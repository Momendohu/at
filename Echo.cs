using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Ateam {
    public class Echo : BaseBattleAISystem {
        private List<int> playerActorID = new List<int>();
        //private List<int> enemyActorID = new List<int>();

        //---------------------------------------------------
        // InitializeAI
        //---------------------------------------------------
        override public void InitializeAI () {
            //�v���C���[�̃A�N�^�[ID���擾�ۑ�
            for(int i = 0;i < GetTeamCharacterDataList(TEAM_TYPE.PLAYER).Count;i++) {
                playerActorID.Add(GetTeamCharacterDataList(TEAM_TYPE.PLAYER)[i].ActorId);
            }

            //�G�l�~�[�̃A�N�^�[ID���擾�ۑ�
            /*for(int i = 0;i < GetTeamCharacterDataList(TEAM_TYPE.ENEMY).Count;i++) {
                enemyActorID.Add(GetTeamCharacterDataList(TEAM_TYPE.ENEMY)[i].ActorId);
            }*/
        }

        //---------------------------------------------------
        // UpdateAI
        //---------------------------------------------------
        System.Random Rand = new System.Random(4);

        override public void UpdateAI () {
            /*for(int i = 0;i < playerActorID.Count;i++) {
                switch(Rand.Next(4)) {
                    case 0:
                    Move(playerActorID[i],Common.MOVE_TYPE.RIGHT);
                    break;

                    case 1:
                    Move(playerActorID[i],Common.MOVE_TYPE.LEFT);
                    break;

                    case 2:
                    Move(playerActorID[i],Common.MOVE_TYPE.UP);
                    break;

                    case 3:
                    Move(playerActorID[i],Common.MOVE_TYPE.DOWN);
                    break;
                }
            }*/

            //�P���ɕ��������߂�
            for(int i = 0;i < 3;i++) {
                //xy��r
                if(GetX_Distance(0,0)< GetY_Distance(0,0)) {
                    if(GetX_Distance(0,0) > 0) {
                        Move(playerActorID[i],Common.MOVE_TYPE.RIGHT);
                    } else {
                        Move(playerActorID[i],Common.MOVE_TYPE.LEFT);
                    }
                } else {
                    if(GetY_Distance(0,0) > 0) {
                        Move(playerActorID[i],Common.MOVE_TYPE.DOWN);
                    } else {
                        Move(playerActorID[i],Common.MOVE_TYPE.UP);
                    }
                }
            }

            Action(playerActorID[0],Define.Battle.ACTION_TYPE.ATTACK_LONG);
            Action(playerActorID[1],Define.Battle.ACTION_TYPE.ATTACK_LONG);
            Action(playerActorID[2],Define.Battle.ACTION_TYPE.ATTACK_LONG);
        }

        //�}���n�b�^���������擾����
        public int GetM_Distance (int player,int enemy) {
            return GetX_Distance(player,enemy) + GetY_Distance(player,enemy);
        }

        //x�������̋������擾����
        public int GetX_Distance (int player,int enemy) {
            return ((int)(GetTeamCharacterDataList(TEAM_TYPE.ENEMY)[enemy].BlockPos.x - GetTeamCharacterDataList(TEAM_TYPE.PLAYER)[player].BlockPos.x));
        }

        //y�������̋������擾����
        public int GetY_Distance (int player,int enemy) {
            return ((int)(GetTeamCharacterDataList(TEAM_TYPE.ENEMY)[enemy].BlockPos.y - GetTeamCharacterDataList(TEAM_TYPE.PLAYER)[player].BlockPos.y));
        }

        //---------------------------------------------------
        // ItemSpawnCallback
        //---------------------------------------------------
        override public void ItemSpawnCallback (ItemSpawnData itemData) {
        }
    }
}