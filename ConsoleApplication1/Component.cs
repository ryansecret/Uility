using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
   public abstract  class Component
   {
       protected IList<Component> children;
        
       public virtual string Name { get; set; }

       public virtual IEnumerable<string> GetNameList()
       {
           yield return Name;
           if (children!=null&&children .Count!=0)
           {
               foreach (var item in children)
               {
                   foreach (var child in item.GetNameList())
                   {
                       yield return child;
                   }
               }
           }
       }

       public Component AddChildren(Component child)
       {
           children.Add(child);
           return this;
       }


       // 获取 IEnumerator 
       public static IEnumerator GetEnumerator(object data)
       {
           if (data == null) throw new NullReferenceException();
           Type type = data.GetType();

           // 是否为 Stack
           if (type.IsAssignableFrom(typeof(Stack))
               || type.IsAssignableFrom(typeof(Stack<string>)))

               return null;
           else
           {
               return null;
           }

          
       }
   }
}
