using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            IPAddress iPAddress = IPAddress.Any;
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 9000);
            Socket socKet_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socKet_server.Bind(iPEndPoint);
            socKet_server.Listen(10);
            Console.WriteLine("Chờ kết nối từ client...");
            Socket socket_Client = socKet_server.Accept();
            Console.WriteLine("Chập Nhận Kết Nối Từ {0}:", socket_Client.RemoteEndPoint.ToString());
            /* string strCon = @"Data Source=DESKTOP-LNJ99RH\\SQLEXPRESS;Initial Catalog=QUANLYTHUVIEN;Integrated Security=True";
             SqlConnection sqlCon = new SqlConnection(strCon);
             if (sqlCon.State == System.Data.ConnectionState.Closed)
             {
                 sqlCon.Open();
             }
             SqlCommand sqlCMD = new SqlCommand();
             sqlCMD.CommandType = System.Data.CommandType.Text;
             byte[] data = new byte[1024];
             int res = socket_Client.Receive(data);
             string s = Encoding.UTF8.GetString(data, 0, res);
             Console.WriteLine(s);
             sqlCMD.CommandText = $"select * from SINHVIEN where MASV = '{s.Trim()}'";
             sqlCMD.Connection = sqlCon;
             SqlDataReader reader = sqlCMD.ExecuteReader();
             string loand = "";
             if (reader.Read())
             {
                  loand = $"{reader.GetString(0)} | {reader.GetString(1)} | {reader.GetString(2)}| {reader.GetString(3)} | {reader.GetString(4)}|{reader.GetString(5)}";
             }
             Console.WriteLine(loand);*/
           /* string strCon = @"Data Source=DESKTOP-LNJ99RH\\SQLEXPRESS;Initial Catalog=QUANLYTHUVIEN;Integrated Security=True";
            SqlConnection sqlCon = new SqlConnection(strCon);
            sqlCon.Open();*/
            byte[] data = new byte[1024];
            int res = socket_Client.Receive(data);
            string s = Encoding.UTF8.GetString(data, 0, res);
            /*SqlCommand sqlCMD = new SqlCommand();
            sqlCMD.CommandText = $"select * from SINHVIEN where MASV = '{s.Trim()}'";
            sqlCMD.Connection = sqlCon;
            SqlDataReader reader = sqlCMD.ExecuteReader();*/
            Console.WriteLine(s);
            string loand = "21";
            /*if (reader.Read())
            {
                loand = $"{reader.GetString(0) }";
            }*/

            data = new byte[20000];
            data = Encoding.UTF8.GetBytes(loand);
            socket_Client.Send(data);
         
        }
    }
}
