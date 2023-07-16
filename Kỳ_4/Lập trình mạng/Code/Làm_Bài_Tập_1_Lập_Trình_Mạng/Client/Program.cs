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
        public IPAddress iPAddress;
        public IPEndPoint iPEndPoint;
        public Socket client_socKet;
        public Byte[] data_1 = new byte[1024];
        public Byte[] data_2 = new byte[1024];
        public Byte[] data_nhan = new byte[1024];
        public void KhoiTaoKetNoi(string diaChiIP, int congKetNoi)
        {
            iPAddress = IPAddress.Parse(diaChiIP);
            iPEndPoint = new IPEndPoint(iPAddress, congKetNoi);
            client_socKet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void KetNoi()
        {
            client_socKet.Connect(iPEndPoint);// kết nối tới địa chỉ ip
        }
        public void GuiDuLieu(int a, int b)
        {
            data_1 = Encoding.UTF8.GetBytes(a.ToString());
            data_2 = Encoding.UTF8.GetBytes(b.ToString());
            client_socKet.Send(data_1, data_1.Length, SocketFlags.None);// gửi dữ liệu
            client_socKet.Send(data_2, data_2.Length, SocketFlags.None);// gửi dữ liệu
        }
        public void HienThiDuLieuNhan()
        {
            int res = client_socKet.Receive(data_nhan);
            string kq = Encoding.UTF8.GetString(data_nhan, 0, res);
            Console.WriteLine("Server gửi: " + kq);
        }
        public void dongDuLieu()
        {
            client_socKet.Disconnect(true);
            client_socKet.Close();
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Program client = new Program();
            client.KhoiTaoKetNoi("127.0.0.1", 9999);
            client.KetNoi();
            Console.WriteLine("Nhập dữ liệu 1: ");
            int a = int.Parse(Console.ReadLine());
            Console.WriteLine("Nhập dữ liệu 2: ");
            int b = int.Parse(Console.ReadLine());
            client.GuiDuLieu(a, b);
            client.HienThiDuLieuNhan();
            client.dongDuLieu();
        }
    }
}
