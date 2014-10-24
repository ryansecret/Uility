using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Uility.Example.异步
{
    class CallBackAsync
    {
        public delegate int TwoOperands(int a, int b); 

        static void Main(string[] args)
        {
            Console.WriteLine("Main is running on thread " + Thread.CurrentThread.ManagedThreadId);
            TwoOperands operation = new TwoOperands(Add);
            //async calling the add. 
            //new System.AsyncCallback(CallbackHandler) 回调处理函数, 异步调用结束后，runtime会调用该函数。
            // "Async parameter" 作为回调参数，传给回调函数，通过 AsyncResult.AsyncState 获取
            operation.BeginInvoke(2, 3, new System.AsyncCallback(CallbackHandler), "Async parameter");
            Console.WriteLine("Main is running on thread " + Thread.CurrentThread.ManagedThreadId);
            Console.Read();
        }

        /// <summary>
        /// 回调处理函数
        /// </summary>
        /// <param name="iar">回调参数</param>
        static void CallbackHandler(IAsyncResult iar)
        {
            
            Console.WriteLine("CallbackHandler is running on thread " + Thread.CurrentThread.ManagedThreadId);
            AsyncResult ar = (AsyncResult)iar;
            // 获取原委托对象。
            TwoOperands operation = (TwoOperands)ar.AsyncDelegate;
            // 结束委托调用。
            int i = operation.EndInvoke(iar);
            // 打印异步调用传入的参数。
            Console.WriteLine("The Async calling parameter is " + ar.AsyncState);
            Console.WriteLine("The adding result is " + i);
        }

        static int Add(int i, int j)
        {
            Console.WriteLine("Add is running on thread " + Thread.CurrentThread.ManagedThreadId + "...");
            Thread.Sleep(5000);
            return i + j;
        }


    }
}
