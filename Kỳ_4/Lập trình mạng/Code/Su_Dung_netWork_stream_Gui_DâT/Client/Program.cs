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
            socket_client.Connect(iPEndPoint);
            NetworkStream networkStream = new NetworkStream(socket_client);
            Byte[] data = new Byte[1024];
            string input;
            while (true)
            {
                Console.WriteLine("Message Input: ");
                input = Console.ReadLine();
                data = Encoding.Unicode.GetBytes(input);
                networkStream.Write(data, 0, data.Length);
                if (networkStream.Equals("QUIT"))
                {
                    break;
                }
                data = new byte[1024];
                int _rec = networkStream.Read(data, 0, data.Length);
                string s = Encoding.Unicode.GetString(data, 0, _rec);
                Console.WriteLine(s);
            }
            socket_client.Close();
            networkStream.Close();
        }
    }
}
