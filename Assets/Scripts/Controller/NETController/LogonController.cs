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
                Console.WriteLine(data, "1");
                string[] strArry = data.Split(',');
                int id = int.Parse(strArry[0]);
                string userid = strArry[1];
                Console.WriteLine(id + "--" + userid);
                GameRoot.instance.mUser = new UserDAO(id, userid);

                LogonSys.instance.SelectRole();
            });
        
    }
    private void SelectLoadScene()
    {
        NetSvc.instance.SendSys(GameSys.登录, MethodController.Register, GameRoot.instance.mUser.name);

    }
}

