using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace Code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*IPAddress _ipaddress = IPAddress.Parse("127.0.0.1");// chuyển đổi về byte cho máy tính hiẻu
            Console.WriteLine(BitConverter.ToString(_ipaddress.GetAddressBytes()));
            IPAddress.IsLoopback(_ipaddress);
            Console.WriteLine(IPAddress.IsLoopback(_ipaddress));
            bool res =  IPAddress.TryParse(_ipaddress.ToString(), out _ipaddress);
            Console.WriteLine(res);*/
            /*IPAddress _ispaddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint _ipendpoint = new IPEndPoint(_ispaddress, 9000);
            string res = _ipendpoint.ToString();
            Console.WriteLine(res);*/
            // THêm post vào các IP
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            string _url = "https://www.facebook.com/";
            Uri _uri = new Uri(_url);
            var _uritype = typeof(Uri);
            _uritype.GetProperties().ToList().ForEach(p =>
            {
                Console.WriteLine($"{p.Name, 15} {p.GetValue(_uri)}");
            });
            Console.WriteLine($"Segment: {string.Join(",", _uri.Segments)}");
            // Lấy tên và địa chỉ của máy chủ local
            string _hostname = Dns.GetHostName();
            var _ipaddress = Dns.GetHostAddresses(_hostname);
            Console.WriteLine("Host máy chủ là: " + _hostname);
            _ipaddress.ToList().ForEach(ip => Console.WriteLine($"Địa chỉ ip của máy Local: " + ip));
            // Trả ra các địa chỉ tương ứng với tên miền 
            var _hostentry = Dns.GetHostEntry(_uri.Host);
            Console.WriteLine($"Server{_uri.Host} có các IP: ");
            _hostentry.AddressList.ToList().ForEach(ip => Console.WriteLine(ip));
        }
    }
}
