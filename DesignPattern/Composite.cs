using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{

    #region 

    public abstract class Component
    {
        protected List<Component> Children;

        public string Name { get; set; }

        public virtual void Add(Component child)
        {
            Children.Add(child);
        }

        public virtual void Remove(Component child)
        {
            Children.Add(child);
        }

        public IEnumerable<string> GetNameList()
        {
            yield return Name;
            if (Children != null && Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    foreach (var name in child.GetNameList())
                    {
                        yield return name;
                    }

                }
            }
        }
    }

    public class Leaf : Component
    {
        public override void Add(Component child)
        {
            throw new NotSupportedException();
        }

        public override void Remove(Component child)
        {
            throw new NotSupportedException();
        }
    }

    public class Composit : Component
    {
        public Composit()
        {
            Children = new List<Component>();
        }
    }

    #endregion

    public class Customer
    {
        public double LateFees { get; set; }

        public int TotalRentNumber { get; set; }

        public string Name { get; set; }


        public bool IsSatisfy()
        {
            HasLateFees hasLate = new HasLateFees();
            HanMax hanMax = new HanMax();

            return hasLate.Not().And(hanMax.Not()).IsSatisfy(this);
        }
    }

    public interface ISpecification<T>  
    {
        ISpecification<T> And(ISpecification<T>  specification);
        ISpecification<T> Not();
        bool IsSatisfy(T t);
    }

    public abstract class Composite : ISpecification<Customer>  
    {
        public ISpecification<Customer> And(ISpecification<Customer> specification)
        {
            return new AndSpecification(this,specification);
        }

        public ISpecification<Customer> Not()
        {
            return new NotSpecification(this);
        }

        
        public abstract bool IsSatisfy(Customer t);

    }

    public class AndSpecification:Composite 

    {
        private ISpecification<Customer> lSpecification;
        private ISpecification<Customer> rSpecification;
        public AndSpecification(ISpecification<Customer> lSpecification,ISpecification<Customer> rSpecification )
        {
            this.lSpecification = lSpecification;
            this.rSpecification = rSpecification;
        }

 
        public override bool IsSatisfy(Customer t)
        {
            return lSpecification.IsSatisfy(t)&&rSpecification.IsSatisfy(t);
        }
    }

    public class NotSpecification : Composite 
    {
        private Composite  sComposite; 
        public NotSpecification(Composite  sComposite)
        {
            this.sComposite = sComposite;
        }
 
        public override bool IsSatisfy(Customer t)
        {
            return !sComposite.IsSatisfy(t);
        }
    }

    public class HasLateFees : Composite
    {
        public override bool IsSatisfy(Customer t)
        {
            return t.LateFees > 20;
        }
    }

    public class HanMax:Composite
    {
        public override bool IsSatisfy(Customer t)
        {
            return t.TotalRentNumber > 10;
        }
    }
}
