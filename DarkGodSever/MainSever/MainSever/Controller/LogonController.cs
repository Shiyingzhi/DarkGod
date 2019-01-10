using System;
using System.Collections.Generic;
using System.Text;
using MainSever.MySql;
using DarkGodAgreement;
namespace MainSever.Controller
{
    public class LogonController:BaseController
    {
        public void LogonGame(Client client,string data)
        {
            string[] userData = data.Split(',');
            Console.WriteLine("用户："+ userData[0]+"登录游戏");
            ReturnSys sys = MySqlTool.instance.FindUser(client.Server,client,userData[0],userData[1]);
            if (client.User == null)
            {
                client.SendMassageSys(sys, "");
            }
            else
            {
                client.SendMassageSys(sys, client.User.id.ToString());
            }
            
        }

    }
}
