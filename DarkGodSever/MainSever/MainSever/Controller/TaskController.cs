using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSever.Tool;
using DarkGodAgreement;

namespace MainSever.Controller
{
    public class TaskController:BaseController
    {
        public void Perform(Client client, string data)
        {
            int guideID = int.Parse(data);
            AutoGuideCfg autoGuideCfg = ServerXmlCfg.instance.GetAutoGuidCfg(guideID);
            client.Perform();
            if (!client.SetExpandCoin(autoGuideCfg.exp,autoGuideCfg.coin))
            {
                string Returndata = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-", client.CurrentRole.lv.ToString(),
                client.CurrentRole.exp.ToString(), client.CurrentRole.currentExp.ToString(), client.CurrentRole.guideID.ToString(),client.CurrentRole.coin.ToString(),client.CurrentRole.crystal.ToString());
                client.SendMassageSys(ReturnSys.完成任务, Returndata);
            }
            else
            {
                string Returndata = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}-{11}-", client.CurrentRole.lv.ToString(),
                client.CurrentRole.exp.ToString(), client.CurrentRole.currentExp.ToString(), client.CurrentRole.guideID.ToString()
                ,client.CurrentRole.strength.ToString(),client.CurrentRole.intelligence.ToString(),client.CurrentRole.physicalPower.ToString()
                ,client.CurrentRole.agility.ToString(),client.CurrentRole.attackNum.ToString(),client.CurrentRole.coin.ToString(),client.CurrentRole.crystal.ToString()
                ,client.CurrentRole.hp.ToString());
                Console.WriteLine(Returndata);
                client.SendMassageSys(ReturnSys.等级提升, Returndata);
            }

            

        }

        public void DailyTask(Client client, string data)
        {
            string[] strArry = data.Split('-');
            client.CurrentRole.combartNum = int.Parse(strArry[0]);
            client.CurrentRole.intensifyNum = int.Parse(strArry[1]);
            client.CurrentRole.killMOBSNum = int.Parse(strArry[2]);
            client.CurrentRole.worldTalkNum = int.Parse(strArry[3]);
            client.CurrentRole.killBossNum = int.Parse(strArry[4]);
            client.UpdateDailyTask();
            client.SendMassageSys(ReturnSys.完成每日任务, data+"-");
            
        }
    }
}
