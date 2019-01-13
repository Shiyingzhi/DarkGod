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
    /// <summary>
    /// 初始化Net服务
    /// </summary>
    public void InitSvc()
    {
        instance = this;
        ClientSoc.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5684));
        ClientSoc.BeginReceive(data, 0, data.Length, SocketFlags.None, AsyncReceive, ClientSoc);
        Debug.Log("建立连接");
    }
    /// <summary>
    /// 异步接受消息
    /// </summary>
    /// <param name="ar"></param>
    private void AsyncReceive(IAsyncResult ar)
    {
        Socket clinet = ar.AsyncState as Socket;
        int DataCount = clinet.EndReceive(ar);
        Debug.Log(DataCount);
        if (DataCount > 0)
        {
            
            string str = Encoding.UTF8.GetString(data,6,DataCount);
            Debug.Log(str);
            string[] strArry = str.Split('|');
            ReturnSys LogonSys = (ReturnSys)(int.Parse(strArry[0]));
            ControllerManage.instance.SelectController(LogonSys, strArry[1]);
        }
        clinet.BeginReceive(data, 0, data.Length, SocketFlags.None, AsyncReceive, clinet);
    }
    /// <summary>
    /// 发送系统请求
    /// </summary>
    /// <param name="sys"></param>
    /// <param name="controller"></param>
    /// <param name="str"></param>
    public void SendSys(GameSys sys,MethodController controller,string str)
    {
        string strByte = string.Format("{0}|{1}|{2}", ((int)controller).ToString(), ((int)sys).ToString(), str);
        Debug.Log(strByte);
        ClientSoc.Send(Tool.GetBytes(strByte));
    }
    /// <summary>
    /// 关闭客户端
    /// </summary>
    public void CloseClient()
    {
        if (ClientSoc != null && ClientSoc.Connected)
        {

            //关闭Socket之前，首选需要把双方的Socket Shutdown掉
            ClientSoc.Shutdown(SocketShutdown.Both);

            //Shutdown掉Socket后主线程停止10ms，保证Socket的Shutdown完成
            System.Threading.Thread.Sleep(10);

            //关闭客户端Socket,清理资源
            ClientSoc.Close();

        }
    }
}
