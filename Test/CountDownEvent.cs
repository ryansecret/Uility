//#region

//using System;
//using System.Collections.Concurrent;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Threading;
//using System.Threading.Tasks;
//using CommonLibrary.Cryptography;

//#endregion

//namespace Test
//{
//    internal class CDESample
//    {
//        // Demonstrates:
//        //      CountdownEvent construction
//        //      CountdownEvent.AddCount()
//        //      CountdownEvent.Signal()
//        //      CountdownEvent.Wait()
//        //      CountdownEvent.Wait() w/ cancellation
//        //      CountdownEvent.Reset()
//        //      CountdownEvent.IsSet
//        //      CountdownEvent.InitialCount
//        //      CountdownEvent.CurrentCount
//        private static void Main()
//        {
//            CryptographyUtils.GenerateRSAKey(new RSACryptoServiceProvider());
//            Console.WriteLine(CryptographyUtils.RSAPrivateKey);
//            Console.WriteLine(CryptographyUtils.RSAPubicKey);
//            Console.Read();
//            Barrirer.Excute();
//            Sephore.Execute(null);
//            // Initialize a queue and a CountdownEvent
//            var queue = new ConcurrentQueue<int>(Enumerable.Range(0, 10000));
//            var cde = new CountdownEvent(10000); // initial count = 10000

//            // This is the logic for all queue consumers
//            Action consumer = () =>
//            {
//                int local;
//                // decrement CDE count once for each element consumed from queue
//                while (queue.TryDequeue(out local)) cde.Signal();
//            };

//            // Now empty the queue with a couple of asynchronous tasks
//            Task t1 = Task.Factory.StartNew(consumer);
//            Task t2 = Task.Factory.StartNew(consumer);

//            // And wait for queue to empty by waiting on cde
//            cde.Wait(); // will return when cde count reaches 0

//            Console.WriteLine("Done emptying queue.  InitialCount={0}, CurrentCount={1}, IsSet={2}",
//                cde.InitialCount, cde.CurrentCount, cde.IsSet);

//            // Proper form is to wait for the tasks to complete, even if you that their work
//            // is done already.
//            Task.WaitAll(t1, t2);

//            // Resetting will cause the CountdownEvent to un-set, and resets InitialCount/CurrentCount
//            // to the specified value
//            cde.Reset(10);

//            // AddCount will affect the CurrentCount, but not the InitialCount
//            cde.AddCount(2);

//            Console.WriteLine("After Reset(10), AddCount(2): InitialCount={0}, CurrentCount={1}, IsSet={2}",
//                cde.InitialCount, cde.CurrentCount, cde.IsSet);

//            // Now try waiting with cancellation
//            var cts = new CancellationTokenSource();
//            cts.Cancel(); // cancels the CancellationTokenSource
//            try
//            {
//                cde.Wait(cts.Token);
//            }
//            catch (OperationCanceledException)
//            {
//                Console.WriteLine("cde.Wait(preCanceledToken) threw OCE, as expected");
//            }

//            // It's good for to release a CountdownEvent when you're done with it.
//            cde.Dispose();
//        }
//    }

//    public class Sephore
//    {
//        private static readonly SemaphoreSlim slim = new SemaphoreSlim(Environment.ProcessorCount, 12);
//        private static readonly Semaphore s1 = new Semaphore(1, 2);

//        public static void Execute(string[] args)
//        {
//            for (int i = 0; i < 15; i++)
//            {
//                Task.Factory.StartNew(obj => { Run(obj); }, i);
//            }

//            Console.Read();
//        }

//        private static void Run(object obj)
//        {
//            slim.Wait();

//            Console.WriteLine("当前时间:{0}任务 {1}已经进入。", DateTime.Now, obj);

////这里busy3s中
//            Thread.Sleep(3000);

//            slim.Release();
//        }

//        private static void Ex()
//        {
//            Task.Factory.StartNew(() => DoWork());
//            Task.Factory.StartNew(() => DoWork());

//            Console.ReadLine();
//        }

//        private static void DoWork()
//        {
//            try
//            {
//                Console.WriteLine("Thread {0} try to do work.", Thread.CurrentThread.ManagedThreadId);
//                s1.WaitOne();
//                Console.WriteLine("Thread {0} is doing work.", Thread.CurrentThread.ManagedThreadId);
//                Thread.Sleep(5000);
//                Console.WriteLine("Thread {0} is finising work.", Thread.CurrentThread.ManagedThreadId);
//            }
//            finally
//            {
//                s1.Release();
//            }
//        }
//    }


//    public class Barrirer
//    {
 
//        private static readonly Task[] tasks = new Task[4];

//        private static Barrier barrier;

//        public static void Excute()
//        {
//            barrier = new Barrier(tasks.Length, i =>
//            {
//                Console.WriteLine("**********************************************************");
//                Console.WriteLine("\n屏障中当前阶段编号:{0}\n", i.CurrentPhaseNumber);
//                Console.WriteLine("**********************************************************");
//            });
           
//            for (int j = 0; j < tasks.Length; j++)
//            {
//                tasks[j] = Task.Factory.StartNew(obj =>
//                {
//                    var single = Convert.ToInt32(obj);

//                    LoadUser(single);
//                    barrier.SignalAndWait();

//                    LoadProduct(single);
//                    barrier.SignalAndWait();

//                    LoadOrder(single);
//                    barrier.SignalAndWait();
//                }, j);
//            }

//            Task.WaitAll(tasks);

//            Console.WriteLine("指定数据库中所有数据已经加载完毕！");

//            Console.Read();
//        }

//        private static void LoadUser(int num)
//        {
//            Console.WriteLine("当前任务:{0}正在加载User部分数据！", num);
//        }

//        private static void LoadProduct(int num)
//        {
//            Console.WriteLine("当前任务:{0}正在加载Product部分数据！", num);
//        }

//        private static void LoadOrder(int num)
//        {
//            Console.WriteLine("当前任务:{0}正在加载Order部分数据！", num);
//        }
//    }
//}