using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public interface IProductA
    {
        void DoingSomething();
    }

    public interface IProductB
    {
        void DoSomething();
    }

    public class ProductA1 : IProductA
    {
        public void DoingSomething()
        {
             
        }
    }

    public class ProductA2:IProductA
    {
        public void DoingSomething()
        {
            
        }
    }

    public class ProductB1 : IProductB
    {
        public void DoSomething()
        {
             
        }
    }

    public class ProductB2:IProductB
    {
        public void DoSomething()
        {
             
        }
    }

    public class AbstractFactory
    {
        public AbstractFactory()
        {
            
        }
        public IProductA CreateProductA()
        {
           
        }

        public IProductB CreateProductB()
        {

        }
    }
}
