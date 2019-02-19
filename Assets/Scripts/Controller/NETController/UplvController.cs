using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using DarkGodAgreement;

public class UplvController:BaseController
{
    public UplvController()
    {
        mCurrentReturnSys = ReturnSys.等级提升; 
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
        {
            string[] strArry = data.Split('-');
            int lv = int.Parse(strArry[0]);
            int exp = int.Parse(strArry[1]);
            int currentExp = int.Parse(strArry[2]);
            int guideID = int.Parse(strArry[3]);
            int strength = int.Parse(strArry[4]);
            int intelligence = int.Parse(strArry[5]);
            int physicalPower = int.Parse(strArry[6]);
            int agility = int.Parse(strArry[7]);
            int attackNum = int.Parse(strArry[8]);
            int coin = int.Parse(strArry[9]);
            int crytal = int.Parse(strArry[10]);
            int hp = int.Parse(strArry[11]);
            Debug.Log(hp);
            Debug.Log(physicalPower);
            if (coin != MainGameSys.instance.mCurrentRole.coin)
            {
                GameRoot.ShowHint("<color=#14FF00>" + "经验值 + " + (currentExp + MainGameSys.instance.mCurrentRole.exp - MainGameSys.instance.mCurrentRole.currentExp).ToString() + "</color>");
                GameRoot.ShowHint("<color=#FBEE48>" + "金币 + " + (coin - MainGameSys.instance.mCurrentRole.coin).ToString() + "</color>");
            }
            else
            {
                GameRoot.ShowHint("<color=#FFFF00>" + "经验值 + " + (currentExp - MainGameSys.instance.mCurrentRole.currentExp).ToString() + "</color>");
            }
            GameRoot.ShowHint("<color=#14FF00>等级提升</color>");
            AudioSvc.instance.PlayButClip(GameConstant.RoleUp);
            MainGameSys.instance.mCurrentRole.lv = lv;
            MainGameSys.instance.mCurrentRole.exp = exp;
            MainGameSys.instance.mCurrentRole.currentExp = currentExp;
            MainGameSys.instance.mCurrentRole.guideID = guideID;
            MainGameSys.instance.mCurrentRole.strength = strength;
            MainGameSys.instance.mCurrentRole.intelligence = intelligence;
            MainGameSys.instance.mCurrentRole.physicalPower = physicalPower;
            MainGameSys.instance.mCurrentRole.agility = agility;
            MainGameSys.instance.mCurrentRole.attackNum = attackNum;
            MainGameSys.instance.mCurrentRole.coin = coin;
            MainGameSys.instance.mCurrentRole.crystal = crytal;
            MainGameSys.instance.mCurrentRole.hp = hp;
            if (MainGameSys.instance.isCity)
            {
                MainGameSys.instance.UpdateMainCatyInfo();
            }
            else if (CombatSys.instance.isCombat)
            {
                MainGameSys.instance.UpdateCombatInfo(); 
                MainGameSys.instance.TheCombatUpLvevel();
            }

        });
    }
}

