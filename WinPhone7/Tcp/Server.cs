using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Sockets;

namespace Server
{
    //客户连接处理，用来接收或发送网络数据
    class ClientHandle
    {
        private string m_username;
        private TcpClient m_clientSocket = null;
        private string m_returnData, m_sendData;
        byte[] data;//中间变量

        public TcpClient ClientSocket//属性
        {
            get { return m_clientSocket; }
            set { m_clientSocket = value; }
        }

        byte[] EncodingASCII(string buf)
        {
            byte[] data = Encoding.ASCII.GetBytes(buf + "\r\n");
            return data;
        }

        public void ResponseClient()
        {
            if (m_clientSocket != null)
            {
                StreamReader rs = new StreamReader(m_clientSocket.GetStream());
                NetworkStream ws = m_clientSocket.GetStream();

                //获取用户名
                m_returnData = rs.ReadLine();
                m_username = m_returnData;
                m_sendData = "Welcome " + m_returnData + "to Server";
                Console.WriteLine(m_sendData);//显示信息
                //回送欢迎信息
                data = EncodingASCII(m_sendData);
                ws.Write(data, 0, data.Length);

                while (true)
                {
                    //接收信息
                    m_returnData = rs.ReadLine();

                    //解释所接收的信息
                    if (m_returnData.IndexOf("QUIT") > -1)
                    {
                        Console.WriteLine(m_username + "has quited!");
                        break;
                    }
                    else if (m_returnData.IndexOf("GETDATE") > -1)
                    {
                        m_sendData = DateTime.Now.ToString();
                    }
                    else
                    {
                        m_sendData = "-->" + m_returnData;
                    }
                    data = EncodingASCII(m_sendData);
                    ws.Write(data, 0, data.Length);
                }
                m_clientSocket.Close();
            }
        }
    }

    class Server
    {
        static void Main(string[] args)
        {
            string m_serverIP = "127.0.0.1";
            int m_port = 5555;
            bool rt = false;
            TcpListener m_Listener = null;
            IPAddress m_host;

            if (args.Length < 2)
            {
                Console.WriteLine("Usage:Server ServerIP Port");
            }
            else
            {
                try
                {
                    m_serverIP = args[0].ToString();
                    m_port = int.Parse(args[1].ToString());
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
                    m_host = IPAddress.Parse(m_serverIP);
                    m_Listener = new TcpListener(m_host, m_port);

                    m_Listener.Start();
                    Console.WriteLine("Starting to listen....");

                    while (true)
                    {
                        TcpClient m_client = m_Listener.AcceptTcpClient();
                        ClientHandle m_handle = new ClientHandle();
                        m_handle.ClientSocket = m_client;

                        Thread m_clientthread = new Thread(new ThreadStart(m_handle.ResponseClient));
                        m_clientthread.Start();
                    }
                    m_Listener.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception" + ex.Message);
                }
            }
        }
    }
}