using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
            IPEndPoint _ipendpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9999);
            TcpClient _client = new TcpClient();
            _client.Connect(_ipendpoint);
            StreamReader _sr = new StreamReader(_client.GetStream());
            StreamWriter _sw = new StreamWriter(_client.GetStream());
            Console.WriteLine(_sr.ReadLine());
            string _input;
            string _command = "";
            Console.WriteLine("Đăng nhập bằng USER Tên user, PASS Tên password");
            Console.WriteLine("Tạo thư mục bằng MKD Tên thư mục cần tạo");
            Console.WriteLine("Upload bằng STOR Tên file");
            Console.WriteLine("Dowload bằng cách RETR Tên file");
            while (true)
            {
                _input = Console.ReadLine();
                _command = _input.Substring(0, 4).Trim().ToUpper();
                switch (_command)
                {
                    case "STOR":
                        {
                            //đọc file gửi cho server
                            _sw.WriteLine(_input);
                            _sw.Flush();
                            FileInfo _fl = null;
                            try
                            {
                                _fl = new FileInfo(_input.Substring(4, _input.Length - 4).Trim());

                            }
                            catch (IOException)
                            {
                                Console.WriteLine("File không tồn tại");
                            }
                            long _totallength = _fl.Length;
                            FileStream _fs = _fl.OpenRead();
                            _sw.Write(_totallength);
                            byte[] _data = new byte[_totallength];
                            int _bytes = _fs.Read(_data, 0, _data.Length);
                            char[] _kt = Encoding.UTF8.GetChars(_data);
                            _sw.Write(_kt, 0, _data.Length);
                            _sw.Flush();
                            _fs.Close();
                            Console.WriteLine(_sr.ReadLine());
                            break;
                        }
                    case "RETR":
                        {
                            _sw.WriteLine(_input);
                            _sw.Flush();
                            string _s = _sr.ReadLine();
                            Console.WriteLine(_s);
                            if (_s.Substring(0, 3).Equals("150"))
                            {
                                Console.WriteLine("Nhập vào nơi lưu tệp:");
                                string _filename = Console.ReadLine();
                                FileStream _fs = new FileStream(_filename, FileMode.CreateNew);
                                //đọc tệp về
                                long _totallength = _sr.Read();
                                byte[] _data = new byte[_totallength];
                                char[] _kt = new char[_data.Length];
                                int _sobytes = _sr.Read(_kt, 0, _data.Length);
                                _data = Encoding.UTF8.GetBytes(_kt);
                                _fs.Write(_data, 0, _data.Length);
                                _fs.Close();
                            }
                            break;
                        }
                    default:
                        {
                            _sw.WriteLine(_input);
                            _sw.Flush();
                            Console.WriteLine(_sr.ReadLine());
                            break;
                        }
                }
                if (_input.ToUpper().Equals("QUIT"))
                {
                    break;
                }
            }
            _sr.Close();
            _sw.Close();
            _client.Close();
        }
    }
}
