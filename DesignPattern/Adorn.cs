using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPattern
{

    /// <summary>
    /// 装饰模式重要的不是set而是get
    /// </summary>
    public class Player
    {
        public  string Name { get; set; }
        public IBaseArmer Armer { get; set; }
    }


    public interface IBaseArmer
    {
        int Defence { get;   }
        int Attack { get;   }
        string Material { get; }
    }

    public class BaseArmer : IBaseArmer
    {
        public int Defence
        {
            get { return 1; }

        }

        public int Attack { get { return 1; }   }

        public string Material
        {
            get { return "布衣"; }
        }

       
    }

    public class ThroneArmer : IBaseArmer
    {
        private IBaseArmer armer;

        public ThroneArmer(IBaseArmer armer )
        {
      
            this.armer = armer;
        }
        public int Defence
        {
            get {return  armer.Defence*2; }
        }

        public int Attack
        {
            get { return armer.Attack*2; }
        }
        public string Material
        {
            get { return "青铜"; }
        }
    }

    public class  Program
    {
        void process()
        {
            IBaseArmer armer = new BaseArmer();
            armer =new ThroneArmer(armer);
            
        }
    }


}
