using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Ateam {
    public class Echo : BaseBattleAISystem {
        private List<int> playerActorID = new List<int>();
        float time = 0;
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
            time++;
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

            Debug.Log(((int)time / 120) % 3);
            ActToEnemy(((int)time / 120) % 3);


        }

        //�w�肵���G�̕����ɓ���
        private void ActToEnemy (int enemy) {
            //�P���ɕ��������߂�
            for(int i = 0;i < 3;i++) {
                //xy��r
                if(Math.Abs(GetX_Distance(i,enemy)) > Math.Abs(GetY_Distance(i,enemy))) {
                    if(GetX_Distance(i,enemy) >= 0) {
                        if(GetX_Distance(i,enemy) > 1) {
                            Move(playerActorID[i],Common.MOVE_TYPE.RIGHT);
                        } else {
                            Move(playerActorID[i],Common.MOVE_TYPE.RIGHT);
                        }
                    } else if(GetX_Distance(i,enemy) < 0) {
                        if(GetX_Distance(i,enemy) < -1) {
                            Move(playerActorID[i],Common.MOVE_TYPE.LEFT);
                        } else {
                            Move(playerActorID[i],Common.MOVE_TYPE.LEFT);
                        }
                    } else {

                    }
                } else if(GetX_Distance(i,enemy) < GetY_Distance(i,enemy)) {
                    if(GetY_Distance(i,enemy) > 0) {
                        if(GetY_Distance(i,enemy) > 1) {
                            Move(playerActorID[i],Common.MOVE_TYPE.UP);
                        } else {
                            Move(playerActorID[i],Common.MOVE_TYPE.UP);
                        }
                    } else if(GetY_Distance(i,enemy) < 0) {
                        if(GetY_Distance(i,enemy) < -1) {
                            Move(playerActorID[i],Common.MOVE_TYPE.DOWN);
                        } else {
                            Move(playerActorID[i],Common.MOVE_TYPE.DOWN);
                        }
                    } else {
                    }
                } else {

                }

                if(GetM_Distance(i,enemy) <= 4) {
                    Action(playerActorID[0],Define.Battle.ACTION_TYPE.INVINCIBLE);
                    Action(playerActorID[1],Define.Battle.ACTION_TYPE.INVINCIBLE);
                    Action(playerActorID[2],Define.Battle.ACTION_TYPE.INVINCIBLE);
                }

                if(GetM_Distance(i,enemy) <= 10) {
                    if(GetM_Distance(i,enemy) <= 3) {
                        Action(playerActorID[0],Define.Battle.ACTION_TYPE.ATTACK_SHORT);
                        Action(playerActorID[1],Define.Battle.ACTION_TYPE.ATTACK_SHORT);
                        Action(playerActorID[2],Define.Battle.ACTION_TYPE.ATTACK_SHORT);
                    } else {
                        Action(playerActorID[0],Define.Battle.ACTION_TYPE.ATTACK_MIDDLE);
                        Action(playerActorID[1],Define.Battle.ACTION_TYPE.ATTACK_MIDDLE);
                        Action(playerActorID[2],Define.Battle.ACTION_TYPE.ATTACK_MIDDLE);
                    }
                } else {
                    Action(playerActorID[0],Define.Battle.ACTION_TYPE.ATTACK_LONG);
                    Action(playerActorID[1],Define.Battle.ACTION_TYPE.ATTACK_LONG);
                    Action(playerActorID[2],Define.Battle.ACTION_TYPE.ATTACK_LONG);
                }
            }
        }

        //�}���n�b�^���������擾����
        public int GetM_Distance (int player,int enemy) {
            return Math.Abs(GetX_Distance(player,enemy)) + Math.Abs(GetY_Distance(player,enemy));
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