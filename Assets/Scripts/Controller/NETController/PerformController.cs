using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarkGodAgreement;
using UnityEngine;

    public class PerformController:BaseController
    {
        public PerformController()
        {
            mCurrentReturnSys = ReturnSys.完成任务;
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
                int coin = int.Parse(strArry[4]);
                int crytal = int.Parse(strArry[5]);
                GameRoot.ShowHint("<color=#14FF00>" + "经验值 + " + (currentExp - MainGameSys.instance.mCurrentRole.currentExp).ToString() + "</color>");
                GameRoot.ShowHint("<color=#FBEE48>" + "金币 + " + (coin - MainGameSys.instance.mCurrentRole.coin).ToString() + "</color>");
                MainGameSys.instance.mCurrentRole.lv = lv;
                MainGameSys.instance.mCurrentRole.exp = exp;
                MainGameSys.instance.mCurrentRole.currentExp = currentExp;
                MainGameSys.instance.mCurrentRole.guideID = guideID;
                MainGameSys.instance.mCurrentRole.coin = coin;
                MainGameSys.instance.mCurrentRole.crystal = crytal;
                if (MainGameSys.instance.isCity)
                {
                    MainGameSys.instance.UpdateMainCatyInfo();
                }
                else if (CombatSys.instance.isCombat)
                {
                    MainGameSys.instance.UpdateCombatInfo();
                }
                

            });
        }
    }

