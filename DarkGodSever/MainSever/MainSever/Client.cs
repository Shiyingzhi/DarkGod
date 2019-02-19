using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using DarkGodAgreement;
using MainSever.Tool;
using MainSever.User;
using System.Timers;
using MainSever.MySql;
namespace MainSever
{
    public class Client
    {
        private Server MainServer;
        private Socket MainClient;
        public Socket ClientSocket { get { return MainClient; } }
        public UserDAO User { get; set; }
        public RoleDAO CurrentRole { get; set; }
        public List<RoleDAO> RoleList = new List<RoleDAO>();
        public Vector3 Vector3;

        public List<Client> ClientList;
        //private Timer HeartbeatTimer;
        public Server Server
        {
            get { return MainServer; }
        }
        //private byte[] Data =  new byte[1024];
        private Message msg = new Message();
        public Client(Server server,Socket client)
        {
            this.MainServer = server;
            this.MainClient = client;
            //HeartbeatTimer = new Timer();
            //HeartbeatTimer.Interval = 30000;
            //HeartbeatTimer.Elapsed += HeartbeatTimer_Elapsed;
            //HeartbeatTimer.Enabled = true;
        }

        void HeartbeatTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SendMassageSys(ReturnSys.空, "");            
        }

        public void Start()
        {
            try
            {
                MainClient.BeginReceive(msg.date, msg.dateLingth, msg.dateSize, SocketFlags.None, ClientAsyncReceive, MainClient);
            }
            catch (Exception e)
            {
                Console.WriteLine("开始接受数据时出错:" +e);
            }
            
        }
        public void ReceptionData(GameSys sys,MethodController controller,string str)
        {
            MainServer.ReceiveMessage(this, sys, controller, str);
        }

        private void ClientAsyncReceive(IAsyncResult ar)
        {
            try
            {
                Socket Client = ar.AsyncState as Socket;
                int DataCount = Client.EndReceive(ar);
                if (DataCount == 0)
                {
                    MainServer.CloseClient(this);
                    return;
                }
                msg.ReadMessage(DataCount, ReceptionData);
                Start();
            }
            catch(Exception e)
            {
                Console.WriteLine("异步接受数据时发生错误:" + e);
            }
            

            
        }

        public void Close()
        {
            Console.WriteLine("一个客户端断开连接");
            MainClient.Close();
        }
        /// <summary>
        /// 返回登录命令
        /// </summary>
        /// <param name="RetSys"></param>
        /// <param name="str"></param>
        public void SendMassageSys(ReturnSys RetSys, string str)
        {
            //string Returndata = string.Format("{0}|{1}", ((int)RetSys).ToString(), str);
            byte[] data = Message.PackData(RetSys, str);
            MainClient.BeginSend(data, 0, data.Length, SocketFlags.None, ClientAsyncSend, MainClient);
        }
        private void ClientAsyncSend(IAsyncResult ar)
        {
            try
            {
                Socket Client = ar.AsyncState as Socket;
                int DataCount = Client.EndSend(ar);
                if (DataCount == 0)
                {
                    Console.WriteLine("数据没有发送成功");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("异步发送数据时发生错误:" + e);
            }
        }
        public void SetRole(int index)
        {
            CurrentRole = RoleList[index];
            if (CurrentRole == null)
            {
                Console.WriteLine("没获取到index不对");
            }
            else
            {
                Console.WriteLine(CurrentRole.strength);
                Console.WriteLine(CurrentRole.intelligence);
                Console.WriteLine(CurrentRole.physicalPower);
                Console.WriteLine(CurrentRole.agility);
                Console.WriteLine(CurrentRole.coin);
            }
        }

        public bool SetExpandCoin(int exp,int coin,int crystal = 0)
        {
            if ((CurrentRole.currentExp + exp) > CurrentRole.exp)
            {
                //设置金币数量
                CurrentRole.coin += coin;
                //设置水晶
                CurrentRole.crystal += crystal;
                //升级
                CurrentRole.currentExp = (CurrentRole.currentExp + exp) - CurrentRole.exp;
                CurrentRole.lv++;
                CurrentRole.exp = CurrentRole.lv * CurrentRole.exp + 1000 * CurrentRole.lv;
                //设置升级属性
                CurrentRole.strength += (int)(CurrentRole.strength * 1.25f);
                CurrentRole.intelligence += (int)(CurrentRole.intelligence * 1.25f);
                CurrentRole.physicalPower += (int)(CurrentRole.physicalPower * 1.25f);
                CurrentRole.agility += (int)(CurrentRole.agility * 1.25f);
                CurrentRole.attackNum = ServerXmlCfg.instance.AttackCost(1, CurrentRole.strength, CurrentRole.intelligence, CurrentRole.physicalPower, CurrentRole.agility);
                CurrentRole.hp = CurrentRole.lv * 1500 + CurrentRole.agility * 2;
                Console.WriteLine(CurrentRole.hp);
                Console.WriteLine(CurrentRole.physicalPower);
                MySqlTool.instance.UpdateLv(CurrentRole.exp, CurrentRole.currentExp, CurrentRole.lv,CurrentRole.coin,CurrentRole.userid,CurrentRole.roleid
                    ,CurrentRole.attackNum,CurrentRole.crystal,CurrentRole.hp,CurrentRole.strength, CurrentRole.intelligence, CurrentRole.physicalPower);
                return true;
            }
            else
            {
                //设置金币数量
                CurrentRole.coin += coin;
                //设置水晶
                CurrentRole.crystal += crystal;
                //设置经验
                CurrentRole.currentExp += exp;
                MySqlTool.instance.UpdateLv(CurrentRole.exp, CurrentRole.currentExp,0,CurrentRole.coin,CurrentRole.userid, CurrentRole.roleid,0,CurrentRole.crystal);
                return false;
            }
        }

        public void Perform()
        {
            CurrentRole.guideID++;
            MySqlTool.instance.UpdateTask(CurrentRole.guideID, CurrentRole.userid, CurrentRole.roleid);
        }

        public void SendAllClient(Client client, string data, ReturnSys sys)
        {
            foreach (var Client in ClientList)
            {
                if (Client == client)
                {
                    Console.WriteLine("自己客户端不发送消息");
                }
                else
                {
                    Console.WriteLine("发送给所有客户端命令" + sys.ToString());
                    Client.SendMassageSys(sys, data);
                }
            }
        }

        public void UpdateDailyTask()
        {
            MySqlTool.instance.UpdateDailyTask(CurrentRole.userid, CurrentRole.roleid, CurrentRole.combartNum, CurrentRole.intensifyNum, CurrentRole.killMOBSNum, CurrentRole.worldTalkNum, CurrentRole.killBossNum);
        }

        
    }
}
