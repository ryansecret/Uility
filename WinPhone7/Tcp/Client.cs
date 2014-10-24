using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace Client
{
    //客户类
    class Client
    {
        static byte[] EncodingASCII(string buf)
        {
            byte[] data = Encoding.ASCII.GetBytes(buf + "\r\n");
            return data;
        }

        static void Main(string[] args)
        {
            string m_servername = "127.0.0.1";
            string m_username = "Client";
            int m_port = 5555;
            TcpClient m_client;
            bool rt = false;
            string m_sendData, m_returnData;
            byte[] data;
            StreamReader rs;
            NetworkStream ws;
            if (args.Length < 3)
            {
                Console.WriteLine("Usage:Client UserName ServerName Port");
            }
            else
            {
                try//获取命令行参数
                {
                    m_username = args[0].ToString();
                    m_servername = args[1].ToString();
                    m_port = int.Parse(args[2].ToString());
                    rt = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Parameter Error:" + ex.Message);
                }
            }
            if (rt)
            {
                try
                {
                    //创建对象并提出连接请求
                    m_client = new TcpClient();
                    m_client.Connect(m_servername, m_port);
                    rs = new StreamReader(m_client.GetStream());
                    ws = m_client.GetStream();
                    //发送用户名
                    m_sendData = m_username;
                    data = EncodingASCII(m_sendData);
                    ws.Write(data, 0, data.Length);

                    while (true)
                    {
                        //获取返回信息并显示
                        m_returnData = rs.ReadToEnd();
                        
                        Console.WriteLine(m_returnData);

                        //发送命令或其它信息
                        Console.WriteLine("Input data[GETDATE|QOIT|Other]:");
                        m_sendData = Console.ReadLine();
                        if (m_sendData.IndexOf("QUIT") > -1)
                        {
                            m_sendData = "QUIT";
                        }
                        else if (m_sendData.IndexOf("GETDATE") > -1)
                        {
                            m_sendData = "GETDATE";
                        }
                        data = EncodingASCII(m_sendData);
                        ws.Write(data, 0, data.Length);
                        if (m_sendData.IndexOf("QUIT") > -1)
                            break;
                    }
                    m_client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception" + ex.Message);
                }
            }
        }
    }
}