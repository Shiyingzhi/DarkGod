using System;
using System.Collections.Generic;
using System.Text;
using MainSever.MySql;
using DarkGodAgreement;
using MainSever.User;
using System.Timers;
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
                string Retrundata = string.Format("{0},{1}",client.User.id.ToString(),client.User.userid);
                client.SendMassageSys(sys, Retrundata);
            }
            
        }

        public void FindRole(Client client, string data)
        {
            MySqlTool.instance.FindRole(client.Server, client, data);
            string returnStr = "";
            int RoleCount = 0;
            foreach (var item in client.RoleList)
            {
                if (item.playerName ==null) return;
                RoleCount++;
                Console.WriteLine(item.currentTired.ToString());
                string role = string.Format("/{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}-{11}-{12}-{13}-{14}-{15}-{16}-{17}-{18}-{19}-{20}-{21}-{22}-", item.roleid.ToString(),
                    item.playerName, ((int)(item.profession)).ToString(), item.lv.ToString(), item.exp.ToString(), item.strength.ToString()
                    , item.intelligence.ToString(), item.physicalPower.ToString(), item.agility.ToString(), item.tired.ToString()
                    ,item.currentExp.ToString(),item.currentTired.ToString(),item.attackNum.ToString(),item.guideID.ToString(),item.coin.ToString(),
                    item.hp.ToString(), item.crystal.ToString(), item.equipLvStr
                    , item.combartNum.ToString(), item.intensifyNum.ToString(), item.killMOBSNum.ToString(), item.worldTalkNum.ToString(), item.killBossNum.ToString());
                returnStr += role;
            }
            if (returnStr == "")
            {
                Console.WriteLine("没有角色");
                client.SendMassageSys(ReturnSys.选择角色,"没有角色");
            }
            else
            {
                client.SendMassageSys(ReturnSys.选择角色, RoleCount.ToString()+returnStr);
            }
            
        }

        public void FoundRole(Client client, string data)
        {
            string[] strArry = data.Split('-');
            string roleid = strArry[0];
            Profession profession = (Profession)int.Parse(strArry[1]);
            string playerName = strArry[2];
            string userid = strArry[3];
            RoleDAO role = MySqlTool.instance.FoundRole(client.Server, client, userid, int.Parse(roleid), playerName, profession);
            if (role!=null)
            {
                string roleStr = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}-{11}-{12}-{13}-{14}-{15}-{16}-{17}-", role.roleid.ToString(),
                    role.playerName, ((int)(role.profession)).ToString(), role.lv.ToString(), role.exp.ToString(), role.strength.ToString()
                    ,role.intelligence.ToString(),role.physicalPower.ToString(),role.agility.ToString(),role.tired.ToString()
                    , role.currentExp.ToString(), role.currentTired.ToString(), role.attackNum.ToString(),role.guideID.ToString(),role.coin.ToString(),
                    role.hp.ToString(),role.crystal.ToString(),role.equipLvStr);
                client.SendMassageSys(ReturnSys.创建成功, roleStr);
            }
            else
            {
                client.SendMassageSys(ReturnSys.创建失败, "");
            }
        }

        public void SetRole(Client client, string data)
        {
            Console.WriteLine("设置角色");
            client.SetRole(int.Parse(data));
        }
        
        
        

    }
}
