using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Ateam {
    public class Echo : BaseBattleAISystem {
        private List<int> playerActorID = new List<int>();
        private List<int> enemyActorID = new List<int>();

        //---------------------------------------------------
        // InitializeAI
        //---------------------------------------------------
        override public void InitializeAI () {
            //�v���C���[�̃A�N�^�[ID���擾�ۑ�
            for(int i = 0;i < GetTeamCharacterDataList(TEAM_TYPE.PLAYER).Count;i++) {
                playerActorID.Add(GetTeamCharacterDataList(TEAM_TYPE.PLAYER)[i].ActorId);
            }

            //�G�l�~�[�̃A�N�^�[ID���擾�ۑ�
            for(int i = 0;i < GetTeamCharacterDataList(TEAM_TYPE.ENEMY).Count;i++) {
                enemyActorID.Add(GetTeamCharacterDataList(TEAM_TYPE.ENEMY)[i].ActorId);
            }
        }

        //---------------------------------------------------
        // UpdateAI
        //---------------------------------------------------
        System.Random Rand = new System.Random(4);

        override public void UpdateAI () {
            //Debug.Log(((int)time / 120) % 3);
            ActToEnemy(0,0);
            ActToEnemy(1,1);
            ActToEnemy(2,2);
        }

        //�w�肵���G�̕����ɓ���
        private void ActToEnemy (int player,int enemy) {
            //Debug.Log(GetTeamCharacterDataList(TEAM_TYPE.PLAYER).Count);
            //�P���ɕ��������߂�

            //xy��r
            if(Math.Abs(GetX_Distance(player,enemy)) > Math.Abs(GetY_Distance(player,enemy))) {
                if(GetX_Distance(player,enemy) >= 0) {
                    if(GetX_Distance(player,enemy) > 1) {
                        Move(playerActorID[player],Common.MOVE_TYPE.RIGHT);
                    } else {
                        Move(playerActorID[player],Common.MOVE_TYPE.DOWN);
                    }
                } else if(GetX_Distance(player,enemy) < 0) {
                    if(GetX_Distance(player,enemy) < -1) {
                        Move(playerActorID[player],Common.MOVE_TYPE.LEFT);
                    } else {
                        Move(playerActorID[player],Common.MOVE_TYPE.UP);
                    }
                } else {

                }
            } else if(GetX_Distance(player,enemy) < GetY_Distance(player,enemy)) {
                if(GetY_Distance(player,enemy) > 0) {
                    if(GetY_Distance(player,enemy) > 1) {
                        Move(playerActorID[player],Common.MOVE_TYPE.UP);
                    } else {
                        Move(playerActorID[player],Common.MOVE_TYPE.LEFT);
                    }
                } else if(GetY_Distance(player,enemy) < 0) {
                    if(GetY_Distance(player,enemy) < -1) {
                        Move(playerActorID[player],Common.MOVE_TYPE.DOWN);
                    } else {
                        Move(playerActorID[player],Common.MOVE_TYPE.RIGHT);
                    }
                } else {
                }
            } else {

            }

            if(Rand.Next(4)<1) {
                for(int i = 0;i < playerActorID.Count;i++) {
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
                }
            }

            if(GetM_Distance(player,enemy) <= 3) {
                Action(playerActorID[0],Define.Battle.ACTION_TYPE.INVINCIBLE);
                Action(playerActorID[1],Define.Battle.ACTION_TYPE.INVINCIBLE);
                Action(playerActorID[2],Define.Battle.ACTION_TYPE.INVINCIBLE);
            }

            if(GetM_Distance(player,enemy) <= 10) {
                if(GetM_Distance(player,enemy) <= 3) {
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

        //�}���n�b�^���������擾����
        public int GetM_Distance (int player,int enemy) {
            return Math.Abs((int)GetX_Distance(player,enemy)) + Math.Abs((int)GetY_Distance(player,enemy));
        }

        //x�������̋������擾����
        public float GetX_Distance (int player,int enemy) {
            return (GetTeamCharacterDataList(TEAM_TYPE.ENEMY)[enemy].BlockPos.x - GetTeamCharacterDataList(TEAM_TYPE.PLAYER)[player].BlockPos.x);
        }

        //y�������̋������擾����
        public float GetY_Distance (int player,int enemy) {
            return (GetTeamCharacterDataList(TEAM_TYPE.ENEMY)[enemy].BlockPos.y - GetTeamCharacterDataList(TEAM_TYPE.PLAYER)[player].BlockPos.y);
        }

        //---------------------------------------------------
        // ItemSpawnCallback
        //---------------------------------------------------
        private ItemSpawnData IsItemData;
        private bool IsSetItem;

        override public void ItemSpawnCallback (ItemSpawnData itemData) {
            SetItemData(itemData);
            IsSetItem=true;
        }

        public void SetItemData (ItemSpawnData data) {
            IsItemData = data;
        }

        public ItemSpawnData GetItemData () {
            return IsItemData;
        }
    }
}