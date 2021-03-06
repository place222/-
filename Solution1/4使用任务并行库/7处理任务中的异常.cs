﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._4使用任务并行库
{
    class _7处理任务中的异常
    {
        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}.Is thread pool thread:{2}", name,
                Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return 42 * seconds;
        }

        static void Main47(string[] args)
        {
            Task<int> task;
            try
            {
                task = Task.Run(() => TaskMethod("Task 1", 2));
                int result = task.Result;
                Console.WriteLine("Result:{0}", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught:{0}", ex);
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            try
            {
                task = Task.Run(() => TaskMethod("Task 2", 2));
                int result = task.GetAwaiter().GetResult();
                Console.WriteLine("Result {0}", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught:{0}", ex);
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();

            var t1 = new Task<int>(() => TaskMethod("Task 3", 3));
            var t2 = new Task<int>(() => TaskMethod("Task 4", 2));
            var complexTask = Task.WhenAll(t1, t2);
            var exceptionHandler = complexTask.ContinueWith(t =>
            Console.WriteLine("Exception caught:{0}", t.Exception), TaskContinuationOptions.OnlyOnFaulted);
            t1.Start();
            t2.Start();

            Thread.Sleep(TimeSpan.FromSeconds(5));

            Console.ReadKey();
        }
    }
}
