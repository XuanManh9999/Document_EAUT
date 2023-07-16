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
        // Bước tạo kết nối
        public IPAddress iPAddress;
        public IPEndPoint iPEndPoint;
        public Socket server_Socket;
        public Socket socketClient;
        public void KhoiTaoKetNoi(int congKetNoi)
        {
            iPAddress = IPAddress.Any;
            iPEndPoint = new IPEndPoint(iPAddress, congKetNoi);
            server_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        // Bước nhận kết nối
        public void nhanKetNoi(int soThietBiChoPhep)
        {
            server_Socket.Bind(iPEndPoint);// Cho socket biết điểm kết nối
            server_Socket.Listen(soThietBiChoPhep);
            Console.WriteLine("Server đang đợi kết nối");
            socketClient = server_Socket.Accept();// đợi
            Console.WriteLine("Nhận Kết Nối Từ: " + socketClient.RemoteEndPoint.ToString());
        }
        // Bước thực thi
        public void ThucThi()
        {
            Byte[] data_1 = new byte[1024];
            Byte[] data_2 = new byte[1024];
            int res_1 = socketClient.Receive(data_1);
            int res_2 = socketClient.Receive(data_2);// nhận dữ liệu
            int data__1 = int.Parse(Encoding.UTF8.GetString(data_1));
            int data__2 = int.Parse(Encoding.UTF8.GetString(data_2));
            int thucThi = data__1 + data__2;
            Byte[] data_Gui = new byte[1024];
            data_Gui = Encoding.UTF8.GetBytes(thucThi.ToString());
            socketClient.Send(data_Gui, data_Gui.Length, SocketFlags.None);
        }
        // Bước đóng kết nối
        public void DongKetNoi()
        {
            socketClient.Shutdown(SocketShutdown.Both);
            socketClient.Close();
            server_Socket.Close();
        }
        static void Main(string[] args)
        {
            // Bước Khởi tạo
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Program server = new Program();
            server.KhoiTaoKetNoi(9999);
            server.nhanKetNoi(10);
            server.ThucThi();
            server.DongKetNoi();
        }
    }
}
