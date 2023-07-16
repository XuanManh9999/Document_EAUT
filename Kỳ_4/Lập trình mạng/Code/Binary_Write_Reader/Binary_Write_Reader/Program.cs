using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Write_Reader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            /*BinaryWriter binaryWriter = new BinaryWriter(File.Open(@"D:\DCCNTT12.10.3_EAUT\Kỳ_4\Lập trình mạng\Code\Binary_Write_Reader\binary.bin", FileMode.Create));
            binaryWriter.Write("Hello Mn");
            binaryWriter.Write(true);*/
            // Đọc
            BinaryReader binaryReader = new BinaryReader(File.Open(@"D:\\DCCNTT12.10.3_EAUT\\Kỳ_4\\Lập trình mạng\\Code\\Binary_Write_Reader\\binary.bin", FileMode.Open));
            Console.WriteLine(binaryReader.ReadString());
            Console.WriteLine(binaryReader.ReadBoolean());
            Console.WriteLine(binaryReader.ReadSingle());
            binaryReader.Close();
        }
    }
}
