using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._4使用任务并行库
{
    class _2使用任务执行基本的操作
    {
        static Task<int> CreateTask(string name)
        {
            return new Task<int>(() => TaskMethod(name));
        }
        static int TaskMethod(string name)
        {
            Console.WriteLine($"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId}.Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return 42;
        }
        static void Main42(string[] args)
        {
            //主线程上执行
            TaskMethod("Main Thread Task");

            //线程池中执行
            Task<int> task = CreateTask("Task 1");
            task.Start();
            int result = task.Result; //阻塞主线程了
            Console.WriteLine("Result is {0}", result);

            //同步执行选项 也是在主线程上执行
            task = CreateTask("Task 2");
            task.RunSynchronously();
            result = task.Result;
            Console.WriteLine("Result is {0}", result);

            //没有阻塞主线程的方式 主线程一直再用轮训的方式
            task = CreateTask("Task 3");
            task.Start();
            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(task.Status);
            result = task.Result;
            Console.WriteLine("Result is {0}", result);

            Console.ReadKey();
        }
    }
}
