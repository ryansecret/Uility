using System;
using System.Collections.Generic;
using System.Text;
namespace UDPComm
{
    public class UDPComm
    {
        public static byte[] EncodingASCII(string buf) //编码
        {
            byte[] data = Encoding.ASCII.GetBytes(buf + "\r\n");
            return data;
        }
        public static string DecodingASCII(byte[] buf)//解码
        {
            string st = Encoding.ASCII.GetString(buf);
            return st;
        }
    }
}