using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Echo.Server
{
    public class TcpServer
    {

        public void Start(int port)
        {
            // 创建服务端的Socket
            m_socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 创建IP和端口节点对象
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            // 为监听socket绑定IP和端口
            m_socketServer.Bind(endPoint);
            // 把监听socket至于被动监听状态
            m_socketServer.Listen(10);
            // accept函数会在此处阻塞，直到有客户端的连接请求，根据该请求的创建新的socket
            Console.WriteLine("开始监听，等待客户端连接...");
            Socket clientSocket = m_socketServer.Accept();
            Console.WriteLine("客户端已连接，开始接收数据...");
            while (true)
            {
                // 初始化一个用于接收数据的字节数组
                byte[] buf = new byte[1024];
                // receive 会阻塞程序，直到接收到数据
                int rcvLen = clientSocket.Receive(buf);
                string msg = Encoding.UTF8.GetString(buf.Take(rcvLen).ToArray());
                Console.WriteLine($"来自客户端的数据:{msg}");
                clientSocket.Send(buf.Take(rcvLen).ToArray());
            }
        }
        
        private Socket m_socketServer;
    }
}
