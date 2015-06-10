using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Windows.Data;
namespace Silverlight.Validate
{
    public class Person : INotifyDataErrorInfo 
    {
        private int age;
        [Required]
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string name]
        {
            get
            {
                string result = null;
                List<string> list = new List<string>();
                AssemblyPart part = new AssemblyPart();
                
                if (name == "Age")
                {
                    if (this.age < 0 || this.age > 150)
                    {
                        result = "Age must not be less than 0 or greater than 150.";
                    }
                }
                return result;
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            yield break;
        }

        public bool HasErrors { get; private set; }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
