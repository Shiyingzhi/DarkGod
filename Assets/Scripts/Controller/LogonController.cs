using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DarkGodAgreement;

public class LogonController:BaseController
{
    public LogonController()
    {
        currentReturnSys = ReturnSys.登录成功;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        Debug.Log(data.Length);
        DisposeData(sys, () =>
            {
                GameRoot.intance.ID = int.Parse(data);
                Debug.Log(GameRoot.intance.ID);
                LogonSys.instance.SelectRole();
            });
        
    }
}

