using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{

    public interface IEmployee 
    {
        double Income { get; set; }
        int Vacations { get; set; }
        void AcceptVisitor(IVisitor visitor);
    }
    public class Employee:IEmployee
    {
        public double Income { get; set; }
        public int Vacations { get; set; }
        public virtual void AcceptVisitor(IVisitor visitor)
        {
            visitor.VisitEmployee(this);
        }
    }
    public class Manager:Employee
    {
        public override void AcceptVisitor(IVisitor visitor)
        {
             visitor.VisitManager(this);
        }
    }
    public interface IVisitor
    {
        void VisitEmployee(IEmployee employee );
        void VisitManager(Manager manager);
    }

    /// <summary>
    /// 1经常为一个固定结构的对象添加一些新的操作
    /// 2 需要用一个独立的类型来组织一批不相干的操作
    /// </summary>
    public  class VisitorVacation:IVisitor 
    {
        public void VisitEmployee(IEmployee employee)
        {
        }

        public void VisitManager(Manager manager)
        {

        }
    }

    public class VisitorSalary:IVisitor
    {
        public void VisitEmployee(IEmployee employee)
        {
        }

        public void VisitManager(Manager manager)
        {

        }
    }
}
