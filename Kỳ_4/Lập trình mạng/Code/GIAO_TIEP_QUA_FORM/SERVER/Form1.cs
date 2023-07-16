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

namespace SERVER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.OutputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Any;
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 9999);
            Socket socKet_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socKet_server.Bind(iPEndPoint);
            socKet_server.Listen(10);
            Console.WriteLine("Chờ Kết Nối Từ Client...");
            Socket socket_client = socKet_server.Accept();
            Console.WriteLine("Chấp Nhận Kết Nối Từ: {0}", socket_client.RemoteEndPoint.ToString());
            byte[] data = new byte[1024];
            int sum = 0;
            while (true)
            {
                int res = socket_client.Receive(data);
                if (res == 0) break;
                int[] arr = new int[data.Length / 4];
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = BitConverter.ToInt32(data, i * 4);
                }
                for (int i = 0; i < arr.Length; i++)
                {
                    sum += arr[i];
                }
                data = new byte[1024];
                data = Encoding.UTF8.GetBytes(sum.ToString());
                socket_client.Send(data);
            }
            socket_client.Close();
            socKet_server.Close();
        }
    }
}
