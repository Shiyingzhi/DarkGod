using System;
using System.Collections.Generic;
using DarkGodAgreement;
using UnityEngine;

    public class SelectRoleController:BaseController
    {
        public SelectRoleController()
        {
            mCurrentReturnSys = ReturnSys.选择角色;
        }
        public override void ProcessMessage(ReturnSys sys, string data)
        {
            DisposeData(sys, () =>
            {
                try 
                {
                    if (data == "没有角色")
                    {
                        //进入选择角色面板
                        Debug.Log("这里执行了吗");
                        LogonSys.instance.mSelectRoleWin.isShow();
                    }
                    else
                    {
                        Debug.Log("这里也执行了");
                        string[] strArry = data.Split('/');
                        int roleCount = int.Parse(strArry[0]);
                        Debug.Log(roleCount);
                        for (int i = 1; i <= roleCount; i++)
                        {
                            
                            string[] roleStr = strArry[i].Split('-');
                            Debug.Log(roleStr.Length);
                            int roleid = int.Parse(roleStr[0]);
                            int lv = int.Parse(roleStr[3]);
                            string playerName = roleStr[1];
                            int exp= int.Parse(roleStr[4]);
                            int strength=int.Parse(roleStr[5]);
                            int intelligence=int.Parse(roleStr[6]);
                            int physicalPower=int.Parse(roleStr[7]);
                            int agility =int.Parse(roleStr[8]);
                            int tired = int.Parse(roleStr[9]);
                            int currentExp = int.Parse(roleStr[10]);
                            int currentTired = int.Parse(roleStr[11]);
                            int attackNum = int.Parse(roleStr[12]);
                            int guideID = int.Parse(roleStr[13]);
                            int coin = int.Parse(roleStr[14]);
                            Profession profession = (Profession)int.Parse(roleStr[2]);
                            int hp = int.Parse(roleStr[15]);
                            int crystal = int.Parse(roleStr[16]);
                            string[] equipLvStr = roleStr[17].Split('#');
                            int[] equipLv = new int[6];
                            for (int j = 0; j < 6; j++)
                            {
                                equipLv[j] = int.Parse(equipLvStr[j]);
                                Debug.Log("元素"+ equipLv[j]);
                            }
                            int combartNum = int.Parse(roleStr[18]);
                            int intensifyNum = int.Parse(roleStr[19]);
                            int killMOBSNum = int.Parse(roleStr[20]);
                            int worldTalkNum = int.Parse(roleStr[21]);
                            int killBossNum = int.Parse(roleStr[22]);
                            RoleDAO role = new RoleDAO(GameRoot.instance.mUser.name, roleid, profession, lv, playerName, exp, strength, intelligence, physicalPower, agility, tired, currentExp, currentTired, attackNum, guideID, coin,hp,crystal,equipLv
                                , combartNum, intensifyNum, killMOBSNum, worldTalkNum, killBossNum);
                            GameRoot.instance.mRoleList.Add(role);
                        }
                        //进入选择角色面板
                        LogonSys.instance.mSelectRoleWin.isShow();
                    }
                }catch(Exception e)
                {
                    Debug.Log(e);
                    LogonSys.instance.mSelectRoleWin.isShow();
                }
                
                
            });
        }
    }

