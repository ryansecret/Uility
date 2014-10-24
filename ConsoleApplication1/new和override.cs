using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
namespace ConsoleApplication1
{
    //internal class Program
    //{
    //    private static void Main(string[] args)
    //    {
    //        Class2 o = new Class2();

    //        Console.WriteLine(o.MethodA());
    //        //A a = new B();
    //        //a.MethodA();
    //        //a.Method();
    //        //B b = new B();
    //        //b.Method();
    //        //b.MethodA();
    //        //A aa= new A();
    //        //aa.MethodA();
    //        //aa.Method();
    //        Console.Read();
    //    }
    //}

    //public class A
    //{
    //    public virtual void Method()
    //    {
    //        Console.WriteLine("This Method in Class A!");
    //    }

    //    public virtual void MethodA()
    //    {
    //        Console.WriteLine("This Method in Class A!");
    //    }
    //}

    //public class B : A
    //{
    //    public new void Method()
    //    {
    //        Console.WriteLine("This Method in Class B!");
    //    }

    //    public override void MethodA()
    //    {
    //        Console.WriteLine("This Method in Class B!");
    //    }
    //}

    //internal abstract class BaseClass
    //{

    //    public virtual void MethodA()
    //    {

    //    }

    //    public virtual void MethodB()
    //    {

    //    }

    //}

    //internal class Class1 : BaseClass
    //{

    //    public   void MethodA( )
    //    {

    //    }

    //    public override void MethodB()
    //    {

    //    }

    //}

    //internal class Class2 : Class1
    //{

    //    public new void MethodB()
    //    {

    //    }

    //}
    class A
    {
        private CultureInfo cultureInfo = new CultureInfo("en-us");
        void ss()
        {
            var dd = string.Format(cultureInfo, "{0:c}", "123");
        }

        public static int X;
        static A()
        {
           
            //未初始化之前的值为0
            X = B.Y + 1;
        }
    }
    class B
    {
        public static int Y = A.X + 1;
        static B() { }
        static void Main()
        {
            Console.WriteLine("X={0},Y={1}", (int)test.A, (int)test.B);
            Console.Read();
        }
    }

    internal enum test
    {
        A,B,C=1
    }

}
