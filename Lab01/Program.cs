using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lab01
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var begin = DateTime.Now;
            var tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                tasks.Add(Task.Run(async() =>
                {
                    string url = $"http://www.mocky.io/v2/5c9f25633000004a45ee9921";//?mocky-delay=500ms";

                    var start = DateTime.Now;
                    using (var client = new HttpClient())
                    {
                        var t1 = client.GetStringAsync(url);
                        var t2 = client.GetStringAsync(url);
                        //t1.Wait();
                        //t2.Wait();
                        //Task.WaitAll(t1, t2);
                        await Task.WhenAll(t1, t2);
                        var end = DateTime.Now;
                        var timeSpan = DateTime.Now - start;
                        Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId}, Start: {start}, End:{end}, TimeSpan:{timeSpan}");
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId}, Total TimeSpan: {DateTime.Now - begin}ms");
            Console.ReadLine();
        }
    }
}