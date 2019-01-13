using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarkGodAgreement;

public class RegisterComeNothingController:BaseController
{
    public RegisterComeNothingController()
    {
        mCurrentReturnSys = ReturnSys.注册失败;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
            {
                GameRoot.ShowHint("注册失败，该用户已存在");
            });
    }
}

