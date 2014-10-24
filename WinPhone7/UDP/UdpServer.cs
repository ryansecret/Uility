using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using UDPComm;
namespace UDPServer
{
    class UDPServer
    {
        static UdpClient m_server;
        static ArrayList mblist;
        static void AddMember(IPEndPoint rep)//加入组
        {
            mblist.Add(rep);
            byte[] data = UDPComm.UDPComm.EncodingASCII("OK");
            m_server.Send(data, data.Length, rep);
        }
        static void DelMember(IPEndPoint rep)//离开组
        {
            mblist.Remove(rep);
            byte[] data = UDPComm.UDPComm.EncodingASCII("OK");
            m_server.Send(data, data.Length, rep);
        }
        static void SendToMember(string buf)//组内转发数据
        {
            foreach (IPEndPoint mb in mblist)
            {
                byte[] data = UDPComm.UDPComm.EncodingASCII(buf);
                m_server.Send(data, data.Length, mb);
            }
        }
        static void Main(string[] args)
        {
            string m_hostIP = "127.0.0.1";
            int m_port = 6666;
            IPEndPoint m_EndPoint;
            ArrayList memberlist = new ArrayList();
            bool rt = false;
            byte[] data;
            string m_ReturnData;

            //从命令行提取主机IP和端口
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: UDPServer hostIP port");
            }
            else
            {
                m_hostIP = args[0].ToString();
                m_port = int.Parse(args[1].ToString());
                rt = true;
            }
            if (rt)
            {
                mblist = new ArrayList();//组成员列表
                IPAddress m_ipA = IPAddress.Parse(m_hostIP);
                m_EndPoint = new IPEndPoint(m_ipA, m_port);
                m_server = new UdpClient(m_EndPoint);
                Console.WriteLine("Ready for Connect......");
                while (true)
                {
                    data = m_server.Receive(ref m_EndPoint);//接收数据
                    m_ReturnData = UDPComm.UDPComm.DecodingASCII(data);
                    if (m_ReturnData.IndexOf("ADD") > -1)//加入组
                    {
                        AddMember(m_EndPoint);
                        Console.WriteLine(m_EndPoint.ToString() + " has added to group!");
                    }
                    else if (m_ReturnData.IndexOf("DEL") > -1)//退出组
                    {
                        DelMember(m_EndPoint);
                        Console.WriteLine(m_EndPoint.ToString() + " has deleted from group!");
                    }
                    else
                    {
                        if (mblist.Contains(m_EndPoint)) //转发数据
                        {
                            SendToMember(m_ReturnData + "[" + m_EndPoint.ToString() + "]");
                            Console.WriteLine(m_ReturnData + "[" + m_EndPoint.ToString() + "]" + " has resented to members!");
                        }
                    }
                }
                m_server.Close();
            }
        }
    }
}