using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
      interface IAdapter
      {
          void Request();
      }

    abstract class AdpterBase:IAdapter
    {
        public void Request()
        {
           IAdaptee adaptee = GetAdaptee();
           adaptee.SpecialRequest();
        }

        public abstract IAdaptee GetAdaptee();
    }

    class AdapterA:AdpterBase
    {
        public override IAdaptee GetAdaptee()
        {
            return null;
        }
    }

    class AdapterB:AdpterBase
    {
        public override IAdaptee GetAdaptee()
        {
            return null;
        }
    }

    internal interface IAdaptee
    {
        void SpecialRequest();
    }

    internal class Adaptee : IAdaptee
    {
        public void SpecialRequest()
        {
        }
    }
}
