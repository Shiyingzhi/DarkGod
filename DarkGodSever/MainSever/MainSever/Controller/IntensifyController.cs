using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSever.Tool;
using MainSever.MySql;
using DarkGodAgreement;
namespace MainSever.Controller
{
    public class IntensifyController:BaseController
    {
        public void IntensifyEquip(Client client, string data)
        {
            
            string[] strData = data.Split('-');
            int pos = int.Parse(strData[0]);
            Console.WriteLine("当前位置" +pos +"当前等级" + client.CurrentRole.equipLv[pos]);
            int starLv = int.Parse(strData[1]);
            client.CurrentRole.equipLv[pos] = starLv;
            switch (pos)
            {
                case 0:
                    int hp = int.Parse(strData[2]);
                    int strength = int.Parse(strData[3]);
                    int physicalPower = int.Parse(strData[4]);
                    client.CurrentRole.hp += hp;
                    client.CurrentRole.strength += strength;
                    client.CurrentRole.physicalPower += physicalPower;
                    break;
                case 1:
                    int hp2 = int.Parse(strData[2]);
                    int agility = int.Parse(strData[3]);
                    int physicalPower2 = int.Parse(strData[4]);
                    client.CurrentRole.hp += hp2;
                    client.CurrentRole.agility += agility;
                    client.CurrentRole.physicalPower += physicalPower2;
                    break;
                case 2:
                    int agility2 = int.Parse(strData[2]);
                    int intelligence = int.Parse(strData[3]);
                    int physicalPower3 = int.Parse(strData[4]);
                    client.CurrentRole.agility += agility2;
                    client.CurrentRole.intelligence += intelligence;
                    client.CurrentRole.physicalPower += physicalPower3;
                    break;
                case 3:
                    int strength2 = int.Parse(strData[2]);
                    int intelligence2 = int.Parse(strData[3]);
                    int physicalPower4 = int.Parse(strData[4]);
                    client.CurrentRole.strength += strength2;
                    client.CurrentRole.intelligence += intelligence2;
                    client.CurrentRole.physicalPower += physicalPower4;
                    break;
                case 4:
                    int strength3 = int.Parse(strData[2]);
                    int intelligence3 = int.Parse(strData[3]);
                    int physicalPower5 = int.Parse(strData[4]);
                    client.CurrentRole.strength += strength3;
                    client.CurrentRole.intelligence += intelligence3;
                    client.CurrentRole.physicalPower += physicalPower5;
                    break;
                case 5:
                    int strength4 = int.Parse(strData[2]);
                    int intelligence4 = int.Parse(strData[3]);
                    int agility3 = int.Parse(strData[4]);
                    client.CurrentRole.strength += strength4;
                    client.CurrentRole.intelligence += intelligence4;
                    client.CurrentRole.agility += agility3;
                    break;
            }
            int coin = int.Parse(strData[5]);
            client.CurrentRole.coin -= coin;
            int attackNum = ServerXmlCfg.instance.AttackCost(client.CurrentRole.lv,client.CurrentRole.strength,client.CurrentRole.intelligence,client.CurrentRole.physicalPower,client.CurrentRole.agility);
            client.CurrentRole.attackNum = attackNum;
            client.CurrentRole.equipLvStr = "";
            for (int i = 0; i < 6; i++)
            {
                client.CurrentRole.equipLvStr += client.CurrentRole.equipLv[i].ToString() + "#";
            }
            int crystal = int.Parse(strData[6]);
            client.CurrentRole.crystal -= crystal;
            //更新数据库
            MySqlTool.instance.UpdateIntensify(client.CurrentRole.userid,client.CurrentRole.roleid,client.CurrentRole.strength, client.CurrentRole.intelligence, client.CurrentRole.physicalPower, client.CurrentRole.agility, client.CurrentRole.attackNum,
                client.CurrentRole.coin, client.CurrentRole.hp, client.CurrentRole.crystal, client.CurrentRole.equipLvStr);
            //返回更新后的数据
            string returnStr = string.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}-{8}-",client.CurrentRole.strength.ToString(),client.CurrentRole.intelligence.ToString(),client.CurrentRole.physicalPower.ToString(),client.CurrentRole.agility.ToString(),client.CurrentRole.attackNum.ToString(),client.CurrentRole.coin.ToString()
                ,client.CurrentRole.crystal.ToString(),client.CurrentRole.equipLvStr,client.CurrentRole.hp.ToString());
            client.SendMassageSys(ReturnSys.强化, returnStr);
        }
    }
}
