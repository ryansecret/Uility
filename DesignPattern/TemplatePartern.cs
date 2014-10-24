using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    internal interface IAbstract
    {
        int Quantity { get;   }

        int Total { get;   }

        Double Average { get;   }
    }

    public abstract class BaseAbstract:IAbstract
    {
        public abstract int Quantity { get; }
        public abstract int Total { get;   }

        public double Average
        {
            get { return (Double)Total/Quantity; }
        }
    }

    public class ArrayData:BaseAbstract
    {
        private int[] data = new int[3]{1,2,3};
       
        public override int Quantity
        {
            get { return data.Length; }
        }

        public override int Total
        {
            get
            {
                int result=0;
                foreach (var i in data)
                {
                    result += i;
                }
                return result;
            }
        }
    }

    public class ListData:BaseAbstract
    {
        public override int Quantity
        {
            get { return 0; }
        }

        public override int Total
        {
            get { return 0; }
        }
    }
}
