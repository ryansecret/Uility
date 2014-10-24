using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI.WebControls;
using Microsoft.Win32;
using System.Xml;
namespace DesignPattern
{
    public interface IState
    {
        bool Euqals(IState state);
    }

    public interface IText 
    {
        string Content { get; }
        string GetMothed();
        IState State { get; set; }
        T Create<T>() where T : class;
    }
    /// <summary>
    /// 抽象类的接口实现可以使抽象的
    /// </summary>
    public abstract class Text : IText
    {
        public abstract int Count { get; set; }
        /// <summary>
        /// 抽象类的转换
        /// </summary>
        private IText text;

        public Text(IText text)
        {
            
            this.text = text;
        }

        public abstract string Content { get; }
        public string GetMothed()
        {
            return "test";
        }
       
        private IState _state;
        public virtual  IState State
        {
            get { return _state; }
            set { _state = value; }
        }

        public virtual  T Create<T>() where T : class
        {
            return Activator.CreateInstance<T>();
        }

        public virtual int GetProperty()
        {
              
            return 0;
        }

        public string this[string ss] { get { return string.Empty; } }

        private float[] floats = new float[2] {1, 2};
        //note:通过委托传递索引规则
        public float this[Predicate<float> predicate] { get {  return Array.FindAll(floats,predicate).First(); } }
    }

    public class ConcreateText : Text
    {
        public ConcreateText(IText text) : base(text)
        {
            string a = "abcdefghigk";
           var dd=  a.ToCharArray();
            Encoding.UTF8.GetBytes(a);
            char[] s = new char[10000];

        }

        public override int Count { get; set; }

        public override string Content
        {
            get { return GetMothed(); }
        }
        public override int GetProperty()
        {
            return base.GetProperty();
        }

        
    }

    public class dd:IEnumerable
    {
        
        public IEnumerator GetEnumerator()
        {
            yield return 1;
        }
       
        public int this[int index] { get { return 1; } }

        public int this[string index] { get { return 1; } }
        
        public int this[Predicate<string> predicate]{get { return 1; }}
        protected internal string df { get; set; }
        void T()
        {
            try
            {

            }

            catch (FileNotFoundException e1)
            {

            }

            catch (Exception e2)
            {

            }

             

            catch
            {

            } 
        }
    }

     
}
