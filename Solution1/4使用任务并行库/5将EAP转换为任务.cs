﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._4使用任务并行库
{
    class _5将EAP转换为任务
    {
        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}.Is thread pool thread:{2}", name,
                Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return 42 * seconds;
        }
        static void Main45(string[] args)
        {
            var tcs = new TaskCompletionSource<int>();

            var worker = new BackgroundWorker();
            worker.DoWork += (sender, evenargs) =>
            {
                evenargs.Result = TaskMethod("Background worker", 5);
            };
            worker.RunWorkerCompleted += (sender, eventArgs) =>
            {
                if (eventArgs.Error != null)
                    tcs.SetException(eventArgs.Error);
                else if (eventArgs.Cancelled)
                    tcs.SetCanceled();
                else
                    tcs.SetResult((int)eventArgs.Result);
            };
            worker.RunWorkerAsync();

            int result = tcs.Task.Result;

            Console.WriteLine("Result is {0}", result);
            Console.ReadKey();

        }
    }
}
