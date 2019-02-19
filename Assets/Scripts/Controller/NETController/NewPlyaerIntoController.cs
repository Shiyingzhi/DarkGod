using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;

public class NewPlyaerIntoController : BaseController {
    public NewPlyaerIntoController()
    {
        mCurrentReturnSys = ReturnSys.新玩家加入;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        Debug.Log("执行这里了吗？??");
        DisposeData(sys, () =>
            {
                Debug.Log("到这里了");
                    string[] strArry = data.Split(',');
                    int id = int.Parse(strArry[0]);
                    Profession profession = (Profession)(int.Parse(strArry[1]));
                    string[] strVector3 = strArry[2].Split('*');
                    float x = float.Parse(strVector3[0]);
                    float y = float.Parse(strVector3[1]);
                    float z = float.Parse(strVector3[2]);
                    GameObject player = ResSvc.intance.LoadGameObjcet(profession, false);
                    player.transform.position = new Vector3(x, y, z);
                    player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    MainGameSys.instance.GamersDic.Add(id, player);          
            });
    }
	
}
