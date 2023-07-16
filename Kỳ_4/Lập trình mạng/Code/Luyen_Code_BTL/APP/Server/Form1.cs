using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        private IPEndPoint IP;
        private Socket socket_server;
        private byte[] data = new byte[1024 * 5000];
        List<Socket> socket_client;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = true;
            Conect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Socket socket in socket_client)
            {
                Send(socket);
            }
            AddMessage(textBox1.Text);
            textBox1.Clear();
        }
        public void Conect()
        {
            socket_client = new List<Socket>();
            IP = new IPEndPoint(IPAddress.Any, 9999);
            socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_server.Bind(IP);// đợi
            Thread listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        socket_server.Listen(100);
                        Socket client = socket_server.Accept();
                        socket_client.Add(client);

                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start(client);
                    }
                }
                catch
                {
                    // Tránh trường hợp server bị chết
                    IP = new IPEndPoint(IPAddress.Any, 9999);
                    socket_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
            });
            listen.IsBackground = true;
            listen.Start();

        }
        public void Send(Socket socket)
        {
            if (textBox1.Text != string.Empty)
            {
                if (textBox1.Text != string.Empty)
                    socket.Send(Serialalize(textBox1.Text));

            }
        }
        public void Close()
        {
            socket_server?.Close();
        }
        void Receive(object obj)
        {
            Socket client = obj as Socket;
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];
                    client.Receive(data);
                    string Message = (string)Deserialize(data);
                    AddMessage(Message);
                }
            }
            catch
            {
                socket_client.Remove(client);
                client.Close();
            }

        }
        void AddMessage(string s)
        {
            listView1.Items.Add(new ListViewItem()
            {
                Text = s

            });
        }
        // Phân mảnh data
        byte[] Serialalize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
        // Gom mảnh lại
        object Deserialize(Byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }


    }
}
