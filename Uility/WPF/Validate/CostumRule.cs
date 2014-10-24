using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using ValidationResult = System.Windows.Controls.ValidationResult;

namespace Uility.WPF.Validate
{
    class CostumRule:ValidationRule 
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int number;
            if (!int.TryParse((string)value, out number))
            {
                return new ValidationResult(false, "输入的内容必须为数字！");
            }
            else if (number > Max || number < Min)
            {
                return new ValidationResult(false, "输入的年龄超过范围");
            }
            else
            {
              var attrib = (DisplayAttribute)Attribute.GetCustomAttribute(
             Assembly.GetExecutingAssembly(), typeof(DisplayAttribute));
                var desc = attrib == null ? "" : attrib.GetDescription();
                //return new ValidationResult(true, null);
                return ValidationResult.ValidResult;
            }
        }
       

    }
}
