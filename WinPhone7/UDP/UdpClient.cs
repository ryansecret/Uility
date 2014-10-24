using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using UDPComm;
namespace UDPClient
{
    class UDPClient
    {
        static void Main(string[] args)
        {
            string m_hostIP = "127.0.0.1";
            int m_port = 6666;
            UdpClient m_client;
            bool rt = false;
            byte[] data;
            string m_SendData, m_ReturnData;
            IPEndPoint m_EndPoint;

            //从命令行提取服务器地址和侦听端口
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: UDPClient hostIP port");
            }
            else
            {
                m_hostIP = args[0].ToString();
                m_port = int.Parse(args[1].ToString());
                rt = true;
            }
            if (rt)
            {
                IPAddress m_ipA = IPAddress.Parse(m_hostIP);
                m_EndPoint = new IPEndPoint(m_ipA, m_port);
                m_client = new UdpClient();
                m_client.Connect(m_EndPoint);
                while (true)
                {
                    
                    Console.WriteLine("Input [ADD|DEL|REF|QUIT|Message]:");
                    m_SendData = Console.ReadLine();
                 
                    if (m_SendData.IndexOf("QUIT") > -1)//退出
                        m_SendData = "DEL";
                    if (m_SendData.IndexOf("REF") <= -1)//刷新显示
                    {
                       
                        data = UDPComm.UDPComm.EncodingASCII(m_SendData);
                        m_client.Send(data, data.Length);
                    }
                    if (m_SendData.IndexOf("QUIT") > -1)
                        break;
                    data = m_client.Receive(ref m_EndPoint);//接收数据
                    m_ReturnData = UDPComm.UDPComm.DecodingASCII(data);
                    Console.WriteLine(m_ReturnData);
                }
                //退出
                Console.WriteLine("Byte!");
                m_client.Close();
            }
        }
    }
}