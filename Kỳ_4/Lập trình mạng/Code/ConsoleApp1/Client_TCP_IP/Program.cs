using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client_TCP_IP
{
    internal class Program
    {
        class Client
        {
            public IPAddress ipaddress { get; set; }
            public IPEndPoint ipenoint { get; set; }
            public Socket socketclient { get; set; }
            public byte[] data = new byte[1024];
            public void ketNoi(string ip, int cong)
            {
                ipaddress = IPAddress.Parse($"{ip}");
                ipenoint = new IPEndPoint(ipaddress, cong);
                socketclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketclient.Connect(ipenoint);// client kết nối tới địa chỉ, và cổng được tạo ra từ IPEndPoint
            }
            public void guiDuLieuToiServer (int soLanNhap)
            {
                string input = "";
                for (int i = 0; i < soLanNhap; i++)
                {
                    Console.WriteLine("Nhập Vào Dữ Liệu Gửi Cho Server: ");
                    input = Console.ReadLine();
                    // tạo mảng để chuyển dữ liệu nhập vào thành dạng byte
                    data = Encoding.UTF8.GetBytes(input);
                    socketclient.Send(data, data.Length, SocketFlags.None);// gửi dữ liệu cho server
                    if (input.ToUpper().Equals("QUIT")) break;
                }
 
               
            }
            public void nhanTuServer()
            {
                data = new byte[1024];
                int recv = socketclient.Receive(data);// nhận dữ liệu và trả về, và hàm này trả về số lượng dữ liệu nó nhận được
                string s = Encoding.UTF8.GetString(data, 0, recv);
                Console.WriteLine("Dữ liệu nhận lại từ Server là: " + s);
            }
            public void DongKetNoi()
            {
                socketclient.Disconnect(true);// hủy kết nối
                socketclient.Close();
                Console.ReadKey();
            }
        }
        static void Main(string[] args)// 192.168.1.22
        {
           
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Client may1 = new Client();
            may1.ketNoi("255.0.0.0", 9999);
            may1.guiDuLieuToiServer(1);
            may1.nhanTuServer();
            may1.DongKetNoi();
            
        }
    }
}