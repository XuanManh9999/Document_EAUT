using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _25_04_2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread thread = new Thread(TEST);
            //thread.Start();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Theard Main: " + i);
            }
        }
        static void TEST()
        {
            Thread.Sleep(1000);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Theard TEST: "+ i);
            }
        }
    }
}
