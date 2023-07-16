using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public void Client_Tim_MaSV()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9999);
            while (true)
            {
                Console.Write("Nhập Vào Mã Sinh Viên Bạn Muốn Tìm Trong CSDL: ");
                string s = Console.ReadLine();
                if (s == "QUIT" || s == "quit")
                {
                    break;
                }
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(iPEndPoint);
                NetworkStream stream = tcpClient.GetStream();
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.AutoFlush = true;
                StreamReader str = new StreamReader(stream);
                streamWriter.WriteLine(s);
                Console.WriteLine("Dữ Liệu Server Trả về");
                Console.WriteLine(str.ReadLine());
                tcpClient.Close();
            }
        }
        public void NhanAnh_TuServer ()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9999);
            while (true)
            {
                Console.Write("Bạn Có Muốn Nhận Về Chuỗi Nhị Phân Của Ảnh: ");
                string s = Console.ReadLine();
                if (s == "QUIT" || s == "quit")
                {
                    break;
                }
                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(iPEndPoint);
                NetworkStream stream = tcpClient.GetStream();
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.AutoFlush = true;
                StreamReader str = new StreamReader(stream);
                streamWriter.WriteLine(s);
                Console.WriteLine("Chuỗi Nhị Phân Server Trả về");
                tcpClient.Close();
            }
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("Server IP: ");
            var ip = IPAddress.Parse(Console.ReadLine());
            var client = new UdpClient();
            client.Connect(ip, 9999);
            // Dòng này chỉ để lưu lại thông tin về đối tác
            // trong object_client chứ không phải là thực hiện kế nối như TCP socket
            while(true)
            {
                Console.WriteLine("#Client >>>");
                var text = Console.ReadLine();
                var buffer = Encoding.Unicode.GetBytes(text);
                client.Send(buffer, buffer.Length);
                var ipendpoint = new IPEndPoint(0, 0);
                buffer = client.Receive(ref ipendpoint);
                var response = Encoding.Unicode.GetString(buffer);
                Console.WriteLine("Server >>> {0}", response);
            }
        }
    }
}
