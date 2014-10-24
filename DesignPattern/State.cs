using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//订单的状态分为新建、处理后状态和取消状态
//状态模式允许对象在内部状态改变时改变她的行为
namespace DesignPattern
{
    public class Order
    {
        public IOrderStatus OrderStatus;
        public Order(IOrderStatus status )
        {
           
            this.OrderStatus = status;
        }

        public OrderStatus Status { get { return OrderStatus.Status; } }
        
        public bool Cancancel()
        {
            return OrderStatus.CanCancel(this);
        }

        public void Cancel()
        {
            if (Cancancel())
            {
                
                OrderStatus.Cancel(this);
            }
        }

        public bool CanProcess()
        {
            return OrderStatus.CanProcess(this);
        }
        public void Process()
        {
            if (CanProcess())
            {
                OrderStatus.Process(this);
            }
            
        }
        public void ChangeStatus(IOrderStatus status )
        {
            this.OrderStatus = status;
        }
    }

   public  interface IOrderStatus
   {
       bool CanProcess(Order order);
       void Process(Order order);
       void Cancel(Order order);
       bool CanCancel(Order order);
       OrderStatus Status { get; }
   }

    public enum OrderStatus
    {
        New=0,
        processed=1,
        Cancel=2
    }

    public class NewState:IOrderStatus
    {
        
 
        public bool CanProcess(Order order)
        {
            return true;
        }

        public void Process(Order order)
        {
           order.ChangeStatus(new ProcessedStatus());
        }

        public void Cancel(Order order)
        {
             order.ChangeStatus(new CancellStatus());
        }

        public bool CanCancel(Order order)
        {
            return true;
        }

        public OrderStatus Status 
        {
           get{return OrderStatus.New;}  
        }
    }

    public class ProcessedStatus:IOrderStatus
    {
        public bool CanProcess(Order order)
        {
            return true;
        }

        public void Process(Order order)
        {
            //todo:实现处理过程
        }

        public void Cancel(Order order)
        {
           
        }

        public bool CanCancel(Order order)
        {
            return false;
        }

        public OrderStatus Status { get{return OrderStatus.processed;}}
    }

    public class CancellStatus:IOrderStatus
    {
        public bool CanProcess(Order order)
        {
            return false;
        }

        public void Process(Order order)
        {
        }

        public void Cancel(Order order)
        {
        }

        public bool CanCancel(Order order)
        {
            return false;
        }

        public OrderStatus Status { get{return OrderStatus.Cancel;} }
    }
}
