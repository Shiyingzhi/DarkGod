using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DarkGodAgreement;

public class RegisterController:BaseController
{
    public RegisterController()
    {
        currentReturnSys = ReturnSys.注册成功;
    }
    public override void ProcessMessage(ReturnSys sys, string data)
    {
        DisposeData(sys, () =>
        {
            GameRoot.ShowHint("注册成功!");
        });
    }
}

