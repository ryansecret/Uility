// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParrelTask.cs" company="">
//   
// </copyright>
// <summary>
//   The parrel task.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Globalization;

namespace Uility.Example
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The parrel task.
    /// </summary>
    public partial class ParrelTask
    {
        // // Sequential version            
        // foreach (var item in sourceCollection)
        // {
        // Process(item);
        // }

        //// Parallel equivalent   pocess耗费的时间越多并行就越省时间
        // Parallel.ForEach(sourceCollection, currentItem => Process(item));
        #region Public Methods

        /// <summary>
        /// The cancell parallel.
        /// </summary>
        public void CancellParallel()
        {
            int[] nums = Enumerable.Range(0, 10000000).ToArray();
            CancellationTokenSource cts = new CancellationTokenSource();

            CancellationToken cancellationToken = new CancellationToken();
            // Use ParallelOptions instance to store the CancellationToken
            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;
            po.MaxDegreeOfParallelism = System.Environment.ProcessorCount;
            Console.WriteLine("Press any key to start. Press 'c' to cancel.");
            Console.ReadKey();

            // Run a task so that we can cancel from another thread.
            Task.Factory.StartNew(
                () =>
                    {
                        if (Console.ReadKey().KeyChar == 'c')
                        {
                            cts.Cancel();
                        }
                        
                        Console.WriteLine("press any key to exit");
                    });

            try
            {
                Parallel.ForEach(
                    nums, 
                    po, 
                    (num) =>
                        {
                            double d = Math.Sqrt(num);
                            Console.WriteLine("{0} on {1}", d, Thread.CurrentThread.ManagedThreadId);
                            po.CancellationToken.ThrowIfCancellationRequested();
                        });
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Parallels for.
        /// </summary>
        public void ParallelFor()
        {


            int[] nums = Enumerable.Range(0, 1000000).ToArray();
            long total = 0;

            // Use type parameter to make subtotal a long, not an int
            // 第三个参数的类型为 Func(Of TResult)，其中 TResult 是将存储线程本地状态的变量的类型。
            Parallel.For<long>(
                0, 
                nums.Length, 
                () => 0, 
                (j, loop, subtotal) =>
                    {
                        subtotal += nums[j];
                        return subtotal;
                    }, 
                (x) => Interlocked.Add(ref total, x));
        }

        /// <summary>
        /// The parallel foreach.
        /// </summary>
        public void ParallelForeach()
        {
            int[] nums = Enumerable.Range(0, 1000000).ToArray();
            long total = 0;

            // First type parameter is the type of the source elements
            // Second type parameter is the type of the local data (subtotal)
            Parallel.ForEach<int, long>(
                nums, 
                // source collection
                () => 0, 
                // method to initialize the local variable
                (j, loop, subtotal) =>
                    {
                       
                        // method invoked by the loop on each iteration
                        subtotal += nums[j]; // modify local variable
                        return subtotal; // value to be passed to next iteration
                    }, 
                // Method to be executed when all loops have completed.
                // finalResult is the final value of subtotal. supplied by the ForEach method.
                (finalResult) => Interlocked.Add(ref total, finalResult));
        }

        /// <summary>
        /// The parallel or sequence.
        /// </summary>
        public void ParallelOrSequence()
        {
            // Source must be array or IList.
            var source = Enumerable.Range(0, 100000).ToArray();

            // Partition the entire source array.
            var rangePartitioner = Partitioner.Create(0, source.Length);

            double[] results = new double[source.Length];

            // Loop over the partitions in parallel.
            Parallel.ForEach(
                rangePartitioner, 
                (range, loopState) =>
                    {
                        // Loop over each range element without a delegate invocation.
                        for (int i = range.Item1; i < range.Item2; i++)
                        {
                            results[i] = source[i] * Math.PI;
                        }
                    });

            Console.WriteLine("Operation complete. Print results? y/n");
            char input = Console.ReadKey().KeyChar;
            if (input == 'y' || input == 'Y')
            {
                foreach (double d in results)
                {
                    Console.Write("{0} ", d);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The break at threshold.
        /// </summary>
        private static void BreakAtThreshold()
        {
            double[] source = MakeDemoSource(10000, 1.0002);

            // note:具有线程安全的堆栈
            ConcurrentStack<double> results = new ConcurrentStack<double>();

            // Store all values below a specified threshold.
            Parallel.For(
                0, 
                source.Length, 
                (i, loopState) =>
                    {
                        double d = Compute(source[i]);
                        results.Push(d);
                        if (d > .2)
                        {
                            // Might be called more than once!
                            loopState.Break();
                            Console.WriteLine("Break called at iteration {0}. d = {1} ", i, d);
                            Thread.Sleep(1000);
                        }
                    });

            Console.WriteLine("results contains {0} elements", results.Count());
        }

        /// <summary>
        /// The compute.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <returns>
        /// The compute.
        /// </returns>
        private static double Compute(double d)
        {
            // Make the processor work just a little bit.
            return Math.Sqrt(d);
        }

        // Create a contrived array of monotonically increasing
        // values for demonstration purposes. 
        /// <summary>
        /// The make demo source.
        /// </summary>
        /// <param name="size">
        /// The size.
        /// </param>
        /// <param name="valToFind">
        /// The val to find.
        /// </param>
        /// <returns>
        /// </returns>
        private static double[] MakeDemoSource(int size, double valToFind)
        {
            double[] result = new double[size];
            double initialval = .01;
            for (int i = 0; i < size; i++)
            {
                initialval *= valToFind;
                result[i] = initialval;
            }

            return result;
        }

        /// <summary>
        /// The multiply matrices parallel.
        /// </summary>
        /// <param name="matA">
        /// The mat a.
        /// </param>
        /// <param name="matB">
        /// The mat b.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        private static void MultiplyMatricesParallel(double[,] matA, double[,] matB, double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            // A basic matrix multiplication.
            // Parallelize the outer loop to partition the source array by rows.
            Parallel.For(
                0, 
                matARows, 
                i =>
                    {
                        for (int j = 0; j < matBCols; j++)
                        {
                            // Use a temporary to improve parallel performance.
                            double temp = 0;
                            for (int k = 0; k < matACols; k++)
                            {
                                temp += matA[i, k] * matB[k, j];
                            }

                            result[i, j] = temp;
                        }
                    }); // Parallel.For
        }

        /// <summary>
        /// The stop loop.
        /// </summary>
        /// note:sss
        private static void StopLoop()
        {
            Console.WriteLine("Stop loop...");
            double[] source = MakeDemoSource(1000, 1);
            ConcurrentStack<double> results = new ConcurrentStack<double>();

            // i is the iteration variable. loopState is a 
            // compiler-generated ParallelLoopState
            Parallel.For(
                0, 
                source.Length, 
                (i, loopState) =>
                    {
                        // Take the first 100 values that are retrieved
                        // from anywhere in the source.
                        if (i < 100)
                        {
                            // Accessing shared object on each iteration
                            // is not efficient. See remarks.
                            double d = Compute(source[i]);
                            results.Push(d);
                        }
                        else
                        {
                            loopState.Stop();
                            return;
                        }
                    }

                // Close lambda expression.
                ); // Close Parallel.For

            Console.WriteLine("Results contains {0} elements", results.Count());
        }

        #endregion
    }

    /// <summary>
    /// The parrel task.
    /// </summary>
    public partial class ParrelTask
    {
        #region Public Methods

        /// <summary>
        /// The create child task.
        /// </summary>
        public void CreateChildTask()
        {
            var parent = Task.Factory.StartNew(
                () =>
                    {
                        Console.WriteLine("Parent task beginning.");

                        var child = Task.Factory.StartNew(
                            () =>
                                {
                                    //用于同步
                                    Thread.SpinWait(5000000);
                                    Console.WriteLine("Attached child completed.");
                                }, 
                            TaskCreationOptions.AttachedToParent);
                       
                    });

            parent.Wait();
            Console.WriteLine("Parent task completed.");
        }

        /// <summary>
        /// The do with exception.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public void DoWithException()
        {
            var t = Task<int>.Factory.StartNew(() => 54);
          
            var c = t.ContinueWith(
                (antecedent) =>
                    {
                        Console.WriteLine("continuation {0}", antecedent.Result);
                        throw new InvalidOperationException();
                    });

            try
            {
                t.Wait();
                c.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine("Exception handled. Let's move on.");
            Console.Read();
        }

        /// <summary>
        /// The patition.
        /// </summary>
        /// <param name="ProcessData">
        /// The process data.
        /// </param>
        public void Patition(Action<double> ProcessData)
        {
            // Static partitioning requires indexable source. Load balancing
            // can use any IEnumerable.
            var nums = Enumerable.Range(0, 100000000).ToArray();

            // Create a load-balancing partitioner. Or specify false for static partitioning.
            Partitioner<int> customPartitioner = Partitioner.Create(nums, true);

            // The partitioner is the query's data source.
            var q = from x in customPartitioner.AsParallel() select x * Math.PI;

            q.ForAll((x) => { ProcessData(x); });
        }

        /// <summary>
        /// The task cancell.
        /// </summary>
        //public void TaskCancell()
        //{
        //    Task task = new Task(() =>
        //    {
        //        CancellationToken ct = cts.Token;
        //        while (someCondition)
        //        {
        //            ct.ThrowIfCancellationRequested();
        //            // Do the work.
        //            //...                        
        //        }
        //    },
        //    cts.Token
        //    );

        //    Task task2 = task.ContinueWith((antecedent) =>
        //    {
        //        CancellationToken ct = cts.Token;

        //        while (someCondition)
        //        {
        //            ct.ThrowIfCancellationRequested();
        //            // Do the work.
        //            //...                        
        //        }
        //    },
        //    cts.Token);

        //    task.Start();
        //    //...

        //    // Antecedent and/or continuation will 
        //    // respond to this request, depending on when it is made.
        //    cts.Cancel();
        //}

        /// <summary>
        /// The task continue.
        /// </summary>
        public void TaskContinue()
        {
            // The antecedent task. Can also be created with Task.Factory.StartNew.
            Task<DayOfWeek> taskA = new Task<DayOfWeek>(() => DateTime.Today.DayOfWeek);

            
            // The continuation. Its delegate takes the antecedent task
            // as an argument and can return a different type.
            Task<string> continuation =
                taskA.ContinueWith((antecedent) => {   return string.Format("Today is {0}.", antecedent.Result); });

            // Start the antecedent.
            taskA.Start();
           
            // Use the contuation's result.
            Console.WriteLine(taskA.Result);

            Console.WriteLine("hey i first");
            
        }

        /// <summary>
        /// The task continue with mutiple. 调用代码可以通过在任务或任务组上使用 Wait、WaitAll 或 WaitAny 方法或 Result() 属性，或者通过在 try-catch 块中包括 Wait 方法，来处理异常。
        /// </summary>
        public void TaskContinueWithMutiple()
        {
            Task<int>[] tasks = new Task<int>[2];
            tasks[0] = new Task<int>(
                () =>
                    {
                        // Do some work... 
                        return 34;
                    });

            tasks[1] = new Task<int>(
                () =>
                    {
                        // Do some work...
                        return 8;
                    });

            var continuation = Task.Factory.ContinueWhenAll(
                tasks,
                (antecedents) =>
                {
                    int answer = tasks[0].Result + tasks[1].Result;
                    Console.WriteLine("The answer is {0}", answer);
                });
            //var continuation = Task.Factory.ContinueWhenAll<int>(
            //   tasks,
            //   (antecedents) =>
            //   {
            //       int answer = tasks[0].Result + tasks[1].Result;
            //       return answer;
            //   });
            tasks[0].Start();
            tasks[1].Start();
           // Console.WriteLine("The answer is {0}", continuation.Result);
           // continuation.Wait();
        }


        public void CancellTask()
        {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;
           
            var task = Task.Factory.StartNew(() =>
            {

                // Were we already canceled?
                ct.ThrowIfCancellationRequested();

                bool moreToDo = true;
                while (moreToDo)
                {
                    // Poll on this property if you have to do
                    // other cleanup before throwing.
                    if (ct.IsCancellationRequested)
                    {
                        // Clean up here, then...
                        ct.ThrowIfCancellationRequested();
                    }

                }
            }, tokenSource2.Token); // Pass same token to StartNew.

            tokenSource2.Cancel();

            // Just continue on this thread, or Wait/WaitAll with try-catch:
            try
            {
                task.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var v in e.InnerExceptions)
                    Console.WriteLine(e.Message + " " + v.Message);
            }
           
            Console.ReadKey();
        }
        #endregion

         



        #region TPL和传统异步编程

        const int MAX_FILE_SIZE = 14000000;
        public static Task<string> GetFileStringAsync(string path)
        {
            FileInfo fi = new FileInfo(path);
            byte[] data = null;
            data = new byte[fi.Length];

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, data.Length, true);

            //Task<int> returns the number of bytes read
            Task<int> task = Task<int>.Factory.FromAsync(
                    fs.BeginRead, fs.EndRead, data, 0, data.Length, null);

            // It is possible to do other work here while waiting
            // for the antecedent task to complete.
            // ...

            // Add the continuation, which returns a Task<string>. 
            return task.ContinueWith((antecedent) =>
            {
                fs.Close();

                // Result = "number of bytes read" (if we need it.)
                if (antecedent.Result < 100)
                {
                    return "Data is too small to bother with.";
                }
                else
                {
                     
                    // If we did not receive the entire file, the end of the
                    // data buffer will contain garbage.
                    if (antecedent.Result < data.Length)
                        Array.Resize(ref data, antecedent.Result);

                    // Will be returned in the Result property of the Task<string>
                    // at some future point after the asynchronous file I/O operation completes.
                    return new UTF8Encoding().GetString(data);
                }
            });
        }
        #endregion
    }

    /// <summary>
    /// 异常处理
    /// </summary>
    public partial class ParrelTask
    {
        #region Public Methods

        /// <summary>
        /// The attach exeption.
        /// </summary>
        /// <exception cref="Exception">
        /// </exception>
        public void AttachExeption()
        {
            // task1 will throw an AE inside an AE inside an AE
            var task1 = Task.Factory.StartNew(
                () =>
                    {
                        var child1 = Task.Factory.StartNew(
                            () =>
                                {
                                    var child2 =
                                        Task.Factory.StartNew(
                                            () => { throw new MyExcetion("Attached child2 faulted."); }, 
                                            TaskCreationOptions.AttachedToParent);

                                    // Uncomment this line to see the exception rethrown.
                                    // throw new MyCustomException("Attached child1 faulted.");
                                }, 
                            TaskCreationOptions.AttachedToParent);
                    });

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                { 
                    if (e is MyExcetion)
                    {
                        // Recover from the exception. Here we just
                        // print the message for demonstration purposes.
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw;
                    }
                }

                // or ...
                // ae.Flatten().Handle((ex) => ex is MyCustomException);
            }
        }

        #endregion
        public class MyExcetion:Exception
        {
            public MyExcetion(string message):base(message)
            {
                
            }
        }
    }

   
}