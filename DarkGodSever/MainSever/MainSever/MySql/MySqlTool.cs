using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DarkGodAgreement;
using MainSever.User;
namespace MainSever.MySql
{
    public class MySqlTool
    {
        MySqlConnection conn;
        public MySqlTool()
        {
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=root; database=darkgoduser;";
            conn = new MySqlConnection(connetStr);
            conn.Open();
        }
        private static MySqlTool _instance;
        public static MySqlTool instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new MySqlTool();
                }
                return _instance;
            }
        }

        public ReturnSys AddUser(string userid,string password)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into idandpassword set userid=@un,password=@pwd", conn);
                cmd.Parameters.AddWithValue("@un", userid);
                cmd.Parameters.AddWithValue("@pwd", password);
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    Console.WriteLine("成功注册");
                    return ReturnSys.注册成功;
                }
                else
                {
                    Console.WriteLine("注册失败");
                    return ReturnSys.注册失败;
                }
            }catch(MySqlException e)
            {
                Console.WriteLine("注册失败");
                return ReturnSys.注册失败;
            }
            
        }

        public ReturnSys FindUser(Server server,Client client, string userid, string password)
        {
            //mysqlcommand =数据库命令，第一个参数是命令语句，第二个参数是发送到的数据库（mysqlconnection）；
            try
            {
                MySqlCommand com = new MySqlCommand("select * from idandpassword where userid=@userid and password=@password;", conn);
                com.Parameters.AddWithValue("userid", userid);
                com.Parameters.AddWithValue("password", password);
                //接受数据库语句执行后返回的的信息流
                MySqlDataReader date = com.ExecuteReader();
                //判断流里是否有数据
                if (date.HasRows)
                {
                    //使用流查询一条数据
                    date.Read();
                    int id = date.GetInt32("id");
                    //string name = date.GetString("userid");
                    //string paw = date.GetString("password");
                    bool isLogon = server.AddUserId(id);
                    if (isLogon)
                    {
                        date.Close();
                        return ReturnSys.账号已登录;
                    }
                    else
                    {
                        client.User = new UserDAO(id);
                        date.Close();
                        return ReturnSys.登录成功;
                    }                   
                }
                else
                {
                    Console.WriteLine("账号不存在" + userid);
                    date.Close();
                    return ReturnSys.登录失败;
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return ReturnSys.登录失败;
            }
            
        }
    }
}
