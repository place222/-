using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._4使用任务并行库
{
    class _1创建任务
    {
        static void TaskMethod(string name)
        {
            Console.WriteLine($"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId}.Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
        }

        static void Main41(string[] args)
        {
            var t1 = new Task(() => TaskMethod("Task 1"));
            var t2 = new Task(() => TaskMethod("Task 2"));
            t2.Start();
            t1.Start();

            Task.Run(() => TaskMethod("Task 3"));
            Task.Factory.StartNew(() => TaskMethod("Task 4"));
            Task.Factory.StartNew(() => TaskMethod("Task 5"), TaskCreationOptions.LongRunning); //不会使用线程池中的线程 会单独开一个独立线程

            Thread.Sleep(TimeSpan.FromSeconds(1));

            Console.ReadKey();
        }
    }
}
