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
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 2023);
            Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string s = "Hello Server!...";
            // tao endpoint nhan du lieu tu server
            EndPoint endPoint = (EndPoint)iPEndPoint;
            byte[] data = new byte[1024];
            data = Encoding.Unicode.GetBytes(s);
            socket_client.SendTo(data, endPoint);
            data = new byte[1024];
            int rec = socket_client.ReceiveFrom(data, ref endPoint);
            s = Encoding.Unicode.GetString(data, 0, rec);
            Console.WriteLine("Nhan Ve Tu SerVer: " + s);
            while (true)
            {
                s = Console.ReadLine();
                data = new byte[1024];
                data = Encoding.Unicode.GetBytes(s);
                socket_client.SendTo(data, endPoint);
                if (s.ToUpper().Equals("QUIT"))
                {
                    break;
                }
                data = new byte[1024];
                rec = socket_client.ReceiveFrom(data, ref endPoint);
                s = Encoding.Unicode.GetString(data, 0, rec);
                Console.WriteLine(s);
            }
            Console.ReadKey();
            socket_client.Close();

        }
    }
}
