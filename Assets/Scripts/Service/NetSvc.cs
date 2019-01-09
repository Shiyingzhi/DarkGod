using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using DarkGodAgreement;
using System.Text;

public class NetSvc : MonoBehaviour {

    public static NetSvc instance;
    Socket ClientSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private byte[] data = new byte[1024];
    public void InitSvc()
    {
        instance = this;
        ClientSoc.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5684));
        ClientSoc.BeginReceive(data, 0, data.Length, SocketFlags.None, AsyncReceive, ClientSoc);
        Debug.Log("建立连接");
    }
    private void AsyncReceive(IAsyncResult ar)
    {
        Socket clinet = ar.AsyncState as Socket;
        int DataCount = clinet.EndReceive(ar);
        Debug.Log(DataCount);
        if (DataCount > 0)
        {
            string str = Encoding.UTF8.GetString(data);
            string[] strArry = str.Split('|');
            ReturnSys LogonSys = (ReturnSys)(int.Parse(strArry[0]));
            switch (LogonSys)
            {
                case ReturnSys.登录失败:
                    Debug.Log("账号密码错误");
                    break;
                case ReturnSys.登录成功:
                    Debug.Log("登录成功进入游戏");
                    break;
            }
        }
        clinet.BeginReceive(data, 0, data.Length, SocketFlags.None, AsyncReceive, clinet);
    }

    public void SendSys(GameSys sys,string str)
    {
        string strByte = string.Format("{0}|{1}", ((int)sys).ToString(), str);
        byte[] SendByte = Encoding.UTF8.GetBytes(strByte);
        ClientSoc.Send(SendByte);
    }
}
