using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;
public class PlayerMoveStopController : BaseController {

    public PlayerMoveStopController()
    {
        mCurrentReturnSys = ReturnSys.停止移动;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeDataSyncMove(sys, () =>
        {
            string[] strArry = data.Split(',');
            int id = int.Parse(strArry[0]);
            GameObject player = MainGameSys.instance.GetPlayerGameObject(id);
            if (player == null) return;
            player.GetComponent<PlayerController>().SetBlend(0);
        });
    }
}
