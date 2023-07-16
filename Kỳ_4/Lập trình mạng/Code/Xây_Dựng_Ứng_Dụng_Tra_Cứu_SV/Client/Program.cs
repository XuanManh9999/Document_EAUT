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
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9000);
            Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_client.Connect(iPEndPoint);
            byte []data = new byte[1024];
            string s = "";
            while (true)
            {
                Console.WriteLine("Nhap Ma SV: ");
                s = Console.ReadLine();
                data = Encoding.UTF8.GetBytes(s);
                socket_client.Send(data);
                if (s == "QUIT") { break; }
                data = new byte[1024];
                int x = socket_client.Receive(data);
                string k = Encoding.UTF8.GetString(data, 0, x);
                Console.WriteLine("DaTa Nhan Ve: " + k);
            }
            socket_client.Disconnect(true);
            socket_client.Close();
        }
    }
}
