using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DarkGodAgreement;
using MainSever.User;
using MainSever.Tool;
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
        /// <summary>
        /// 注册用户
        /// </summary>
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
        /// <summary>
        /// 查找用户
        /// </summary>
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
                    string name = date.GetString("userid");
                    //string paw = date.GetString("password");
                    bool isLogon = server.AddUserId(id);
                    if (isLogon)
                    {
                        date.Close();
                        return ReturnSys.账号已登录;
                    }
                    else
                    {
                        client.User = new UserDAO(id, name);
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
        /// <summary>
        /// 查找角色
        /// </summary>
        public void FindRole(Server server, Client client, string userid)
        {
            MySqlDataReader data = null;
            try
            {
                MySqlCommand com = new MySqlCommand("select * from role where userid=@userid", conn);
                com.Parameters.AddWithValue("userid", userid);
                data = com.ExecuteReader();
                while (data.HasRows)
                {
                    if (data.Read())
                    {
                        int RoleId = data.GetInt32("RoleId");
                        string PlayerName = data.GetString("PlayerName");
                        int professionNum = data.GetInt32("Profession");
                        Profession profession = (Profession)professionNum;
                        int lv = data.GetInt32("lv");
                        int exp = data.GetInt32("Exp");
                        int strength = data.GetInt32("Strength");
                        int intelligence = data.GetInt32("Intelligence");
                        int physicalPower = data.GetInt32("PhysicalPower");
                        int agility = data.GetInt32("Agility");
                        int tired = data.GetInt32("Tired");
                        int currentExp = data.GetInt32("CurrentExp");
                        int currentTired = data.GetInt32("CurrentTired");
                        int attackNum = data.GetInt32("AttackNum");
                        int guideID = data.GetInt32("GuideID");
                        int coin = data.GetInt32("Coin");
                        int hp = data.GetInt32("Hp");
                        int crystal = data.GetInt32("Crystal");
                        string equipLvStr = data.GetString("EquipLv");
                        string[] dataStr = equipLvStr.Split('#');
                        int[] equipLv = new int[6];
                        for (int i = 0; i < 6; i++)
                        {
                            equipLv[i] = int.Parse(dataStr[i]);
                        }
                        int combartNum = data.GetInt32("CombartNum");
                        int intensifyNum = data.GetInt32("IntensifyNum");
                        int killMOBSNum = data.GetInt32("KillMOBSNum");
                        int worldTalkNum = data.GetInt32("WorldTalkNum");
                        int killBossNum = data.GetInt32("KillBossNum");
                        RoleDAO role = new RoleDAO(userid, RoleId, profession, lv, PlayerName, exp, strength, intelligence, physicalPower, agility, tired, currentExp, currentTired, attackNum, guideID, coin, hp, crystal, equipLv, equipLvStr
                            ,combartNum,intensifyNum,killMOBSNum,worldTalkNum,killBossNum);
                        if (client.RoleList.Contains(role))
                        { 
                            Console.WriteLine("已经存在无需添加");
                        }
                        else
                        {
                            Console.WriteLine("添加");
                            client.RoleList.Add(role);
                        }                       
                    }
                    else
                    {
                        break;
                    }
                    
                }
                data.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                data.Close();
            }
        }

        public RoleDAO FoundRole(Server server, Client client, string userid, int roleid, string playerName, Profession profession)
        {
            try
            {
                int strength = 0;
                int intelligence = 0;
                int physicalPower = 0;
                int agility  = 0;
                int tired  = 0;
                int currentExp = 0;
                int currentTired = 0;
                int attackNum = 0;
                switch (profession)
                {
                    case Profession.Null:
                        break;
                    case Profession.暗影刺客:
                        strength = 165;
                        intelligence = 78;
                        physicalPower = 103;
                        agility = 120;
                        tired = 150;
                        currentExp = 0;
                        currentTired = 150;
                        attackNum = ServerXmlCfg.instance.AttackCost(1, strength, intelligence, physicalPower, agility);
                        break;
                }
                int hp = 1*1500 + agility * 20;
                MySqlCommand com = new MySqlCommand("insert into role set RoleId =@roleid, Profession =@profession, lv = 1, PlayerName = @playerName, userid=@userid, Strength= @strength, Intelligence =@intelligence, PhysicalPower= @physicalPower, Agility =@agilit, Tired =@tired, CurrentExp =@currentExp, CurrentTired =@currentTired, AttackNum =@attackNum , Hp=@Hp , EquipLv=@equipLv", conn);
                com.Parameters.AddWithValue("roleid", roleid);
                com.Parameters.AddWithValue("profession", profession);
                com.Parameters.AddWithValue("playerName", playerName);
                com.Parameters.AddWithValue("userid", userid);
                com.Parameters.AddWithValue("strength", strength);
                com.Parameters.AddWithValue("intelligence", intelligence);
                com.Parameters.AddWithValue("physicalPower", physicalPower);
                com.Parameters.AddWithValue("agilit", agility);
                com.Parameters.AddWithValue("tired", tired);
                com.Parameters.AddWithValue("currentExp", currentExp);
                com.Parameters.AddWithValue("currentTired", currentTired);
                com.Parameters.AddWithValue("attackNum", attackNum);
                com.Parameters.AddWithValue("Hp", hp);
                com.Parameters.AddWithValue("equipLv", "0#0#0#0#0#0#");
                int count = com.ExecuteNonQuery();
                Console.WriteLine(count);
                if (count > 0)
                {
                    Console.WriteLine("成功了");
                    Console.WriteLine(tired + "/" + currentTired);
                    RoleDAO role = new RoleDAO(userid, roleid, profession, 1, playerName, 1000, strength, intelligence, physicalPower, agility, tired, currentExp, currentTired, attackNum, 1001, 0, hp, 0, new int[6], "0#0#0#0#0#0#");
                    client.RoleList.Add(role);
                    return role;
                }
                else
                {
                    Console.WriteLine("失败了");
                    return null;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        /// <summary>
        /// 更新等级经验
        /// </summary>
        public void UpdateLv(int exp, int currentExp, int lv, int coin, string userid, int roleid, int attackNum, int crystal, int hp =0,int strength = 0, int intelligence = 0, int physicalPower = 0, int agility = 0)
        {
            try
            {
                MySqlCommand com = null;
                if (lv == 0)
                {
                    com = new MySqlCommand("update role set Exp =@exp ,CurrentExp =@currentexp ,Coin =@coin,Crystal =@crystal where userid = @userid and RoleId =@roleid", conn);
                    com.Parameters.AddWithValue("exp", exp);
                    com.Parameters.AddWithValue("currentexp", currentExp);
                    com.Parameters.AddWithValue("userid", userid);
                    com.Parameters.AddWithValue("roleid", roleid);
                    com.Parameters.AddWithValue("coin", coin);
                    com.Parameters.AddWithValue("crystal", crystal);
                    com.ExecuteNonQuery();
                }
                else
                {
                    com = new MySqlCommand("update role set Exp =@exp ,CurrentExp =@currentexp ,lv= @lv ,Strength=@strength ,Intelligence=@intelligence ,PhysicalPower=@physicalPower ,Agility=@agility ,AttackNum =@attackNum ,Coin =@coin,Crystal =@crystal ,Hp=@hp  where userid = @userid and RoleId =@roleid", conn);
                    com.Parameters.AddWithValue("exp", exp);
                    com.Parameters.AddWithValue("currentexp", currentExp);
                    com.Parameters.AddWithValue("lv", lv);
                    com.Parameters.AddWithValue("userid", userid);
                    com.Parameters.AddWithValue("roleid", roleid);
                    com.Parameters.AddWithValue("strength", strength);
                    com.Parameters.AddWithValue("intelligence", intelligence);
                    com.Parameters.AddWithValue("physicalPower", physicalPower);
                    com.Parameters.AddWithValue("agility", agility);
                    com.Parameters.AddWithValue("attackNum", attackNum);
                    com.Parameters.AddWithValue("coin", coin);
                    com.Parameters.AddWithValue("crystal", crystal);
                    com.Parameters.AddWithValue("hp", hp);
                    com.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            

            
            
        }
        /// <summary>
        /// 更新任务
        /// </summary>
        public void UpdateTask(int guideID,string userid,int roleid)
        {
            try
            {
                MySqlCommand com = new MySqlCommand("update role set GuideID =@guideID  where userid = @userid and RoleId =@roleid", conn);
                com.Parameters.AddWithValue("guideID", guideID);
                com.Parameters.AddWithValue("userid", userid);
                com.Parameters.AddWithValue("roleid", roleid);
                com.ExecuteNonQuery();
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// 更新强化属性
        /// </summary>
        public void UpdateIntensify( string userid,int roleid, int strength,int intelligence,int physicalPower,int agility, int attackNum, int coin, int hp, int crystal, string equipLv)
        {
            try
            {
                MySqlCommand com = new MySqlCommand("update role set Strength =@strength ,Intelligence =@intelligence ,PhysicalPower= @physicalPower ,Agility=@agility ,AttackNum=@attackNum ,Coin =@coin ,Hp =@hp,Crystal =@crystal,EquipLv =@equipLv where userid = @userid and RoleId =@roleid", conn);
                com.Parameters.AddWithValue("userid", userid);
                com.Parameters.AddWithValue("roleid", roleid);
                com.Parameters.AddWithValue("strength", strength);
                com.Parameters.AddWithValue("intelligence", intelligence);
                com.Parameters.AddWithValue("physicalPower", physicalPower);
                com.Parameters.AddWithValue("agility", agility);
                com.Parameters.AddWithValue("attackNum", attackNum);
                com.Parameters.AddWithValue("coin", coin);
                com.Parameters.AddWithValue("hp", hp);
                com.Parameters.AddWithValue("crystal", crystal);
                com.Parameters.AddWithValue("equipLv", equipLv);
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        public void UpdateDailyTask(string userid, int roleid, int combartNum, int intensifyNum, int killMOBSNum, int worldTalkNum, int killBossNum)
        {
            try
            {
                MySqlCommand com = new MySqlCommand("update role set CombartNum =@combartNum ,IntensifyNum =@intensifyNum ,KillMOBSNum= @killMOBSNum ,WorldTalkNum=@worldTalkNum ,KillBossNum=@killBossNum where userid = @userid and RoleId =@roleid", conn);
                com.Parameters.AddWithValue("userid", userid);
                com.Parameters.AddWithValue("roleid", roleid);
                com.Parameters.AddWithValue("combartNum", combartNum);
                com.Parameters.AddWithValue("intensifyNum", intensifyNum);
                com.Parameters.AddWithValue("killMOBSNum", killMOBSNum);
                com.Parameters.AddWithValue("worldTalkNum", worldTalkNum);
                com.Parameters.AddWithValue("killBossNum", killBossNum);
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
