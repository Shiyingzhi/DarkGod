using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;
public class PlayerCityMoveController : BaseController {

    public PlayerCityMoveController()
    {
        mCurrentReturnSys = ReturnSys.主城移动同步;
    }

    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeDataSyncMove(sys, () =>
        {
            if (MainGameSys.instance.isCity)
            {
                string[] strArry = data.Split('*');
                int id = int.Parse(strArry[0]);
                float positionX = float.Parse(strArry[1]);
                float positionY = float.Parse(strArry[2]);
                float positionZ = float.Parse(strArry[3]);
                float RotationX = float.Parse(strArry[4]);
                float RotationY = float.Parse(strArry[5]);
                float RotationZ = float.Parse(strArry[6]);
                GameObject player = MainGameSys.instance.GetPlayerGameObject(id);
                if (player == null) return;
                player.GetComponent<CharacterController>().enabled = true;
                player.transform.position = new Vector3(positionX, positionY, positionZ);
                player.GetComponent<PlayerController>().SetBlend(1);
                player.transform.eulerAngles = new Vector3(RotationX, RotationY, RotationZ);
            }
        });
    }
}
