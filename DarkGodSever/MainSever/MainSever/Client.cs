using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using DarkGodAgreement;
using MainSever.Tool;
using MainSever.User;
namespace MainSever
{
    public class Client
    {
        private Server MainServer;
        private Socket MainClient;
        public Socket ClientSocket { get { return MainClient; } }
        public UserDAO User { get; set; }
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
        }

        public void Start()
        {
            try
            {
                MainClient.BeginReceive(msg.date, msg.dateLingth, msg.dateSize, SocketFlags.None, ClientAsyncReceive, MainClient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }
        public void ReceptionData(GameSys sys,MethodController controller,string str)
        {
            MainServer.ReceiveMessage(this, sys, controller, str);
        }

        private void ClientAsyncReceive(IAsyncResult ar)
        {
            Socket Client = ar.AsyncState as Socket;
            int DataCount = Client.EndReceive(ar);
            msg.ReadMessage(DataCount, ReceptionData);
            Start();

            
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

            MainClient.Send(Message.PackData(RetSys, str));
        }
    }
}
