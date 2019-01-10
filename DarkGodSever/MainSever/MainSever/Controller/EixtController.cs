using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSever.Controller
{
    public class EixtController:BaseController
    {
        public void EixtGame(Client client, string data)
        {
            client.Server.CloseClient(client);
        }
    }
}
