using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DesignPattern
{
 
    public class Context
    {
        private IList<IInterpret> _interprets = new List<IInterpret>();
        public List<string> sql=new List<string>();
        public string Sql
        {
            get
            {
                foreach (var interpret in Interprets)
                {
                     interpret.Interpret(this);
                }
                return string.Join("",sql);
            }
        }

        public IList<IInterpret> Interprets
        {
            get { return _interprets; }
            set { _interprets = value; }
        }

        
    }

    public interface IInterpret
    {
        void Interpret(Context context );
    }
    public class Expression:IInterpret
    {
        public Property Property { get; set; }
        public Equlity Equlity { get; set; }
        public ValueInterpret Value { get; set; }
        public void Interpret(Context context)
        {
            Property.Interpret(context);
            Equlity.Interpret(context);
            Value.Interpret(context);
        }
    }
    

    public  class Property:IInterpret
    {
        public string ShowText { get; set; }
        public string SqlName { get; set; }
        

        public void Interpret(Context context)
        {
            context.sql.Add(SqlName);
        }
    }

    public class ValueInterpret : IInterpret
    {
        public Type DataType { get; set; }
        public string Value { get; set; }
        public void Interpret(Context context)
        {
            if (DataType.Name.ToLower().Equals("string"))
            {
                context.sql.Add(string.Format("'{0}'", Value));
            }
            else
            {
                context.sql.Add(Value);
            }
        }
    }

    public class Equlity:IInterpret
    {
        public string ShowText { get; set; }
        public void Interpret(Context context)
        {
            switch (ShowText)
            {
                case "大于":
                    context.sql.Add(">");
                    break;
                case "小于":
                    context.sql.Add("<");break;
                case "等于":
                    context.sql .Add("=");
                    break;

            }
        }
    }
}
