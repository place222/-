using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._4使用任务并行库
{
    class _6实现取消选项
    {
        private static int TaskMethod(string name, int seconds, CancellationToken token)
        {
            Console.WriteLine($"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId} Is Thread pool thread {Thread.CurrentThread.IsThreadPoolThread}");
            for (int i = 0; i < seconds; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (token.IsCancellationRequested)
                    return -1;
            }
            return 42 * seconds;
        }
        //需要注意的就是取消任务的标志需要传送2回，一个给底层任务，一个给任务的构造函数
        //如果任务启动前取消了 TPL负责取消 如果还调用Start()会报InvalidOperationException异常
        //如果任务开始了  由我们写的代码负责
        static void Main46(string[] args)
        {
            var cts = new CancellationTokenSource();
            var longTask = new Task<int>(() => TaskMethod("Task 1", 10, cts.Token), cts.Token);
            Console.WriteLine(longTask.Status);
            cts.Cancel();
            Console.WriteLine(longTask.Status);
            Console.WriteLine("First task has been cancelled before execution");
            cts = new CancellationTokenSource();
            longTask = new Task<int>(() => TaskMethod("Task 2", 10, cts.Token), cts.Token);
            longTask.Start();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            cts.Cancel();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            Console.WriteLine("A task has been completed with result {0}", longTask.Result);


            Console.ReadKey();
        }
    }
}
