using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    class Program
    {
        private static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            var task1 = client.GetStringAsync("http://www.mocky.io/v2/5cb14a3e330000b311572035");

            var task2 = client.GetStringAsync("http://www.mocky.io/v2/5cb14a3e330000b311572035");

            var task3 = client.GetStringAsync("http://www.mocky.io/v2/NotFound");

            var allTasks = new List<Task>() { task1, task2, task3 };
            try
            {
                Task.WhenAll(allTasks).Wait();
            }
            catch (AggregateException e)
            {
                foreach (var eInnerException in e.InnerExceptions)
                {
                    Console.WriteLine(eInnerException.Message);
                }
            }

            foreach (var allTask in allTasks)
            {
                switch (allTask.Status)
                {
                    case TaskStatus.Created:
                        break;
                    case TaskStatus.WaitingForActivation:
                        break;
                    case TaskStatus.WaitingToRun:
                        break;
                    case TaskStatus.Running:
                        break;
                    case TaskStatus.WaitingForChildrenToComplete:
                        break;
                    case TaskStatus.RanToCompletion:
                        Console.WriteLine("執行成功");
                        break;
                    case TaskStatus.Canceled:
                        Console.WriteLine("工作被取消");
                        break;
                    case TaskStatus.Faulted:
                        Console.WriteLine("執行發生異常");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            }

            Console.WriteLine("Press any key for continuing...");
            Console.ReadKey();
        }
    }
}
