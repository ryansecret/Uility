using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{
    public interface IMState
    {
        
    }

    public interface IMemo<T> where T : IMState
    {
        T State { get; set; }
    }

    public class MemoBase<T> : IMemo<T> where T : IMState
    {
        private T _state;
        public T State

        {
            get { return _state; }
            set { _state = value; }
        }
    }

    public interface IOriginal<T, M>
        where T : IMState
        where M : IMemo<T>, new()
    {
        IMemo<T> Memo { get; set; } 
    }

    public class OriginalBase<T,M>:IOriginal<T,M> where T : IMState
        where M : IMemo<T>, new()
    {
        protected T State;
        public virtual  IMemo<T> Memo 
        { get { M m = new M();
            m.State = this.State;
            return m;
        }  
            set { this.State = value.State; }
        }
    }
}
}
