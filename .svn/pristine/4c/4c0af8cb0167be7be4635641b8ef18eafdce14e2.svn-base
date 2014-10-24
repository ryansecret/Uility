using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mef
{
    using System.ComponentModel.Composition;

    public  class InheritedImprot
    {

        [Import]
        public IUserServie UService { get; set; }

        public InheritedImprot()
        {
             
            string name = UService.GetUserName();
        }

    }

   [InheritedExport]
   public interface IUserServie
   {
       string GetUserName();
   }
   public class UserService : IUserServie
   {
       public string GetUserName()
       {
           return "张三";
       }
   }

}
