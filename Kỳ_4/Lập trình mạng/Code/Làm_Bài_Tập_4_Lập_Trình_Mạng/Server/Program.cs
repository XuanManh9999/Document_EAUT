using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 9999);
            Socket socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_server.Bind(iPEndPoint);
            socket_server.Listen(1000);
            Socket socket_client = socket_server.Accept();
            Byte[] nhanData = new byte[1024];
            string serverNoi = "";
            while (true)
            {
                serverNoi = "";
                nhanData = new byte[1024];
                Console.WriteLine("Vui Lòng Ấn YES để nhận này giờ. Từ Client: ");
                serverNoi = Console.ReadLine();
                if (serverNoi == "YES")
                {
                    int doDai = socket_client.Receive(nhanData);
                    string ClientNoi = Encoding.UTF8.GetString(nhanData, 0, doDai);
                    Console.Write("Client trả về: ");
                    Console.WriteLine(ClientNoi);
                }else
                {
                    Console.WriteLine("Vui Lòng Nhập Yes, hoặc ấn QUIT để thoát: ");
                    if (serverNoi == "QUIT")
                    {
                        break;
                    }
                }
            }
            socket_client.Shutdown(SocketShutdown.Both);
            socket_server.Close();
            socket_client.Close();


        }
    }
}
