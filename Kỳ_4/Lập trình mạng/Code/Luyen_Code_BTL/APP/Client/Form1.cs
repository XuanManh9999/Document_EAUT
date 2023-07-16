using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        private IPEndPoint IP;
        private Socket socketClient;
        private byte[] data = new byte[1024 * 5000];
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = true;
            Conect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Send();
            AddMesesage(textBox1.Text);     
        }
        public void Conect()
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IP = new IPEndPoint(iPAddress, 9999);
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socketClient.Connect(IP);// Kết nối tới server
            }catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối tới Server", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();
        }
        public void Close()
        {
            socketClient.Close();
        }
        public void Send()
        {
            if(textBox1.Text != string.Empty)
            {
                data = new byte[1024 * 5000];
                data = Encoding.UTF8.GetBytes(textBox1.Text);
                socketClient.Send(data);    
            }
        }
        public void Receive()
        {
            try
            {
                while(true)
                {
                    data = new byte[1024 * 5000];
                    socketClient.Receive(data);
                    string Message = Encoding.UTF8.GetString(data);
                    AddMesesage(Message);
                }
            }catch
            {
                Close();
            }
        }
        void AddMesesage(string s)
        {
            listView1.Items.Add(new ListViewItem() { 
                Text = s,
            });
            textBox1.Clear();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
