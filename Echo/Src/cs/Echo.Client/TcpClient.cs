using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Echo.Client
{
    public class TcpClient
    {
        public Socket Connect(string ip,int port)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            s.Connect(endPoint);
            return s;
        }
        public void InputLoop(Socket s)
        {
            while (true)
            {
                Console.Write("等待输入:");
                string msg = Console.ReadLine();
                byte[] buf = Encoding.UTF8.GetBytes(msg);
                s.Send(buf);
                byte[] bufServer = new byte[1024];
                int rcvlen=s.Receive(bufServer);
                Console.WriteLine($"接收到服务端的回写:{Encoding.UTF8.GetString(bufServer.Take(rcvlen).ToArray())}");
            }
        }
        
    }
}
