using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._5使用5._0
{
    class _4对并行执行的异步任务使用await操作符
    {
        async static Task AsynchronousProcessing()
        {
            Task<string> t1 = GetInfoAsync("Task 1", 3);
            Task<string> t2 = GetInfoAsync("Task 2", 5);

            string[] results = await Task.WhenAll(t1, t2);
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }

        async static Task<string> GetInfoAsync(string name, int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds)); // 这个可能还是原来的线程 计时器
            //await Task.Run(() => Thread.Sleep(TimeSpan.FromSeconds(seconds))); //阻塞了 2个任务2个线程
            return string.Format("Task {0} is running on thread id {1}. Is thread pool thread:{2}", name,
                Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.IsThreadPoolThread);
        }

        static void Main(string[] args)
        {
            Task t = AsynchronousProcessing();
            t.Wait();

            Console.ReadKey();
        }
    }
}
