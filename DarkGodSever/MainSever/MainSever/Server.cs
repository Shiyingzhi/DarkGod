using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using DarkGodAgreement;

namespace MainSever
{
    class Server
    {
        Socket ServerSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Client> ClientList = new List<Client>();
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
        public void SendClient(Client client,ReturnSys sys, string str)
        {
            client.SendMassageSys(sys,str);
        }
        /// <summary>
        /// 关闭客户端
        /// </summary>
        /// <param name="client"></param>
        public void CloseClient(Client client)
        {
            client.Close();
            ClientList.Remove(client);
        }
    }
}
