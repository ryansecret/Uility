using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Management.Instrumentation;
namespace Consoles
{
    public class Class1:IEquatable<Class1>
    {
      void test()
      {
         
          //DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory();
          //ConfigurationManager.ConnectionStrings[0].
          //dbProviderFactory.CreateCommand() 
      }
 
        public bool Equals(Class1 other)
        {
            return false;
        }
    }
}
