using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DesignPattern
{

    public enum PurchaseType
    {
        Internal,Discount,Regular,Mail
    }

    public class Request
    {
        private double _price;
        private PurchaseType _purchaseType;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public PurchaseType PurchaseType
        {
            get { return _purchaseType; }
            set { _purchaseType = value; }
        }

        public Request(double price,PurchaseType purchaseType )
        {
            this._price  = price;
            this._purchaseType = purchaseType;
        }
    }

    public interface ICor
    {
        void HandleRequest(Request request);
        ICor Sucessor { get; set; }
        PurchaseType CanHandledType { get; set; }
    }

    /// <summary>
    /// 链式模式通过遍历链表找到一个合适的处理操作
    /// </summary>
    public abstract class CorBase:ICor
    {
        private ICor _sucessor;
        private PurchaseType _canHandledType;

        public CorBase(PurchaseType type)
        {
            this._canHandledType = type;
        }
        public void HandleRequest(Request request)
        {
            if (_canHandledType==request.PurchaseType )
            {
                Process(request);
            }
            else
            {
                if (Sucessor!=null)
                {
                     Sucessor.HandleRequest(request);
                }
            }
        }

        public abstract void Process(Request request);
        public ICor Sucessor
        {
            get { return _sucessor; }
            set { _sucessor = value; }
        }

        public PurchaseType CanHandledType
        {
            get { return _canHandledType; }
            set { _canHandledType = value; }
        }
    }

    public class DiscountHandler:CorBase
    {
        public DiscountHandler() : base(PurchaseType.Discount)
        {
        }

        public override void Process(Request request)
        {
            request.Price *= 0.8;
        }
    }

    public class InternalHandler:CorBase
    {
        public InternalHandler( ) : base(PurchaseType.Internal)
        {
        }

        public override void Process(Request request)
        {
            request.Price *= 0.5;
        }
    }

    public class CorTest
    {
        void Test()
        {
            ICor  discount = new DiscountHandler();
            discount.Sucessor = new InternalHandler();
            Request request = new Request(20, PurchaseType.Internal);
            discount.HandleRequest(request);
        }
    }

}
