using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution1._4使用任务并行库
{
    class _3组合任务
    {
        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine($"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId} IsThread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return 42 * seconds;
        }
        static void Main43(string[] args)
        {
            var firstTask = new Task<int>(() => TaskMethod("First Task", 3));
            var secondTask = new Task<int>(() => TaskMethod("Second Task", 2));

            firstTask.ContinueWith(t => Console.WriteLine($"The first answer is {t.Result}. Thread id {Thread.CurrentThread.ManagedThreadId},is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}"),
                TaskContinuationOptions.OnlyOnRanToCompletion);

            firstTask.Start();
            secondTask.Start();

            Thread.Sleep(TimeSpan.FromSeconds(4));

            //又同步执行后续的这个操作 指定回到主线程上执行
            Task continuation = secondTask.ContinueWith(
                t => Console.WriteLine($"The second anser is {t.Result}.Thread id {Thread.CurrentThread.ManagedThreadId},is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}"),
                TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);

            //另一种方式定义后续操作
            continuation.GetAwaiter().OnCompleted(
                () => Console.WriteLine($"Continuation Task Completed! Thread id {Thread.CurrentThread.ManagedThreadId}, is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}"));


            Thread.Sleep(TimeSpan.FromSeconds(2));

            Console.WriteLine();
            firstTask = new Task<int>(() =>
            {
                var innerTask = Task.Factory.StartNew(() => TaskMethod("Second Task", 5), TaskCreationOptions.AttachedToParent);
                innerTask.ContinueWith(t => TaskMethod("Third Task", 2), TaskContinuationOptions.AttachedToParent);
                return TaskMethod("First Task", 2);
            });
            firstTask.Start();
            while (!firstTask.IsCompleted)
            {
                Console.WriteLine(firstTask.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(firstTask.Status);

            Thread.Sleep(TimeSpan.FromSeconds(10));

            Console.ReadKey();
        }
    }
}
