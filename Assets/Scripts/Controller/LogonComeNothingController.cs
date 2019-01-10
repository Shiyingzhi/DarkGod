using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarkGodAgreement;

public class LogonComeNothingController:BaseController
{
    public LogonComeNothingController()
    {
        currentReturnSys = ReturnSys.登录失败;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
        {
            GameRoot.ShowHint("用户名密码不正确");
        });
    }
}

