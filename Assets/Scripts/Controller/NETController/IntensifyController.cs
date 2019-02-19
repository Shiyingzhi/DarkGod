using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DarkGodAgreement;

public class IntensifyController:BaseController
{
    public IntensifyController()
    {
        mCurrentReturnSys = ReturnSys.强化;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
            {
                Debug.Log(data);
                string[] strArray = data.Split('-');
                int strength  = int.Parse(strArray[0]);
                int intelligence = int.Parse(strArray[1]);
                int physicalPower = int.Parse(strArray[2]);
                int agility = int.Parse(strArray[3]);
                int attackNum = int.Parse(strArray[4]);
                int coin = int.Parse(strArray[5]);
                int cryal = int.Parse(strArray[6]);
                string[] equipLvStr = strArray[7].Split('#');
                int hp = int.Parse(strArray[8]);
                int[] equipLv = new int[6];
                for (int i = 0; i < 6; i++)
                {
                    equipLv[i] = int.Parse(equipLvStr[i]);
                }
                GameRoot.ShowHint("升级成功");
                MainGameSys.instance.SetIntensifyTask(true);
                MainGameSys.instance.UpdateIntensify(strength, intelligence, physicalPower, agility, attackNum, coin, cryal, equipLv,hp);
                if (MainGameSys.instance.mCurrentRole.intensifyNum < 5)
                {
                    MainGameSys.instance.mCurrentRole.intensifyNum++;
                    string Senddata = string.Format("{0}-{1}-{2}-{3}-{4}", MainGameSys.instance.mCurrentRole.combartNum.ToString()
                        , MainGameSys.instance.mCurrentRole.intensifyNum.ToString()
                        , MainGameSys.instance.mCurrentRole.killMOBSNum.ToString()
                        , MainGameSys.instance.mCurrentRole.worldTalkNum.ToString()
                        , MainGameSys.instance.mCurrentRole.killBossNum.ToString());
                    NetSvc.instance.SendSys(GameSys.任务, MethodController.DailyTask, Senddata);
                }
                

            });
    }
}

