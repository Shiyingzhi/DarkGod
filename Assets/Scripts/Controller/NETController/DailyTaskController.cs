using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;

public class DailyTaskController : BaseController {

    public DailyTaskController() 
    {
        mCurrentReturnSys = ReturnSys.完成每日任务;
    }

    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
        {
            Debug.Log(data); 
            string[] strArry = data.Split('-');
            MainGameSys.instance.mCurrentRole.combartNum = int.Parse(strArry[0]);
            MainGameSys.instance.mCurrentRole.intensifyNum = int.Parse(strArry[1]);
            MainGameSys.instance.mCurrentRole.killMOBSNum = int.Parse(strArry[2]);
            MainGameSys.instance.mCurrentRole.worldTalkNum = int.Parse(strArry[3]);
            MainGameSys.instance.mCurrentRole.killBossNum = int.Parse(strArry[4]);
            MainGameSys.instance.UpdateTaskWin();
            
        });
    }
}
