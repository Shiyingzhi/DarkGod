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
        public UserDAO(int id)
        {
            this.id = id;
        }

    }
}
