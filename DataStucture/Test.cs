using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataStucture
{
    public class Test
    {
        public void BubbleSort(List<int> list)
        {
            for (int i = list.Count-1; i >0; i--)
            {
                int tem = list[i];
                for (int j = 0; j < i; j++)
                {
                    if (list[j]>tem)
                    {
                        list[i] = list[j];
                        list[j] = tem;
                        tem = list[i];
                    }
                }
            }
        }

        public void BubbleSortV2(List<int> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
               
                for (int j = 0; j < i; j++)
                {
                    if (list[j] > list[j+1])
                    {
                        int tem = list[j+1];
                        list[j + 1] = list[j];
                        list[j] = tem;
                    }
                }
            }
        }


        public void InsertSort(List<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                int tem = list[i];
                int index = i-1;
                while (index>0&&list[index]>tem)
                {
                    list[index] = list[index - 1];
                    index--;
                }
                list[index] = tem;
            }
        }
        
        public void SelectionSort(List<int> list)
        {        
            for (int i = 0; i < list.Count; i++)
            {
                int minIndex = i;
                for (int j = i+1; j < list.Count; j++)
                {
                    if (list[j]<list[minIndex])
                    {
                        
                        minIndex = j;
                    }
                }
                int tem = list[i];
                list[i] = list[minIndex];
                list[minIndex] = tem;
            }
        }
        //时间复杂度理想情况 nlogn 最差 n的s方 1<s<2
        public void ShellSort(List<int> list,int seed)
        {
            int increatement = list.Count;
            do
            {

                increatement = increatement/seed + 1;
                for (int i = increatement+1; i < list.Count; i++)
                {
                    int tem = list[i];
                    int j;
                    for (j = i-increatement; j >0&&list[j]>tem; j-=increatement)
                    {
                        list[j + increatement] = list[j];
                    }
                    list[j + increatement] = tem;
                }
            } while (increatement>1);
        }

        public void QuickSort(List<int> list, int low, int upper)
        {
                int i = low;
                int tem = list[i];
                int j = upper;
                while (i<j)
                {
                    while (i<j&&list[j]>tem)
                    {
                        j--;
                    }
                    list[i] = list[j];

                    while (i<j&&list[i]<tem)
                    {
                        i++;
                    }
                    list[j] = list[i];
                }
                list[i] = tem;
                if (i>low)
                {
                    QuickSort(list,low,i);
                }
                if (i<upper)
                {
                     QuickSort(list,i+1,upper);
                }
             
        }
    }

    public class TplTest
    {
        string text = "静夜思 李白床前明月光，疑似地上霜。举头望明月，低头思故乡。";

        private static void Print(string text, int offset)
        {
            text
                .Select((c, i) => new { Char = c, Index = i })
                .GroupBy(c => c.Index % offset, c => c).ToList()
                .ForEach(g => Console.WriteLine());
        }

        private void Test()
        {
            Parallel.For(0, 100, (d) => { });
            int total=0;
            Parallel.ForEach(Enumerable.Range(1, 100), ()=>1, (i, loop, local) =>
            {
                local += 3;
                return local;
            }, (sum) => { Interlocked.Add(ref total, sum); });
            CancellationToken cancellationToken = new CancellationToken();
            
            Task<int> task = new Task<int>(() => { return 1; });
            var taskV2= task.ContinueWith(d => { Console.WriteLine(d); },cancellationToken);
            taskV2.Wait(cancellationToken);
            cancellationToken.Register(() => { Console.WriteLine("cancelled"); });
              
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            
            cancellationTokenSource.Cancel();
            if (cancellationTokenSource.IsCancellationRequested)
            {
                 
            }
            Partitioner<int> partitioner = Partitioner.Create(Enumerable.Range(1, 1111));
            Parallel.ForEach(partitioner, d => {  });
            List<int> nums = Enumerable.Range(1, 111).ToList();

            
        }


        public static int FactorialRecursively(int n)
        {
            return FactorialContinuation(n - 1, r => n * r);
        }
 
        public static int FactorialContinuation(int n, Func<int, int> continuation)
        {
            return FactorialContinuation(n - 1,
                r => continuation(n * r));
        }

        public int FabnaciTailRecurse(int n,int acc1,int acc2)
        {
            if (n == 0)
                return acc1;
            else
            {
                return FabnaciTailRecurse(n - 1,acc2, acc1 + acc2);
            }
        }

        public int FactorialRecurse(int n,int acc1,int acc2)
        {
            if (n == 0)
                return acc1;
            else
            {
                return FactorialRecurse(n - 1, acc2, acc1*acc2);
            }
        }


        public int FactorialRecurseV2(int n, int acc2)
        {
            if (n==0)
            {
                return acc2;
            }
            return FactorialRecurseV2(n - 1, n*acc2);
        }


        public static int FibonacciContinuation(int n, Func<int, int> continuation)
        {
           
            if (n < 2) return continuation(n);
            return FibonacciContinuation(n - 1,r1 => FibonacciContinuation(n - 2,r2 => continuation(r1 + r2)));
        }

    }
}
