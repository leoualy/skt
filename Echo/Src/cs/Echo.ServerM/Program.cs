using System;
using Echo.Tcp;

namespace Echo.ServerM
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpSocket server = new TcpSocket();
            server.Listen(8800);
            Console.WriteLine("多客户端响应服务已启动...");
            Console.Read();
        }
    }
}
