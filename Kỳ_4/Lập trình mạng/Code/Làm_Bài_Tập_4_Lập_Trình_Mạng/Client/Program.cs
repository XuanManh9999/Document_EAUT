using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9999);
            Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_client.Connect(iPEndPoint);//kết nối tới server
            Byte[] data = new byte[1024];
            string client_noi;
            client_noi = DateTime.Now.ToString();
            data = new byte[1024];
            data = Encoding.UTF8.GetBytes(client_noi);// chuổi thành dạng byte
            socket_client.Send(data, data.Length, SocketFlags.None);// gửi cho server
            socket_client.Disconnect(true);
            socket_client.Close();


        }
    }
}
