using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server_TCP_IP
{
    internal class Program
    {
        class Server
        {
            public IPAddress ipaddress { get; set; }
            public IPEndPoint ipendpoint { get; set; }
            public Socket socketserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            public Socket socketclient { get; set; }
            public void NhanKetNoi(int cong, int soThietBiKN)
            {
                IPAddress ipaddress = IPAddress.Any;// Muốn lắng nghe toàn bộ địa chỉ IP
                                                    // Sử dụng để lắng nghe kết nối tới địa chỉ, và cổng 9999
                IPEndPoint ipendpoint = new IPEndPoint(ipaddress, cong);//kết nối với địa chỉ mảng từ client với cổng là 9999
                Socket socketserver = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketserver.Bind(ipendpoint);// Cho Socket biết địa chỉ IP, và cổng lắng nghe về client
                socketserver.Listen(soThietBiKN);// số kết nối tối đa mà nó có thể chấp nhận đồng thời 
                Console.WriteLine("Chờ kết nối từ client: ");
                socketclient = socketserver.Accept();// thiết lập kết nối, cho phép chấp nhận kết nối từ máy khách
                Console.WriteLine("Chấp nhận kết nối từ: {0}", socketclient.RemoteEndPoint);// trả về IP của máy chủ và máy khách tương ứng
                                                                                            // Nhận dữ liệu
            }
            public void thucThi()
            {
                while (true)
                {
                    byte[] data = new byte[1024];
                    int recv = socketclient.Receive(data);// nhận dữ liệu trả về độ dài dữ liệu nhận được
                    if (recv == 0)
                    {
                        // không nhận được 
                        break;
                    }
                    string s = Encoding.UTF8.GetString(data, 0, recv);// nhạn dữ liệu, vị trí index, độ dài dữ liệu
                    Console.WriteLine("Dữ Liệu Nhận Được Từ Client: " + s);
                    if (s.ToUpper().Equals("QUIT")) break;// nhận được chữ QUIT -> Thoát
                    s = s.ToUpper();
                    data = new byte[1024];// tạo mới để nhận được các dữ liệu mới
                    data = Encoding.UTF8.GetBytes(s);// chuyển về dạng byte gửi lại cho client 
                    socketclient.Send(data, data.Length, SocketFlags.None);// gửi lại cho client
                }
            }
            public void dongKetNoi()
            {
                socketclient.Close();
                socketclient.Shutdown(SocketShutdown.Both);
                socketserver.Close();
            }
        }
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Server server1 = new Server();
            server1.NhanKetNoi(9999, 10);
            server1.thucThi();
            server1.dongKetNoi();
            Console.ReadKey();

        }
    }
}
