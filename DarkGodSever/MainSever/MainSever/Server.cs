﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using DarkGodAgreement;
using MainSever.Controller;
using MainSever.MySql;
using MainSever.User;
namespace MainSever
{
    public class Server
    {
        Socket ServerSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public List<Client> ClientList = new List<Client>();
        ManageController MainController = new ManageController();
        List<int> userIdList = new List<int>();
        public int PlayerCount { get; set; }
        /// <summary>
        /// 开启服务器
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void StarServer(string ip, int port)
        {
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);
            ServerSoc.Bind(ipEnd);
            ServerSoc.Listen(10);
            ServerSoc.BeginAccept(AcceptAsyncCallBack, ServerSoc);
        }
        /// <summary>
        /// 异步接受客户端连接
        /// </summary>
        /// <param name="ar"></param>
        private void AcceptAsyncCallBack(IAsyncResult ar)
        {
            Socket Server = ar.AsyncState as Socket;
            Socket ClientSoc = Server.EndAccept(ar);
            Client ConnectClinet = new Client(this,ClientSoc);
            ClientList.Add(ConnectClinet);
            foreach (Client item in ClientList)
            {
                item.ClientList = ClientList;
            }
            Console.Write("当前在线人数:");
            Console.WriteLine(ClientList.Count);
            //SendClient(ConnectClinet, ReturnSys.登录成功, "登录成功");
            ConnectClinet.Start();
            ServerSoc.BeginAccept(AcceptAsyncCallBack, ServerSoc);
        }
        /// <summary>
        /// 向客户端发送消息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="str"></param>
        //public void SendClient(Client client,string str)
        //{
        //    client.SendMassageSys(str);
        //}
        /// <summary>
        /// 关闭客户端
        /// </summary>
        /// <param name="client"></param>
        public void CloseClient(Client client)
        {
            ClientList.Remove(client);
            if (client.User != null) 
                userIdList.Remove(client.User.id);
            Console.Write("当前在线人数:");
            Console.WriteLine(ClientList.Count);
            PlayerCount--;
            if (client != null && client.ClientSocket.Connected)
            {
                client.ClientSocket.Shutdown(SocketShutdown.Both);
                System.Threading.Thread.Sleep(10);
                client.Close();
            }
            
        }
        /// <summary>
        /// 将接受数据传递给控制器
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sys"></param>
        /// <param name="controller"></param>
        /// <param name="data"></param>
        public void ReceiveMessage(Client client ,GameSys sys,MethodController controller,string data)
        {
            MainController.SelectController(client,sys,controller,data);
        }

        /// <summary>
        /// 用户进入服务器
        /// </summary>
        /// <param name="id"></param>
        public bool AddUserId(int id)
        {
            if (userIdList.Contains(id))
            {
                return true;
            }
            else
            {
                userIdList.Add(id);
                return false;
            }
            
        }

        public void SendAllClient(Client client,string data,ReturnSys sys)
        {
            for (int i = 0; i < ClientList.Count; i++)
            {
                if (ClientList[i] == client)
                {
                    Console.WriteLine("自己客户端不发送消息");
                }
                else
                {
                    Console.WriteLine("发送给所有客户端命令" + sys.ToString());
                    ClientList[i].SendMassageSys(sys, data);
                }
            }
        }

    }
}
