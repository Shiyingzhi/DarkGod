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
        mCurrentReturnSys = ReturnSys.登录成功;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        Debug.Log(data.Length);
        DisposeData(sys, () =>
            {
                //设置用户id
                GameRoot.intance.ID = int.Parse(data);
                LogonSys.instance.SelectRole();
            });
        
    }
}

