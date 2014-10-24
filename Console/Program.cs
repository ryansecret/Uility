using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Uility.Example;
using System.Net;
using System.Xml;
namespace Consoles
{
    static class Program
    {
        //public override int GetHashCode()
        //{
        //    return this.GetHashCode() | ss.GetHashCode();
            
        //}
        // struct MyStruct
        //{
        //    public string show { get; set; }
        //    public int i ;
        //}

       

        static int BinarySearching(this List<int> list,int value)
        {
            
            int upperBound, lowerBound, mid;
            upperBound = list.Count - 1;
            lowerBound = 0;
            while (true)
            {
                mid = (upperBound + lowerBound)/2;
                if (list[mid]==value)
                {
                    return mid;
                }
                else
                {
                    if (value<list[mid])
                    {
                        upperBound = mid - 1;
                    }
                    else
                    {
                        lowerBound = mid + 1;
                    }
                }
            }
            
        }

        static void SelectionSort(this IList<int> list)
        {
            int upperBound = list.Count;
            for (int outer = 0; outer < upperBound; outer++)
            {
                int minIndex = outer;
                for (int inner = outer+1; inner < upperBound; inner++)
                {
                    if (list[inner] < list[minIndex])
                    {
                        minIndex = inner;
                    }
                }
                int temp = list[minIndex];
                list[minIndex] = list[outer];
                list[outer] = temp;
            }
        }

        static void InsertSort(this IList<int> list )
        {
            int upperBound = list.Count;
            for (int i = 1; i < upperBound; i++)
            {
                int inner = i;
                int tem = list[i];
                while (inner > 0 && list[inner - 1] > tem)
                {
                    list[inner] = list[inner - 1];
                    inner--;
                }
                list[inner] = tem;
            }
        }
        static void ShellSorting(this IList<int> list,int seed )
        {
             
            int increment = list.Count;

            do
            {
                increment = increment / seed + 1;
                for (int i = increment+1; i <list.Count; i++)
                {
                    if (list[i]<list[i-increment])
                    {
                        int j;
                        int tem = list[i];
                        for ( j = i-increment;j>0&&tem<list[j]; j-=increment)
                        {
                            list[j + increment] = list[j];
                        }
                        list[j + increment] = tem;
                    } 
                }
            } while (increment>1);
        }

        static void ShellSort(this IList<int> list)
        {
            int inc;
            for (inc = 1; inc <= list.Count / 9; inc = 3 * inc + 1) 
            for (; inc > 0; inc /= 3)
            {
                for (int i = inc + 1; i <= list.Count; i += inc)
                {
                    int t = list[i - 1];
                    int j = i;
                    while ((j > inc) && (list[j - inc - 1] > t))
                    {
                        list[j - 1] = list[j - inc - 1];
                        j -= inc;
                    }
                    list[j - 1] = t;
                }
            }
            //int numElements = list.Count;
            //int inner, temp;
            //int h = 1;
            //while (h <= numElements/3)
            //    h = h*3 + 1;
            //while (h > 0)
            //{
            //    for (int outer = h; h <= numElements - 1; h++)
            //    {
            //        temp = list[outer];
            //        inner = outer;
            //        while ((inner > h - 1) && list[inner - h] >= temp)
            //        {
            //            list[inner] = list[inner - h];
            //            inner -= h;
            //        }
            //        list[inner] = temp;
            //    }
            //    h = (h - 1)/3;
            //}
        }
        [Test(Name ="dd")]
        static void ButtbleSort([Test(Name = "dd")]IList<int> list)
        {
            int count = list.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                for (int j = 0; j < i; j++)
                {

                    if (list[j] > list[j + 1])
                    {
                        int tem = list[j + 1];
                        list[j + 1] = list[j];
                        list[j] = tem;
                    }
                }
            }
           
        }

        public static int FabSearch(int i)
        {
            if (i<3)
            {
                return 1;
            }
            else
            {
                
                return FabSearch(i - 1) + FabSearch(i - 2);
            }
        }

        public class what
        {
            void test1(ref int dd,out int i)
            {
                i = 1;
            }

            void test()
            {
              int ss = 1,s;
              test1(ref ss,out  s);
            } 
        }

        [AttributeUsage(AttributeTargets.All)]
        public class TestAttribute : Attribute
        {
            [ThreadStatic]
            public static string test;

            private string _name;

            public  string Name
            {
                get { return _name; }
                set { _name = value; }
            }

             
            
            public TestAttribute()
            {
                ReaderWriterLockSlim readerWriterLock = new ReaderWriterLockSlim();
                readerWriterLock.EnterReadLock();
                readerWriterLock.ExitReadLock();
            }
        }
        public static void CancellParallel()
        {
           
        }
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
            System.Console.WriteLine("Stop loop...");
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

            System.Console.WriteLine("Results contains {0} elements", results.Count());
        }

        public abstract class A
        {
            public A()
            {
                PrintValues();
            }

            public   void PrintValues()
            {

            }

        }

        public class B:A
        {
            private int x = 1;
            private int y;
            public B()
            {
                y = -1;
            }
            public new  void PrintValues()
            {
                System.Console.WriteLine(string.Format("x={0},y={1}",x,y));
            }
        }

        //static void Main(string[] args)
        //{
 
        //   //StopLoop();

        //    //int i = 3;
        //    //using (TransactionScope scope=new TransactionScope() )
        //    //{
        //    //    i = 9;
                
        //    //}
        //    //System.Console.WriteLine(i);
        //    //  List<int> list = new List<int>(){22,44,24,42,11};


        //    //int i = 0;
        //    //++i;
        //    //System.Console.WriteLine(i);

        //    //for (int i = 0; i < 10; i++)
        //    //{
        //    //    ThreadPool.QueueUserWorkItem(Show);
        //    //}
        //    //short i = 1;
        //    //i = (short)(i + 1);

        //    //var dd=  Regex.IsMatch("ADFFF", "[A-Z]");
        //    // System.Console.WriteLine(dd);
           
 
        //   // List<string> lists = new List<string>();
          
               
             
        //   // int[][] dd = new int[4][];
        //   // dd[0] = new int[] {3};
        //   // dd[1] = new int[] { 1, 2, 2 };
        //   // dd[2] = new int[] { 3, 33, 333, 555 };
        //   // int x= dd.GetUpperBound(0);
        //   // var y= dd.GetLowerBound(0);
        //   // List<int> list = new List<int>() { 4, 3, 55, 22, 33 ,333,222,111,45,46};
        //   //// ButtbleSort(list);
        //   //// int index = list.BinarySearch(33);
        //   // int innum = 55;
        //   // double  outnum = 56;

           
        //   // list.ShellSorting(5);
        //   // System.Console.WriteLine(string.Join(",", list));
        //    //Thread thread = new Thread(new ThreadStart());
        //    //Thread.Sleep();



        //    //System.Console.WriteLine(ConvertBits(2).ToString());
        //    //byte[] bitSet = new byte[] { 1, 2, 3, 4, 5 };
        //    //BitArray bit = new BitArray(bitSet);

        //    //Hashtable hashtable = new Hashtable();


        //    //System.Console.WriteLine(bit.Count);
        //    //for (int i = 0; i < bitSet.Length; i++)
        //    //{

        //    //    System.Console.WriteLine(bit.Get(i));
        //    //}
        //    //byte[] ByteSet = new byte[] { 1, 2, 3, 4, 5 };
        //    //BitArray BitSet = new BitArray(ByteSet);
        //    //for (int bits = 0; bits <= BitSet.Count - 1; bits++)
        //    //   System. Console.Write(BitSet.Get(bits) + "");
        //    // int num=  Convert.ToInt32("8", 10);
        //    //System.Console.WriteLine(Convert.ToString(num,2));
        //    System.Console.Read();
        //    //Child child = new Child();
        //    //ParrelTask parrelTask = new ParrelTask();
        //    //parrelTask.CancellTask();
        //    //System.Console.WriteLine("first process");
        //    // System.Console.Read();
        //    //string dd=   Substitute("${month}_${year}_${day}", (name) => "@" + name);

        //}

     

        private static StringBuilder ConvertBits(int val)
        {
            int dispMask = 1 << 31;


            System.Console.WriteLine(Convert.ToString(dispMask, 2));

            StringBuilder bitBuffer = new StringBuilder(35);
            for (int i = 1; i <= 32; i++)
            {
                if ((val & dispMask) == 0)
                    bitBuffer.Append("0");
                else
                    bitBuffer.Append("1");
                val <<= 1;
                if ((i % 8) == 0)
                    bitBuffer.Append(" ");
            }
            return bitBuffer;
        }


       

        [MethodImpl(MethodImplOptions.Synchronized)]
        static void Show(object os)
        {

            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine(i);
            }

        }


        public static string Substitute(string input, Func<string, string> substitutor, string pattern = @"(\$\{.+?\})")
        {
            if (string.IsNullOrEmpty(input)) return input;

            var buffer = new StringBuilder();
            var matches = Regex.Matches(input, pattern);

            // No replacements?
            if (matches.Count == 0) return input;

            int lastIndex = 0;

            Match match = null;
            for (int ndx = 0; ndx < matches.Count; ndx++)
            {
                match = matches[ndx];
                var name = match.Value.Substring(2, match.Value.Length - 3);
                var replacement = substitutor(name);

                // Append to buffer remaining text not including the variable.
                if (match.Index != 0)
                {
                    var length = match.Index - lastIndex;
                    var outside = input.Substring(lastIndex, length);
                    buffer.Append(outside);
                }
                lastIndex = match.Index + match.Length;
                buffer.Append(replacement);
            }
            if (match.Index + match.Length < input.Length)
            {
                var lastcontent = input.Substring(match.Index + match.Length);
                buffer.Append(lastcontent);
            }
            return buffer.ToString();
        }

        public class parent
        {
            public int dd;
            public parent()
            {
                dd = 1;
            }
        }

        public class Child : parent
        {
            public Child()
            {
                dd = 2;
            }
        }
    }


}

//namespace MonitorCS1
//{
//    internal class MonitorSample
//    {
//        private const int MAX_LOOP_TIME = 1000;
//        private Queue m_smplQueue;

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
//                Monitor.Pulse(m_smplQueue);
//                //Wait in the loop, while the queue is busy.
//                //Exit on the time-out when the first thread stops. 
//                while (Monitor.Wait(m_smplQueue))
//                {
//                    //Pop the first element.
//                    int counter = (int) m_smplQueue.Dequeue();
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

//        private static void Main(string[] args)
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
//        }
//    }
//}
