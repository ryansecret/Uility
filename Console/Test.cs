using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
 

namespace Consoles
{
    
    public interface ITestBase
    {
        int Count { get; set; }
        decimal Uniprice { get; set; }
        decimal Total { get; }
        decimal Tax { get; }
    }

    public abstract  class TestBase : ITestBase
    {
        public int Count { get; set; }
        public decimal Uniprice { get; set; }


        public decimal Total { get { return Tax + Uniprice*Count; } }

        public abstract decimal Tax { get; }
        
    }

    public   class Basic : TestBase 
    {
        public override decimal Tax
        {
            get { return 0; }
        }
 
    }

    public class Imported : Basic
    {
        public override decimal Tax
        {
            get
            {
                
                return base.Tax+33;
            }
        }
    }

    public class TaxItemFactory
    {
        private static Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
        static TaxItemFactory()
        {
           dictionary.Add("imported",typeof(Imported));
        }

        public TestBase CreatTaxItem(string type)
        {
            return Activator.CreateInstance(dictionary[type]) as TestBase;
        }
          
    }

    public enum TaxType
    { book,medical,cos}

    

    public class SingleTon
    {
 
        public static readonly SingleTon singleTon = new SingleTon();
      
        private SingleTon()
        {
       
        }
    }

    public class Singleton
    {
        public static volatile  Singleton _instance = null;
        private Singleton()
        {
           
        }

        public static Singleton Instance
        {
            get
            {
                if (_instance==null)
                {
                    lock (typeof(Singleton))
                    {
                        if (_instance==null)
                        {
                            _instance = new Singleton();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
