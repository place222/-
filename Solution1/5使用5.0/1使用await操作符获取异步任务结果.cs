using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._5使用5._0
{
    class _1使用await操作符获取异步任务结果
    {

        static Task AsynchronyWithTPL()
        {
            Task<string> t = GetInfoAsync("Task 1");
            Task t2 = t.ContinueWith(task =>
            Console.WriteLine(task.Result), TaskContinuationOptions.NotOnFaulted);
            Task t3 = t.ContinueWith(task => Console.WriteLine(
                task.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted);

            return Task.WhenAny(t2, t3);
        }

        async static Task AsynchronAwait()
        {
            try
            {
                string result = await GetInfoAsync("Task 2");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        async static Task<string> GetInfoAsync(string name)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return $"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId}. Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}";
        }

        static void Main51(string[] args)
        {
            Task t = AsynchronyWithTPL();
            t.Wait();

            t = AsynchronAwait();
            t.Wait();
        }
    }
}
