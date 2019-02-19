using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkGodAgreement;
namespace MainSever.Controller
{
    public class TalkController:BaseController
    {
        public void WorldTalk(Client client, string data)
        {
            Console.WriteLine(data);
            client.Server.SendAllClient(client, data+"*", ReturnSys.世界聊天);
        }
    }
}
