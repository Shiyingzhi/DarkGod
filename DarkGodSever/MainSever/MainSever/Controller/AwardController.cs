using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkGodAgreement;
namespace MainSever.Controller
{
    public class AwardController:BaseController
    {
        public void GetAward(Client client, string data)
        {
            string[] strArry = data.Split('*');
            int exp = int.Parse(strArry[0]);
            int coin = int.Parse(strArry[1]);
            int crystal = int.Parse(strArry[2]);
            if (!client.SetExpandCoin(exp, coin, crystal))
            {
                string Returndata = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-", client.CurrentRole.lv.ToString(),
                client.CurrentRole.exp.ToString(), client.CurrentRole.currentExp.ToString(), client.CurrentRole.guideID.ToString(), client.CurrentRole.coin.ToString(), client.CurrentRole.crystal.ToString());
                client.SendMassageSys(ReturnSys.完成任务, Returndata);
            }
            else
            {
                string Returndata = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-{9}-{10}-{11}-", client.CurrentRole.lv.ToString(),
                client.CurrentRole.exp.ToString(), client.CurrentRole.currentExp.ToString(), client.CurrentRole.guideID.ToString()
                , client.CurrentRole.strength.ToString(), client.CurrentRole.intelligence.ToString(), client.CurrentRole.physicalPower.ToString()
                , client.CurrentRole.agility.ToString(), client.CurrentRole.attackNum.ToString(), client.CurrentRole.coin.ToString(), client.CurrentRole.crystal.ToString(),
                client.CurrentRole.hp.ToString());
                Console.WriteLine(Returndata);
                client.SendMassageSys(ReturnSys.等级提升, Returndata);
            }
        }
    }
}
