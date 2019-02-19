using System;
using System.Collections.Generic;
using DarkGodAgreement;
using UnityEngine;
public class FoundTrueController:BaseController
{
    public FoundTrueController()
    {
        mCurrentReturnSys = ReturnSys.创建成功;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
        {
            try
            {
                Debug.Log(data);
                string[] strArry = data.Split('-');
                int roleid = int.Parse(strArry[0]);
                int lv = int.Parse(strArry[3]);
                string playerName = strArry[1];
                int exp = int.Parse(strArry[4]);
                int strength = int.Parse(strArry[5]);
                int intelligence = int.Parse(strArry[6]);
                int physicalPower = int.Parse(strArry[7]);
                int agility = int.Parse(strArry[8]);
                int tired = int.Parse(strArry[9]);
                int currentExp = int.Parse(strArry[10]);
                int currentTired = int.Parse(strArry[11]);
                int attackNum = int.Parse(strArry[12]);
                int guideID = int.Parse(strArry[13]);
                int coin = int.Parse(strArry[14]);
                int hp = int.Parse(strArry[15]);
                Debug.Log(hp);
                int crystal = int.Parse(strArry[16]);
                string[] equipLvStr = strArry[17].Split('#');
                int[] equipLv = new int[6];
                for (int i = 0; i < 6; i++)
                {
                    equipLv[i] = int.Parse(equipLvStr[i]);
                }
                Profession profession = (Profession)int.Parse(strArry[2]);
                RoleDAO role = new RoleDAO(GameRoot.instance.mUser.name, roleid, profession, lv, playerName, exp, strength, intelligence, physicalPower, agility, tired, currentExp, currentTired, attackNum, guideID, coin,hp,crystal,equipLv);
                GameRoot.instance.mRoleList.Add(role);
                GameRoot.ShowHint("创建成功");
                LogonSys.instance.mFoundWin.isShow(false);
                LogonSys.instance.mSelectRoleWin.isShow();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            
            
        });
    }
}

