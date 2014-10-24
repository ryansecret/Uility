using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Xml;
namespace DesignPattern
{
    public interface IProduct
    {
        string Name { get; set; }
    }

    public class ProductA:IProduct
    {
        public string Name { get; set; }
    }
    public class ProductB:IProduct
    {
        public string Name { get; set; }
    }

    public interface IFactory
    {
        IProduct Create();
    }

    public class FactoryA:IFactory
    {
        public IProduct Create()
        {
            return new ProductA();
        }
    }

    public class FactoryB:IFactory
    {
        public IProduct Create()
        {
            return new ProductB();
        }
    }

    public partial class Client
    {
        void Test(string productName)
        {
            IFactory factory = (new Assembly()).Create<IFactory>();
            IProduct product  = factory.Create();
        }
    }

    public   class Assembly
    {
        public static IDictionary<Type, Type> dictionary = new Dictionary<Type, Type>();

        public const string SectionName = "CustomFactory";
        static Assembly()
        {
            NameValueCollection collection =
                System.Configuration.ConfigurationManager.GetSection("SectionName") as NameValueCollection;
            Debug.Assert(collection != null, "collection != null");
            for (int i = 0; i < collection.Count; i++)
            {
                string target = collection.GetKey(i);
                string source = collection[i];
                dictionary.Add(Type.GetType(target), Type.GetType(source));
            }
        }

        public object Create(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T))  ;
        }
    }

    public interface IAbstractFactory
    {
        T Create<T>() where T : class;
    }

    public abstract class AbstractFactoryBase:IAbstractFactory
    {
        private IDictionary<Type, Type> mapper; 
        public AbstractFactoryBase(IDictionary<Type,Type> dic )
        {
            this.mapper = dic;
        }
        public T Create<T>() where T : class
        {
            if (mapper==null||mapper.Count==0||!mapper.ContainsKey(typeof(T)))
            {
                throw  new Exception(typeof(T).ToString());
            }
            else
            {
               return  Activator.CreateInstance(mapper[typeof (T)]) as T;
            }
        }
    }
}
