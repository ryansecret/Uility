using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{

    interface ITarget
    {
        void Process();
    }
    class Proxy:ITarget
    {
        public void Process()
        {
            RealSubject.Singleton.Process();
        }
    }
    class RealSubject:ITarget
    {
        public void Process()
        {
        }
        //模拟复杂性
        static readonly ITarget singleton = new RealSubject();
        private  RealSubject()
        {
            
        }
        public static ITarget Singleton { get { return singleton; } }
    }

    partial class Client
    {
        void Do()
        { 
            target.Process();
        }
        ITarget target=new Proxy();
        
    }
}
