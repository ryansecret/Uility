using System;
using System.Reflection;
using System.Collections.Generic;
namespace MarvellousWorks.PracticalPattern.Concept.Attributing
{
    // Builder 抽象行为定义
    public interface IAttributedBuilder
    {
        IList<string> Log { get;}      // 记录 Builder 的执行情况
        void BuildPartA();
        void BuildPartB();
        void BuildPartC();
    }

    [Director(3, "BuildPartA")]
    [Director(2, "BuildPartB")]
    [Director(1, "BuildPartC")]
    public class AttributedBuilder : IAttributedBuilder
    {
        private IList<string> log = new List<string>();
        public IList<string> Log { get { return log; } }

        public void BuildPartA() { log.Add("a"); }
        public void BuildPartB() { log.Add("b"); }
        public void BuildPartC() { log.Add("c"); }
    }

    // 通过 attribute 扩展 Director
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true)]
    public sealed class DirectorAttribute : Attribute, IComparable<DirectorAttribute>
    {
        private int priority;    // 执行优先级
        private string method;

        public DirectorAttribute(int priority, string method)
        {
            this.priority = priority;
            this.method = method;
        }

        public int Priority { get { return this.priority; } }
        public string Method { get { return this.method; } }

        // 提供按照优先级比较的 ICompare<T> 实现, 由于Array.Sort<T>
        // 实际是升序排列，而 Array.Reverse 是完全反转，因此这里调
        // 整了比较的方式为“输入参数优先级 - 当前实例优先级” 
        public int CompareTo(DirectorAttribute attribute)
        {
            return attribute.priority - this.priority;
        }
    }


    public class Director
    {
        public void BuildUp(IAttributedBuilder builder)
        {
            // 获取 Builder 的 DirectorAttribute 属性
            object[] attributes = 
                builder.GetType().GetCustomAttributes(typeof(DirectorAttribute), false);
            if (attributes.Length <= 0) return;
            DirectorAttribute[] directors = new DirectorAttribute[attributes.Length];
            for (int i = 0; i < attributes.Length; i++)
                directors[i] = (DirectorAttribute)attributes[i];

            // 按每个 DirectorAttribute 优先级逆序排序后，逐个执行
            Array.Sort<DirectorAttribute>(directors);
            foreach (DirectorAttribute attribute in directors)
                InvokeBuildPartMethod(builder, attribute);
        }

        // helper method : 按照 DirectorAttribute 的要求，执行相关的 Builder 方法
        private void InvokeBuildPartMethod(
            IAttributedBuilder builder, DirectorAttribute attribute)
        {
            switch (attribute.Method)
            {
                case "BuildPartA": builder.BuildPartA(); break;
                case "BuildPartB": builder.BuildPartB(); break;
                case "BuildPartC": builder.BuildPartC(); break;
            }
        }
    }
}
