using System;
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
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9000);
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(iPEndPoint);
            byte[] data = new byte[10000];
            while (true)
            {
                Console.WriteLine("Nhập Vào Số Phần Tử Bạn Muốn Nhập Vào Mảng.");
                int n = int.Parse(Console.ReadLine());
                int[] arr = new int[n];
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Nhập Vào Phần Tử Thứ {0}", i + 1);
                    arr[i] = int.Parse(Console.ReadLine());
                }
                data = arr.SelectMany(BitConverter.GetBytes).ToArray();
                socketClient.Send(data);
                int x = socketClient.Receive(data);
                string s = Encoding.UTF8.GetString(data, 0, x);
                Console.WriteLine("Giá Trị Nhận Về Từ Server là: {0}", s);
            }
        }
    }
}
