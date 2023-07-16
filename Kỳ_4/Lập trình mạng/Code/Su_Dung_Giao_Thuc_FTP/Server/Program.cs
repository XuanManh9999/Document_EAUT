using System;
using System.Collections.Generic;
using System.IO;
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
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            string _rootDir = @"D:\\DCCNTT12.10.3_EAUT\\Kỳ_4\\Lập trình mạng\\Code\\Su_Dung_Giao_Thuc_FTP\\Server\\TEST.txt";
            IPEndPoint _ipendpoint = new IPEndPoint(IPAddress.Any, 9999);
            TcpListener _serverlistenner = new TcpListener(_ipendpoint);
            _serverlistenner.Start();
            TcpClient _client = _serverlistenner.AcceptTcpClient();
            StreamReader _sr = new StreamReader(_client.GetStream());
            StreamWriter _sw = new StreamWriter(_client.GetStream());
            _sw.WriteLine("Chào mừng kết nối tới MyFTP");
            _sw.Flush();
            while (true)
            {
                string _request = _sr.ReadLine();
                string _command = "";
                if (_request.Length != 0) _command = _request.Substring(0, 4);
                switch (_command.ToUpper().Trim())
                {
                    case "USER":
                        {
                            _sw.WriteLine("331. Nhập Pass");
                            _sw.Flush();
                            Console.WriteLine(_request);
                            break;
                        }
                    case "PASS":
                        {
                            _sw.WriteLine("230. Đăng nhập thành công");
                            _sw.Flush();
                            Console.WriteLine(_request);
                            break;
                        }
                    case "MKD":
                        {
                            string _folderName = _request.Substring(4, _request.Length - 4);
                            _folderName = _rootDir + "/" + _folderName.Trim();
                            try
                            {
                                Directory.CreateDirectory(_folderName);
                                _sw.WriteLine("150. Tạo thư mục thành công");
                                _sw.Flush();
                            }
                            catch (IOException)
                            {
                                _sw.WriteLine("550 Taoj thư mục có lỗi");
                                _sw.Flush();
                            }
                            break;
                        }
                    case "RETR":
                        {
                            string _fileName = _request.Substring(4, _request.Length - 4);
                            _fileName = _rootDir + "/" + _fileName.Trim();
                            try
                            {
                                if (File.Exists(_fileName))
                                {
                                    //gửi nội dung file về cho client xử lý
                                    _sw.WriteLine("150 Truyền file thành công");
                                    _sw.Flush();
                                    FileStream _fs = new FileStream(_fileName, FileMode.Open);
                                    long _totallength = _fs.Length;
                                    byte[] _data = new byte[_totallength];
                                    _fs.Read(_data, 0, _data.Length);
                                    _sw.Write(_totallength);
                                    char[] _kt = Encoding.UTF8.GetChars(_data);
                                    _sw.WriteLine(_kt, 0, _data.Length);
                                    _sw.Flush();
                                    _fs.Close();
                                }
                                else
                                {
                                    _sw.WriteLine("550 File không tồn tại trên server");
                                    _sw.Flush();
                                }
                            }
                            catch (IOException)
                            {
                                _sw.WriteLine("550 Không truyền được file");
                                _sw.Flush();
                            }
                            break;
                        }
                    case "STOR":
                        {
                            //_request.LastIndexOf("/")
                            //_request.LastIndexOf("/")
                            string _fileName = _request.Substring(5, _request.Length - 5);
                            _fileName = _rootDir + "/" + _fileName.Trim();
                            try
                            {
                                FileStream _fs = new FileStream(_fileName, FileMode.CreateNew);
                                long _totallength = _sr.Read();
                                byte[] _data = new byte[_totallength];
                                char[] _kt = Encoding.UTF8.GetChars(_data);
                                int _sobyte = _sr.Read(_kt, 0, _data.Length);
                                _fs.Write(Encoding.UTF8.GetBytes(_kt), 0, _sobyte);
                                string _doc = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(_kt));
                                Console.WriteLine(_doc);
                                _fs.Close();
                                _sw.WriteLine("150 Up File thành công");
                                _sw.Flush();

                            }
                            catch (IOException)
                            {
                                _sw.WriteLine("550 không truyền được file");
                                _sw.Flush();
                            }
                            break;
                        }
                    case "QUIT":
                        {
                            _client.Close();
                            break;
                        }
                    default:
                        {
                            _sw.WriteLine("sai lệnh");
                            _sw.Flush();
                            break;
                        }
                }
            }
        }
    }
}
