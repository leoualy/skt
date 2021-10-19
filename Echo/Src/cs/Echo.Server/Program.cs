using System;

namespace Echo.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpServer server = new TcpServer();
            server.Start(8800);
        }
    }
}
