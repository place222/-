using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._5使用5._0
{
    class _2在lambda表达式中使用await操作符
    {
        async static Task AsynchronousProcessing()
        {
            Func<string, Task<string>> asyncLambda = async name =>
             {
                 await Task.Delay(TimeSpan.FromSeconds(2));
                 return $"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId}.Is thread pool thread{Thread.CurrentThread.IsThreadPoolThread}";
             };
            string result = await asyncLambda("async lambda");

            Console.WriteLine(result);
        }


        static void Main52(string[] args)
        {
            Task t = AsynchronousProcessing();
            t.Wait();
        }
    }
}
