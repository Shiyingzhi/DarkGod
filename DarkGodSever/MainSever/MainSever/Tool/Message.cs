using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DarkGodAgreement;
namespace MainSever.Tool
{
    class Message
    {
        
        byte[] Date = new byte[1024];
        int DateLingth = 0;
        
        public byte[] date
        {
            get { return Date; }
        }
        public int dateLingth
        {
            get { return DateLingth; }
            set { DateLingth = value; }
        }
        public int dateSize
        {
            get { return Date.Length - DateLingth; }
        }
        public void ReadMessage(int Aount,Action<GameSys,MethodController,string> porocessDatacallback)
        {
            DateLingth += Aount;
            while (true)
            {
                if (dateLingth <= 4) return;
                int count = BitConverter.ToInt32(date, 0);
                if (dateLingth - 4 >= count)
                {
                    string str = Encoding.UTF8.GetString(date, 4, count);
                    //Console.WriteLine("分割前的数据"+str);
                    string[] strArray = str.Split('|');
                    GameSys gameSys = (GameSys)int.Parse(strArray[0]);
                    MethodController Controller = (MethodController)int.Parse(strArray[1]);
                    porocessDatacallback(gameSys, Controller, strArray[2]);
                    Array.Copy(date, count + 4, date, 0, dateLingth - 4 - count);
                    dateLingth -= (count + 4);

                }
                else
                {
                    break;
                }
                
            }

        }
        public static byte[] PackData(ReturnSys sys, string data)
        {
            //Console.WriteLine("返回给客户端的数据"+sys.ToString()+"||" +data);

            string retSys = ((int)sys).ToString();
            string dataStr = string.Format("字节{0}|{1}", retSys, data);
            byte[] Retdata = Encoding.UTF8.GetBytes(dataStr);
            return Retdata;
            //byte[] requsetByte = BitConverter.GetBytes((int)sys);
            //byte[] dataByte = Encoding.UTF8.GetBytes(data);
            //int dataAmount = requsetByte.Length + dataByte.Length;
            //byte[] dataAmountByte = BitConverter.GetBytes(dataAmount);
            //byte[] NewBytes = dataAmountByte.Concat(requsetByte).ToArray<byte>();  //.Concat(dataByte) as byte[];
            //return NewBytes.Concat(dataByte).ToArray<byte>();
        }
        
    }

}
