using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using DarkGodAgreement;
namespace MainSever
{
    class Client
    {
        private Server MainServer;
        private Socket MainClient;

        private byte[] Data =  new byte[1024];

        public Client(Server server,Socket client)
        {
            this.MainServer = server;
            this.MainClient = client;
        }

        public void Start()
        {
            MainClient.BeginReceive(Data, 0, Data.Length, SocketFlags.None, ClientAsyncReceive, MainClient);
        }
        public void ReceptionData()
        {
            Console.WriteLine(Encoding.UTF8.GetString(Data));
            MainServer.SendClient(this, ReturnSys.登录成功, "");
        }

        private void ClientAsyncReceive(IAsyncResult ar)
        {
            Socket Client = ar.AsyncState as Socket;
            int DataCount = Client.EndReceive(ar);
            if (DataCount > 0)
            {
                ReceptionData();
                Start();
            }
            else
            {
                MainServer.CloseClient(this);
            }
            
        }

        public void Close()
        {
            Console.WriteLine("一个客户端断开连接");
            MainClient.Close();
        }
        //接受那个控制器的那个方法名，返回结果，明天来改
        public void SendMassageSys(ReturnSys RetSys, string str)
        {
            string Returndata = string.Format("{0}|{1}", ((int)RetSys).ToString(), str);
            Data = Encoding.UTF8.GetBytes(Returndata);
            MainClient.Send(Data);
        }
    }
}
