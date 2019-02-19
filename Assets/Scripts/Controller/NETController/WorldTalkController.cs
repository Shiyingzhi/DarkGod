using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkGodAgreement;

public class WorldTalkController : BaseController {
    public WorldTalkController()
    {
        mCurrentReturnSys = ReturnSys.世界聊天;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
        {
            string[] talk = data.Split('*');
            MainGameSys.instance.UpdateWorldTalk(talk[0]);
        });
    }
}
