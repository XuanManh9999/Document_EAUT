using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CLIENT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 2023);
            Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string s = "Hello Server!...";
            // tao endpoint nhan du lieu tu server
            EndPoint endPoint = (EndPoint)iPEndPoint;
            byte[] data = new byte[1024];
            data = Encoding.Unicode.GetBytes(s);
            socket_client.SendTo(data, iPEndPoint);
            // tao endpoint nhan data tu server
            EndPoint endpoint = (EndPoint)iPEndPoint;
            data = new byte[1024];
            int rec = socket_client.ReceiveFrom(data, ref endPoint);
            s = Encoding.Unicode.GetString(data, 0, rec);
            Console.WriteLine("Nhan Ve Tu SerVer: " + s);
            while (true)
            {
                Console.WriteLine("Nhập Vào Số Phần Tử Bạn Muốn Tính Toán");
                s = Console.ReadLine();
                float[] arr = new float[int.Parse(s)];
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.WriteLine("Nhập Vào Phần Tử Thứ: " + (i + 1));
                    arr[i] = float.Parse(Console.ReadLine());
                }
                data = new byte[1024];
                data = arr.SelectMany(BitConverter.GetBytes).ToArray();
                socket_client.SendTo(data, endpoint);
                Console.WriteLine("Nhập Vào Phép Tính Bạn Muốn Sử Dụng");
                s = Console.ReadLine();
                data = new byte[1024];
                data = Encoding.Unicode.GetBytes(s);
                socket_client.SendTo(data, endpoint);// Hoàn tất việc gửi data
                if (s.ToUpper().Equals("QUIT"))
                {
                    break;
                }
                data = new byte[1024];
                rec = socket_client.ReceiveFrom(data, ref endpoint);
                s = Encoding.Unicode.GetString(data, 0, rec);
                Console.WriteLine("Giá Trị Nhận Về Từ Server: {0}",  s);
            }
            Console.ReadKey();
            socket_client.Close();

        }
    }
}
