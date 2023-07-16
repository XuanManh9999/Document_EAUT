using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SERVER
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
            float a, b = 0;
            while (true)
            {
                data = new byte[1024];
                int res = socket_server.ReceiveFrom(data, ref endpoint);
                float[] arr = new float[data.Length / 4];
                Buffer.BlockCopy(data, 0, arr, 0, data.Length);
                data = new byte[1024];
                recv = socket_server.ReceiveFrom(data, ref endpoint);
                s = Encoding.Unicode.GetString(data, 0, recv);// Có được yêu cầu
                if (s.Trim() == "cong")
                {
                    float sum = 0;
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        sum += arr[i];
                    }
                    data = new byte[1024];
                    data = Encoding.Unicode.GetBytes(sum.ToString());
                    socket_server.SendTo(data, 0, data.Length, SocketFlags.None, endpoint);
                }else if (s.Trim() == "tru")
                {
                    float sum = arr[0];
                    // 1, 2, 3
                    for (int i = 1; i < arr.Length - 1; i++)
                    {
                        sum -= arr[i];
                    }
                    data = new byte[1024];
                    data = Encoding.Unicode.GetBytes(sum.ToString());
                    socket_server.SendTo(data, 0, data.Length, SocketFlags.None, endpoint);
                }else if (s.Trim() == "nhan")
                {
                    float sum = 1;
                    // 1, 2, 3
                    for (int i = 0; i < arr.Length - 1; i++)
                    {
                        sum *=  arr[i];
                    }
                    data = new byte[1024];
                    data = Encoding.UTF8.GetBytes(sum.ToString());
                    socket_server.SendTo(data, endpoint);
                }
                else if (s.Trim() == "chia")
                {
                    float sum = arr[0];
                    for (int i = 1; i < arr.Length - 1; i++)
                    {
                         sum /= arr[i];
                    }
                    data = new byte[1024];
                    data = Encoding.Unicode.GetBytes(sum.ToString());
                    socket_server.SendTo(data, endpoint);
                }
                
                if (s.ToUpper().Equals("QUIT"))
                {
                    break;
                }
            }
            socket_server.Close();
        }
    }
}
