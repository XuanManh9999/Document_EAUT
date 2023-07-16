using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Tạo một đối tượng thread mới
        Thread t = new Thread(CountNumbers);

        // Bắt đầu thực thi thread
        t.Start();

        // Tiếp tục thực hiện các công việc khác trong main thread
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine("Main thread counting: " + i);
            Thread.Sleep(500); // Dừng main thread trong 0.5 giây
        }
    }

    static void CountNumbers()
    {
        // Thực hiện đếm số trong thread mới
        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine("New thread counting: " + i);
            //Thread.Sleep(1000); // Dừng thread mới trong 1 giây
        }
    }
}
