using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    //note:Aop机制拦截的实现是通过在属性和方法打属性标签来实现的
    [AttributeUsage(AttributeTargets.Class,AllowMultiple=true)]
    class MyAttribure:Attribute
    {
         
        private int Consequece;

        public MyAttribure(int order)
        {
             
            this.Consequece = order;
        }
    }
    [MyAttribure(3)]
    public class Test
    {

    }
}
