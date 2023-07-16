using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Program
    {
        public static SqlConnection chuoiketNoiCuaManh()
        {
            string strCon = @"Data Source=DESKTOP-LNJ99RH\SQLEXPRESS;Initial Catalog=LTM_TRA_TESV;Integrated Security=True";
            SqlConnection sqlCon = new SqlConnection(strCon);
            sqlCon.Open();
            return sqlCon;
        }
        public void Server_Tra_Ve_Ten_SV()
        {
            IPAddress iPAddress = IPAddress.Any;
            IPEndPoint ipend_point = new IPEndPoint(iPAddress, 9999);
            TcpListener tcpListener = new TcpListener(ipend_point);
            tcpListener.Start(10);
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                NetworkStream stream = tcpClient.GetStream();
                StreamReader str = new StreamReader(stream);
                StreamWriter str2 = new StreamWriter(stream);
                str2.AutoFlush = true;
                string DATA_CLIENT_YEU_CAU = str.ReadLine();
                if (DATA_CLIENT_YEU_CAU == "QUIT" || DATA_CLIENT_YEU_CAU == "quit")
                {
                    break;
                }
                SqlCommand sqlCMD = new SqlCommand();
                sqlCMD.CommandType = System.Data.CommandType.Text;
                sqlCMD.CommandText = $"SELECT THONGTINSINHVIEN.TENSV FROM THONGTINSINHVIEN WHERE MASV = '{DATA_CLIENT_YEU_CAU}'";
                sqlCMD.Connection = Program.chuoiketNoiCuaManh();
                SqlDataReader reader = sqlCMD.ExecuteReader();
                string data = "";
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        data = reader.GetString(0);
                    }
                }
                else
                {
                    data = "Không tồn tại tên sinh viên theo mã sinh viên này";
                    Console.WriteLine(data);
                }
                str2.WriteLine(data);
                tcpClient.Close();

            }
        }
        public void guiDuongDanAnh()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Any;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 9999);
            TcpListener tcpListener = new TcpListener(iPEndPoint);
            tcpListener.Start(10);
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                NetworkStream stream = tcpClient.GetStream();
                StreamReader str = new StreamReader(stream);
                StreamWriter str2 = new StreamWriter(stream);
                str2.AutoFlush = true;
                string DATA_CLIENT_YEU_CAU = str.ReadLine();
                if (DATA_CLIENT_YEU_CAU == "ok" || DATA_CLIENT_YEU_CAU == "OK")
                {
                    // Đọc ảnh từ tệp
                    byte[] imageByte = File.ReadAllBytes(@"D:\\DCCNTT12.10.3_EAUT\\Kỳ_4\\Lập trình mạng\\Code\\LTM_Thu_2_17_4_2023\\IMG\\anh-doremon-cute.jpg");
                    str2.WriteLine(imageByte);
                    tcpClient.Close();
                }
                else if (DATA_CLIENT_YEU_CAU == "QUIT" || DATA_CLIENT_YEU_CAU == "quit")
                {
                    break;
                }
            }
        }
        static void Main(string[] args)
        {
            // sử dụng giao thức UDP, xây dựng chat 2 máy
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;
            var Server = new UdpClient(9999);// Tự động bin với cổng 1308
            while(true)
            {
                var remoteEP = new IPEndPoint(0, 0);
                var buffer = Server.Receive(ref remoteEP);
                var text = Encoding.Unicode.GetString(buffer);
                Console.WriteLine("#Client >> {0}", text) ;
                Console.Write("#Server >>>");
                var response = Console.ReadLine();
                buffer = Encoding.Unicode.GetBytes(response);
                Server.Send(buffer, buffer.Length, remoteEP);
            }
        }
    }
}
