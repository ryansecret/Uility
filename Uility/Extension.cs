﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Uility
{
    /// <summary>
    /// 
    /// </summary>
    public static class ComparableExtensions
    {

        /// <summary>
        /// Betweens the specified actual.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual">The actual.</param>
        /// <param name="lower">The lower.</param>
        /// <param name="upper">The upper.</param>
        /// <returns></returns>
        public static bool Between<T>(this T actual, T lower, T upper) where T : IComparable<T>
        {
    

            return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) < 0;
        }


        /// <summary>
        ///  通过泛型制定了约束
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="values">The values.</param>
        public static void AddRange<T, S>(this ICollection<T> list, params S[] values)
    where S : T
        {
            foreach (S value in values)
                list.Add(value);
        }

        public static int? Compare<T>(T first, T second)
        {
            return Compare(Comparer<T>.Default, first, second);
        }

       
        public static int? Compare<T>(IComparer<T> comparer, T first, T second)
        {
            int ret = comparer.Compare(first, second);
            if (ret == 0)
            {
                return null;
            }
            return ret;
        }


        public static IEnumerable<string> Get()
        {
            Converter<string, string> dd = new Converter<string, string>(input => input);
              
                yield return string.Empty;
            yield break;
        }

        
         
    }

    class Test:IComparable<int>
    {
        public  EventHandler<EventArgs> handler;
        public int CompareTo(int other)
        {

             
            return 0;
        }
    }


    internal class OperatorTest
    {
        public delegate void Show();
        private EventHandler<EventArgs> handler;

        public static implicit operator OperatorTest(int i)
        {
            return new OperatorTest();
        }
        public static OperatorTest operator +(OperatorTest test,Test ss)
        {
            test.handler += ss.handler;
            return test;
        }

     
    }
 

    /// <summary>
    /// 控制调试时的显示信息
    /// </summary>
    [DebuggerDisplay("FirstName={FirstName}, LastName={LastName}")]
    class Customer : IEqualityComparer,IComparer
    {
        [XmlIgnore]
        public string FirstName;
        
        public string LastName;


        /// <summary>
        /// 相当于#if  #endif
        /// </summary>
        [DebuggerStepThrough]
        [Conditional("DEBUG")]
        static void DebugMethod()
        {

        }

        public bool Equals(object x, object y)
        {
            ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();
            lockSlim.EnterWriteLock();

            throw new NotImplementedException();
        }

        public int GetHashCode(object obj)
        {
            
          return   FirstName.GetHashCode() & LastName.GetHashCode();
        }

        public int Compare(object x, object y)
        {
            Convert.ToByte("ss", 16);
            throw new NotImplementedException();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }


    }

    [FlagsAttribute]
    enum MultiHue : short
    {
        Black = 0,
        Red = -3,
        Green = 2,
        Blue = 4
    };


    public static class LinqExtension
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }
        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                
                expr = Expression.Property(expr, pi);
              
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}
