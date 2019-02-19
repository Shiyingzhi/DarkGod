using System;
using System.Collections.Generic;
using System.Text;

namespace MainSever
{
    class Program
    {
        static void Main(string[] args)
        {
            Server MainServer = new Server();
            Console.Write("服务器启动");
            MainServer.StarServer("127.0.0.1", 5684);
            Console.ReadKey();
        }
    }
}
