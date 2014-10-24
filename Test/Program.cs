using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uility.Example.多线程
{

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class SemaphoreSlimDemo
    {

         // CountdownEvent  

        // Demonstrates:
        //      SemaphoreSlim construction
        //      SemaphoreSlim.Wait()
        //      SemaphoreSlim.Release()
        //      SemaphoreSlim.AvailableWaitHandle


        //static void Main()
        //{

        //    SemaphoreSlim ss = new SemaphoreSlim(2); // set initial count to 2
        //    Console.WriteLine("Constructed a SemaphoreSlim with an initial count of 2");
          
        //    Console.WriteLine("First non-blocking Wait: {0} (should be true)", ss.Wait(0));
        //    Console.WriteLine("Second non-blocking Wait: {0} (should be true)", ss.Wait(0));
        //    Console.WriteLine("Third non-blocking Wait: {0} (should be false)", ss.Wait(0));

        //    // Do a Release to free up a spot
        //    ss.Release();

        //    Console.WriteLine("Non-blocking Wait after Release: {0} (should be true)", ss.Wait(0));

        //    // Launch an asynchronous Task that releases the semaphore after 100 ms
        //    Task t1 = Task.Factory.StartNew(() =>
        //    {
        //        Thread.Sleep(100);
        //        Console.WriteLine("Task about to release SemaphoreSlim");
        //        ss.Release();
        //    });

        //    // You can also wait on the SemaphoreSlim via the underlying Semaphore WaitHandle.
        //    // HOWEVER, unlike SemaphoreSlim.Wait(), it WILL NOT decrement the count.
        //    // In the printout below, you will see that CurrentCount is still 1
        //    ss.AvailableWaitHandle.WaitOne();
        //    Console.WriteLine("ss.AvailableWaitHandle.WaitOne() returned, ss.CurrentCount = {0}", ss.CurrentCount);

        //    // Now a real Wait(), which should return immediately and decrement the count.
        //    ss.Wait();
        //    Console.WriteLine("ss.CurrentCount after ss.Wait() = {0}", ss.CurrentCount);

        //    // Clean up
        //    t1.Wait();
        //    ss.Dispose();
        //}
    }

    //using System;
    //using System.Threading;
    //using System.Collections;

    //namespace MonitorCS1
    //{
    //    class MonitorSample
    //    {
    //        const int MAX_LOOP_TIME = 100;
    //        Queue m_smplQueue;

    //        public MonitorSample()
    //        {
    //            m_smplQueue = new Queue();
    //        }
    //        public void FirstThread()
    //        {
    //            int counter = 0;
    //            lock (m_smplQueue)
    //            {
    //                while (counter < MAX_LOOP_TIME)
    //                {
    //                    //Wait, if the queue is busy.
    //                    Monitor.Wait(m_smplQueue);
    //                    //Push one element.
    //                    m_smplQueue.Enqueue(counter);
    //                    //Release the waiting thread.
    //                    Monitor.Pulse(m_smplQueue);

    //                    counter++;
    //                }
    //            }
    //        }
    //        public void SecondThread()
    //        {
    //            lock (m_smplQueue)
    //            {
    //                //Release the waiting thread.
    //                 Monitor.Pulse(m_smplQueue);
    //                //Wait in the loop, while the queue is busy.
    //                //Exit on the time-out when the first thread stops. 
    //                while (Monitor.Wait(m_smplQueue, 1000))
    //                {
    //                    //Pop the first element.
    //                    int counter = (int)m_smplQueue.Dequeue();
    //                    //Print the first element.
    //                    Console.WriteLine(counter.ToString());
    //                    //Release the waiting thread.
    //                    Monitor.Pulse(m_smplQueue);
    //                }
    //            }
    //        }
    //        //Return the number of queue elements.
    //        public int GetQueueCount()
    //        {
    //            return m_smplQueue.Count;
    //        }

    //        static void Main(string[] args)
    //        {
    //            //Create the MonitorSample object.
    //            MonitorSample test = new MonitorSample();
    //            //Create the first thread.
    //            Thread tFirst = new Thread(new ThreadStart(test.FirstThread));
    //            //Create the second thread.
    //            Thread tSecond = new Thread(new ThreadStart(test.SecondThread));
    //            //Start threads.
    //            tFirst.Start();
    //            tSecond.Start();
    //            //wait to the end of the two threads
    //            tFirst.Join();
    //            tSecond.Join();
    //            //Print the number of queue elements.
    //            Console.WriteLine("Queue Count = " + test.GetQueueCount().ToString());
    //            Console.Read();
    //        }
    //    }
    //}
}
