using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Tool
{
    public static byte[] GetBytes(string str)
    {
        byte[] Date = Encoding.UTF8.GetBytes(str);//将参数转换为byte[]
        int LengthDate = Date.Length;//获取Date的长度
        byte[] LengthByte = BitConverter.GetBytes(LengthDate);//将Date长度转换为Byte[]
        byte[] NewDate = LengthByte.Concat(Date).ToArray();//LentthByte和Date相连
        return NewDate;//返回相连byte[];

    }
}

