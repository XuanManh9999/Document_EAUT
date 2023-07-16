using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            IPAddress ipdaress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipdaress, 9999);
            Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_client.Connect(ipEndPoint);
            byte[] data = new byte[1024];
            while (true) {
                Console.WriteLine("Nhập Số Phần Từ Muốn Nhập Qua Mảng");
                int n = int.Parse(Console.ReadLine());
                int []arr = new int[n];
                for(int i = 0; i <  n; i++)
                {
                    Console.WriteLine("Nhập Vào Phần Tử Thứ {0}", i);
                    arr[i] = int.Parse(Console.ReadLine());
                }
                data = arr.SelectMany(BitConverter.GetBytes).ToArray();
                socket_client.Send(data);// Gửi Data 1
                int res = socket_client.Receive(data);// nhận dự liệu từ server
                int c = int.Parse(Encoding.UTF8.GetString(data, 0, res));
                Console.WriteLine("Nhận Dữ Liệu Từ Server: {0}", c );
            }
            socket_client.Disconnect(true);
            socket_client.Close();
        }
    }
}
