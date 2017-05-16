using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
