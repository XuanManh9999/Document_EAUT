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
            Console.Title = "TCPClinet";
            IPAddress _ipaddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint _ipendpoint = new IPEndPoint(_ipaddress, 9999);
            Socket _socketclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socketclient.Connect(_ipendpoint);
            byte[] _data = new byte[1024];
            int _recv = _socketclient.Receive(_data);
            string _s = Encoding.Unicode.GetString(_data, 0, _recv);
            Console.WriteLine("Server gửi:  " + _s);
            string _input;
            while (true)
            {
                Console.WriteLine("Nhập Vào Dữ Liệu Để Gửi Cho Server: ");
                _input = Console.ReadLine();
                _data = new byte[1024];
                _data = Encoding.UTF8.GetBytes(_input);
                _socketclient.Send(_data, _data.Length, SocketFlags.None);
                if (_input.ToUpper().Equals("QUIT")) break;
                _data = new byte[1024]; 
                _recv = _socketclient.Receive(_data);
                _s = Encoding.UTF8.GetString(_data, 0, _recv);
                Console.WriteLine("Server gửi: {0}", _s);
            }
            _socketclient.Disconnect(true);
            _socketclient.Close();
            Console.ReadKey();

        }
    }
}
