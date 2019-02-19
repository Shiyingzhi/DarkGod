using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSever.User
{
    public class UserDAO
    {
        public int id { get; set; }
        public string userid { get; set; }
        public UserDAO(int id,string userid)
        {
            this.id = id;
            this.userid = userid;
        }


    }
}
