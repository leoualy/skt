using System;
using System.Net.Sockets;

namespace Echo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            Console.Write("按任意键发起连接请求");
            Console.ReadKey();
            Socket s= client.Connect("127.0.0.1", 8800);
            Console.WriteLine("成功连接到服务端");
            client.InputLoop(s);
        }
    }
}
