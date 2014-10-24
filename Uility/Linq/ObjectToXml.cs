﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Uility.Linq
{
    public static  class ObjectToXml
    {
        public static IEnumerable<XElement> AsXElements(this object source)
        {
            foreach (PropertyInfo prop in source.GetType().GetProperties())
            {
                object value = prop.GetValue(source, null);
                yield return new XElement(prop.Name.Replace("_", "-"), value);
            }
        }

     
        public static IEnumerable<XAttribute> AsXAttributes(this object source)
        {
            foreach (PropertyInfo prop in source.GetType().GetProperties())
            {
                object value = prop.GetValue(source, null);
                yield return new XAttribute(prop.Name.Replace("_", "-"), value ?? "");
            }
        }
    }
}
