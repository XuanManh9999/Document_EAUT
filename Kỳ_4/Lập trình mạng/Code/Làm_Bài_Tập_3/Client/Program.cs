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
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9999);
            Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_client.Connect(iPEndPoint);//kết nối tới server
            Byte[] data = new byte[1024];
            string client_noi = "";
            while(true)
            {
                while(true)
                {
                    client_noi = "";
                    Console.Write("Client: ");
                    client_noi = Console.ReadLine();
                    if (client_noi == "QUIT" || client_noi == "quit")
                    {
                        break;
                    } 
                    data = new byte[1024];
                    data = Encoding.UTF8.GetBytes(client_noi);// chuổi thành dạng byte
                    socket_client.Send(data, data.Length, SocketFlags.None);// gửi cho server
                    data = new byte[1024];
                    // Nhận Lại Từ Server
                    int res = socket_client.Receive(data);// nhận dữ liệu từ server
                    client_noi = Encoding.UTF8.GetString(data, 0, res);
                    Console.Write("Server: ");
                    Console.WriteLine(client_noi);
                }
            }
            socket_client.Disconnect(true);
            socket_client.Close();

        }
    }
}
