using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;

public class PlayerJoinCombartController : BaseController {

    public PlayerJoinCombartController()
    {
        mCurrentReturnSys = ReturnSys.玩家进入副本;
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
            MainGameSys.instance.GamersDic[id].gameObject.SetActive(false);
            MainGameSys.instance.GamersDic.Remove(id);
        });
    }
}
