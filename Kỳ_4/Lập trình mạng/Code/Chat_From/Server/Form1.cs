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

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Start();
        }
        private Socket socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Socket socket_client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] data = new byte[1024]; 
        private void Start()
        {
            try
            {
                IPAddress iPAddress = IPAddress.Any;
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 2023);
                socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket_server.Bind(iPEndPoint);
                socket_server.Listen(10);
                Thread thread = new Thread(() =>
                {
                    while (true)
                    {
                        socket_server.BeginAccept(new AsyncCallback(AccepCallBack), null);
                    }
                });
                thread.Start();
                thread.IsBackground = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AccepCallBack(IAsyncResult ar)
        {
            try
            {
                socket_client = socket_server.EndAccept(ar);
                data = new byte[socket_client.ReceiveBufferSize];
                socket_client.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), null);
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
                int recv = socket_client.EndReceive(ar);
                string s = Encoding.Unicode.GetString(data);
                AppendToTextBox(s);
                socket_client.BeginReceive(data, 0, data.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), null);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        } 
        private void AppendToTextBox(string s)
        {
            try
            {
                MethodInvoker invoker = new MethodInvoker(delegate
                {
                    textBox1.Text += s + "\n";
                });
                this.Invoke(invoker);
            }catch(Exception ex ) {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
