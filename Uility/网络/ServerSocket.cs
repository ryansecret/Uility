using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerSocket
{
    /// <summary>
    /// Socket监听服务器，用于监听客户端连接请求
    /// 作者：周公
    /// 编写时间：2009-03-18    
    /// </summary>
    public class ServerSocket : IDisposable
    {
        Socket listener = null;
        /// <summary>
        /// 开始监听指定端口
        /// </summary>
        public void StartListening(int port)
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            //以运行服务器端程序所在的机器为服务器监听客户端连接
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
             
            //创建一个TCP/IP Socket用于监听客户端连接
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            try
            {
                //先绑定要监听的主机和端口
                listener.Bind(localEndPoint);
                //再开始监听，并且指定监听队列的最大长度
                listener.Listen(10);

                //开始监听连接
                while (true)
                {
                    Console.WriteLine("等待客户端连接...");
                    //线程将一直阻塞直到有新的客户端连接
                    Socket handler = listener.Accept();
                    //启用一个新的线程用于处理客户端连接
                    //这样主线程还可以继续接受客户端连接
                    SocketThread socketThread = new SocketThread(handler);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public static int Main(String[] args)
        {
            ServerSocket server = new ServerSocket();
            server.StartListening(11000);
            return 0;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (listener != null)
            {
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
        }

        #endregion
    }

    public class SocketThread : IDisposable
    {
        private Socket socket;
        private Thread thread;
        private bool isListening = false;
        private string text;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="socket">用于处理客户端应答的Socket</param>
        public SocketThread(Socket socket)
        {
            this.socket = socket;
            isListening = true;
            thread = new Thread(new ThreadStart(Work));
            thread.Start();
        }


        public void Work()
        {
            byte[] buffer = new byte[1024];
            while (isListening)
            {
                int receivedLength = socket.Receive(buffer);
                text = System.Text.Encoding.UTF8.GetString(buffer, 0, receivedLength);
                //<EOF>是自定义的协议，表示中止消息交流
                if (text.IndexOf("<EOF>") > -1)
                {
                    isListening = false;
                    socket.Send(new byte[] {0});
                }
                else
                {
                    //Console.WriteLine("接收到的数据：" + text);
                    //根据客户端的请求获取相应的响应信息
                    string message = GetMessage(text);
                    //将响应信息以字节的方式发送到客户端
                    socket.Send(Encoding.UTF8.GetBytes(message));
                }
            }
        }

        private string GetMessage(string request)
        {
            string message = string.Empty;
            //Console.WriteLine("Message=" + request);
            switch (request)
            {
                case "date":
                    message = "服务器日期：" + DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case "time":
                    message = "服务器时间：" + DateTime.Now.ToString("HH:mm:ss");
                    break;
                case "datetime":
                    message = "服务器日期时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    break;
                case "year":
                    message = "服务器年份：" + DateTime.Now.Year.ToString();
                    break;
                case "month":
                    message = "服务器月份：" + DateTime.Now.Month.ToString();
                    break;
                case "day":
                    message = "这是本月第" + DateTime.Now.Day.ToString() + "天";
                    break;
                default:
                    message = "不正确的参数";
                    break;
            }
            return message;
        }

       

        public void Dispose()
        {
            isListening = false;
            if (thread != null)
            {
                if (thread.ThreadState != ThreadState.Aborted)
                {
                    thread.Abort();
                }
                thread = null;
            }
            if (socket != null)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}