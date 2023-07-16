using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
namespace Server_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Bài 1 TCP";
            IPAddress _ipaddress = IPAddress.Any;
            IPEndPoint _ipendpoint = new IPEndPoint(_ipaddress, 9999);// 6535
            Socket _socketserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Thực hiện bin ipendpoint socket và lắng nghe
            _socketserver.Bind(_ipendpoint);
            _socketserver.Listen(10);
            Console.WriteLine("Chời kết Nối Từ Client...");
            Socket _socketclient = _socketserver.Accept();
            Console.WriteLine("Chấp nhận kết nối từ: {0}", _socketclient.RemoteEndPoint);
            // Tạo chuỗi dữ liệu, chuyển thành mảng byte
            string s_ = "Chào Mừng Bạn Đến Với Server";
            byte[] _data = new byte[1024];
            _data = Encoding.UTF8.GetBytes(s_);
            _socketclient.Send(_data, _data.Length, SocketFlags.None);//SocketFlags đọc
            // Nhận dữ liệu
            while (true)
            {
                _data = new byte[1024];
                int _recv = _socketclient.Receive(_data);// Trả về độ dài 
                if (_recv == 0) break;
                s_ = Encoding.UTF8.GetString(_data, 0, _recv);
                Console.WriteLine("Client gửi đến : {0}", s_);
                if (s_.ToUpper().Equals("QUIT")) break;
                s_ = s_.ToUpper();
                _data = new byte[1024];
                _data = Encoding.UTF8.GetBytes(s_);
                _socketclient.Send(_data, _data.Length, SocketFlags .None);
            }
            _socketclient.Shutdown(SocketShutdown.Both);
            _socketclient.Close();
            _socketserver.Close();
            Console.ReadKey();
        }
    }
}
