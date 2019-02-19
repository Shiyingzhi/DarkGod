using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using DarkGodAgreement;

public class CurrentPlayerController:BaseController
{
    public CurrentPlayerController()
    {
        mCurrentReturnSys = ReturnSys.加载当前在线玩家;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
        {
            Debug.Log(data);
            string[] strArry = data.Split(',');
            int count = int.Parse(strArry[0]);
            for (int i = 1; i <= count; i++)
            {
                string[] playerPos = strArry[i].Split('*');
                int id = int.Parse(playerPos[0]);
                float x = float.Parse(playerPos[1]);
                float y = float.Parse(playerPos[2]);
                float z = float.Parse(playerPos[3]);
                Profession profession = (Profession)(int.Parse(playerPos[4]));
                GameObject player = ResSvc.intance.LoadGameObjcet(profession, false);
                player.transform.position = new Vector3(x, y, z);
                player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                Debug.Log("添加id了++++++++++++++++++++++++++++++++++++++++++");
                MainGameSys.instance.GamersDic.Add(id, player);
            }
       });
    }
}

