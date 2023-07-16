using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            Socket socket_server =new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_server.Bind(iPEndPoint);
            socket_server.Listen(10);
            Console.WriteLine("Server đang đợi kết nối đó: ");
            // Khở tạo đợi kết noois 
            Socket socket_client = socket_server.Accept();// đợi kết nối
            Byte[] data = new Byte[1024];
            NetworkStream networkStream = new NetworkStream(socket_client);
            while(true)
            {
                data = new byte[1024];
                int doDaiDoc = networkStream.Read(data, 0, data.Length);
                string s = Encoding.UTF8.GetString(data, 0, doDaiDoc);
                Console.WriteLine(s);
                data = new byte[1024];
                s = s.ToUpper();
                if (s.Equals("QUIT")) break;
                data = Encoding.Unicode.GetBytes(s);
                networkStream.Write(data, 0, data.Length);
            }
            Console.ReadKey();
            socket_client.Close();
            socket_server.Close();
        }
    }
}
