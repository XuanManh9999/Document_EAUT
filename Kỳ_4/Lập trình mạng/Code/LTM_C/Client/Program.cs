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
            IPAddress ipadress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipendpoint = new IPEndPoint(ipadress, 9999);
            Socket socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(ipendpoint);// kết nối tới chính địa chỉ của nó
            string input = "";
            while (true)
            {
                Console.WriteLine("Nhập vào dữ liệu bạn muốn gửi cho server: ");
                input = Console.ReadLine();
                // Tạo một đối tượng 
                byte[] data = new byte[1024];
                data = Encoding.UTF8.GetBytes(input);
                socketClient.Send(data, data.Length, SocketFlags.None);// gửi cho server
                if ((input.ToUpper().Equals("QUIT"))) break;
                data = new byte[1024];// tạo thêm một cái data để tránh lặp lại dữ liệu
                int res_ = socketClient.Receive(data);// nhận lại data gửi về
                string s = Encoding.UTF8.GetString(data);
                Console.WriteLine("Server gửi: " + s);
            }
            socketClient.Close();
            socketClient.Disconnect(true);

        }
    }
}
