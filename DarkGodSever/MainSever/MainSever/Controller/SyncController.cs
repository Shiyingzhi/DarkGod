using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainSever.User;
using DarkGodAgreement;
namespace MainSever.Controller
{
    public class SyncController:BaseController
    {
        //进入游戏位置
        public void SyncLoadRole(Client client, string data)
        {
            string RetrunData = client.Server.PlayerCount.ToString()+",";
            foreach (Client item in client.Server.ClientList)
            {
                if (client == item||item.User == null||item.Vector3 ==null) continue;
                RetrunData += string.Format("{0}*{1}*{2}*{3}*{4},", item.User.id.ToString(), item.Vector3.positionX.ToString(), item.Vector3.positionY.ToString(), item.Vector3.positionZ.ToString(), ((int)item.CurrentRole.profession).ToString());
            }
            client.SendMassageSys(ReturnSys.加载当前在线玩家, RetrunData);
            client.Server.PlayerCount++;
        }
        //位置同步
        public void SyncMove(Client client, string data)
        {
            string[] strArry = data.Split('*');
            float positionX = float.Parse(strArry[0]);
            float positionY = float.Parse(strArry[1]);
            float positionZ = float.Parse(strArry[2]);
            float RotationX = float.Parse(strArry[3]);
            float RotationY = float.Parse(strArry[4]);
            float RotationZ = float.Parse(strArry[5]);
            client.Vector3.positionX = positionX;
            client.Vector3.positionY = positionY;
            client.Vector3.positionZ = positionZ;
            client.Vector3.RotationX = RotationX;
            client.Vector3.RotationY = RotationY;
            client.Vector3.RotationZ = RotationZ;
            string Retrundata = string.Format("{0}*{1}*{2}*{3}*{4}*{5}*{6}*", client.User.id.ToString(),
            client.Vector3.positionX.ToString(), client.Vector3.positionY.ToString(), client.Vector3.positionZ.ToString(),
            client.Vector3.RotationX.ToString(), client.Vector3.RotationY.ToString(), client.Vector3.RotationZ.ToString());
            client.SendAllClient(client, Retrundata, ReturnSys.主城移动同步);
        }
        public void JoinCity(Client client, string data)
        {
            string[] strArry = data.Split(',');
            string[] strVector3 = strArry[2].Split('*');
            float x = float.Parse(strVector3[0]);
            float y = float.Parse(strVector3[1]);
            float z = float.Parse(strVector3[2]);
            client.Vector3 = new Vector3(x, y, z, 0, 0, 0);
            client.Server.SendAllClient(client, data, ReturnSys.新玩家加入);
        }

        public void JoinCombart(Client client, string data)
        {
            client.Server.PlayerCount--;
            client.Server.SendAllClient(client, data, ReturnSys.玩家进入副本);
        }
        public void SyncMoveStop(Client client, string data)
        {
            client.Server.SendAllClient(client, client.User.id.ToString()+",", ReturnSys.停止移动);
        }


    }
}
