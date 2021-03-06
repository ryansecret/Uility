﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Runtime;
using System.Configuration;
using System.Xml;

namespace Silverlight.Example
{   

    //note:空接口的目的
    public class Utility
    {
        
        public void BeginInvoke(Delegate method, object arg)
        {
            //XmlWriter writer = XmlWriter.Create();
            //writer.WriteBase64();
            //if (Deployment.Current != null)
            //{
          
            //    Deployment.Current.Dispatcher.BeginInvoke(method, arg);
            //}
        }
        //todo:注意附加属性的使用
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dependencyObject"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static T GetOrAddValue<T>(DependencyObject dependencyObject, DependencyProperty property) where T : class, new()
        {
            T value = dependencyObject.GetValue(property) as T;
            if (value == null)
            {
                value = new T();
                dependencyObject.SetValue(property, value);
            }

            return value;
        }
    }
}
