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
            MainServer.StarServer("127.0.0.1", 5684);
            Console.ReadKey();
        }
    }
}
