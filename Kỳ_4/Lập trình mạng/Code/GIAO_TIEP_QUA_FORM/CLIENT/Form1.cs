using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CLIENT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            IPAddress ipdaress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipdaress, 9999);
            Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_client.Connect(ipEndPoint);
            byte[] data = new byte[1024];
            while (true)
            {
                Console.WriteLine("Nhập Số Phần Từ Muốn Nhập Qua Mảng");
                //int n = int.Parse(Console.ReadLine());
                int[] arr = new int[] {1, 2, 3};
                /*for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Nhập Vào Phần Tử Thứ {0}", i);
                    arr[i] = int.Parse(Console.ReadLine());
                }*/
                data = arr.SelectMany(BitConverter.GetBytes).ToArray();
                socket_client.Send(data);// Gửi Data 1
                int res = socket_client.Receive(data);// nhận dự liệu từ server
                int c = int.Parse(Encoding.UTF8.GetString(data, 0, res));
                lblHienThi.Text = c.ToString();
                Console.WriteLine("Nhận Dữ Liệu Từ Server: {0}", c);
            }
            socket_client.Disconnect(true);
            socket_client.Close();

        }

        private void lblHienThi_Click(object sender, EventArgs e)
        {

        }
    }
}
