using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9000);
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(iPEndPoint);
            byte[] data = new byte[1024];
            int recv;
            string s;
            string input;
            while(true)
            {
                Console.WriteLine("Nhập chuỗi: ");
                input = Console.ReadLine();
                data = new byte[1024];
                data = Encoding.Unicode.GetBytes(input);
                socketClient.Send(data, data.Length, SocketFlags.None);
                if (input.ToUpper().Equals("QUIT")) break;
                data = new byte[1024];
                recv = socketClient.Receive(data);
                s = Encoding.Unicode.GetString(data, 0, recv);
                Console.WriteLine("Server Gui: {0}", s);
            }
            socketClient.Disconnect(true);
            socketClient.Close();
        }
    }
}
