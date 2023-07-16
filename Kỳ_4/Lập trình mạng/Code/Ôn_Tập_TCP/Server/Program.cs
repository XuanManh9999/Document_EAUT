using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Any;
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9000);
            Socket socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_server.Bind(iPEndPoint);// Giúp socket biết điểm để lắng nghe
            socket_server.Listen(10);
            Console.WriteLine("Chờ kết nối từ Client...");
            Socket socket_client = socket_server.Accept();
            Console.WriteLine("Chấp nhận kết nối từ client có ip: " + socket_client.RemoteEndPoint.ToString());
            byte[] data = new byte[1024];
            while (true)
            {
                data = new byte[1024];
                int x = socket_client.Receive(data);
                int[] arr = new int[data.Length / 4];//// Mảng int có chiều dài là 1/4 chiều dài của mảng byte
                Buffer.BlockCopy(data, 0, arr, 0, data.Length);
                int sum = 0;
                foreach (int k in arr)
                {
                    sum += k;
                }
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(sum.ToString());
                socket_client.Send(data);
            }

        }
    }
}
