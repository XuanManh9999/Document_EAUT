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
            // Chuyen ve dang ki tu UTF8
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            IPAddress iparess = IPAddress.Any;// Nhan ket noi tu dau cung duoc
            IPEndPoint ipenpoint = new IPEndPoint(iparess, 9999);// nhan vao dia chi va cong ket not
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Chờ kết nối từ client....");
            serverSocket.Bind(ipenpoint);// cho nó biến địa chỉ sẽ kết nối
            serverSocket.Listen(10);// tối đa có 10 thiết bị kết nối một lúc
            Socket socketclient = serverSocket.Accept();// chờ tới khi có kết nối và ván lại vào socketclient
            Console.WriteLine("Chấp nhận kết nối từ: " + socketclient.RemoteEndPoint);
            string s;
            while (true)
            {
                byte[] data = new byte[1024];
                int res = socketclient.Receive(data);// nhạn dữ liệu từ client 
                if (res == 0) break;
                s = Encoding.UTF8.GetString(data, 0, res );
                s = s.ToUpper();
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(s);
                socketclient.Send(data, data.Length, SocketFlags.None);// gửi lại cho client 
            }
            socketclient.Shutdown(SocketShutdown.Both);
            socketclient.Close();
            serverSocket.Close();
            Console.ReadKey();
        }
    }
}
