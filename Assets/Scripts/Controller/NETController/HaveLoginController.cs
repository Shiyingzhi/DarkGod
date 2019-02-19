using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarkGodAgreement;

public class HaveLoginController:BaseController
{
    public HaveLoginController()
    {
        mCurrentReturnSys = ReturnSys.账号已登录;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
            {
                GameRoot.ShowHint("您的账号已经登录了");
            });
    }
}

