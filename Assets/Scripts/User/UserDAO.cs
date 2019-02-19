using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UserDAO
{
    public int id { get; set; }
    public string name { get; set; }
    public UserDAO(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

