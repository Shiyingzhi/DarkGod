using System;
using System.Collections.Generic;
using System.Text;
using MainSever.MySql;
using DarkGodAgreement;

namespace MainSever.Controller
{
    public class RegisterController:BaseController
    {
        public void Register(Client client, string data)
        {
            string[] userData = data.Split(',');
            Console.WriteLine("新注册用户");
            Console.WriteLine("用户："+ userData[0] + "||" +"密码："+ userData[1]);
            ReturnSys sys = MySqlTool.instance.AddUser(userData[0], userData[1]);
            client.SendMassageSys(sys, "");
        }
    }
}
