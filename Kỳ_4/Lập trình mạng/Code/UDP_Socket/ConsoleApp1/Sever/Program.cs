using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Sever
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            IPAddress iPAddress = IPAddress.Any;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 2023);
            Socket socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket_server.Bind(iPEndPoint);
            Console.WriteLine("Server {0} waiting...", iPEndPoint);
            IPEndPoint remoteIpendpoint = new IPEndPoint(IPAddress.Any, 0);
            EndPoint endpoint = (EndPoint)remoteIpendpoint;
            byte[] data = new byte[1024];
            int recv = socket_server.ReceiveFrom(data, ref endpoint);
            string s = Encoding.Unicode.GetString(data, 0, recv);
            // in Thông Báo địa chỉ của clent kết nối
            Console.WriteLine("Nhận Kết Nối Từ: {0}", s);
            data = Encoding.Unicode.GetBytes("Xin chào client");
            socket_server.SendTo(data, endpoint);
            while (true)
            {
                data = new byte[1024];
                recv = socket_server.ReceiveFrom(data, ref endpoint);
                s = Encoding.Unicode.GetString(data, 0, recv);
                if (s.ToUpper().Equals("QUIT"))
                {
                    break;
                }
                Console.WriteLine(s);
                data = new byte[1024];
                data = Encoding.Unicode.GetBytes(s.ToUpper());
                socket_server.SendTo(data, 0, data.Length, SocketFlags.None, endpoint);
            }
            Console.ReadKey();
            socket_server.Close();
        }
    }
}
