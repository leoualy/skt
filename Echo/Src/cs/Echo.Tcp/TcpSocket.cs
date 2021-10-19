using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Tcp
{
    public class TcpSocket
    {
        public void Listen(int port)
        {
            Socket s = CreateSocket();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
            s.Bind(endPoint);
            s.Listen(10);
            Console.WriteLine("开始监听，等待客户端连接...");
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Socket client = s.Accept();
                    Recv(client);
                }
            });
        }

        public Socket Connect(string ip,int port)
        {
            Socket s = CreateSocket();
            s.Connect(CreateEndPoint(port, ip));
            return s;
        }

        public void Send(Socket s, string msg)
        {
            byte[] buf = Encoding.UTF8.GetBytes(msg);
            s.Send(buf);
        }

        private void Recv(Socket s)
        {
            byte[] buf = new byte[1024];
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    int rcvLen = s.Receive(buf);
                    string msg = Encoding.UTF8.GetString(buf.Take(rcvLen).ToArray());
                    Console.WriteLine($"接收到客户端的数据:{msg}");
                    s.Send(buf.Take(rcvLen).ToArray());
                }
            });
        }

        private Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private IPEndPoint CreateEndPoint(int port,string ip = null)
        {
            return new IPEndPoint(ip == null ? IPAddress.Any : IPAddress.Parse(ip), port);
        }
        
    }
}
