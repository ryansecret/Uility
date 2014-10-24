using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace structure
{
    class Program
    {
        class nodes<T>
        {
            T data;
            nodes<T> Lnode, rnode, pnode;
            public T Data
            {
                get { return data; }
                set { data = value; }
            }
            public nodes<T> LNode
            {
                get { return Lnode; }
                set { Lnode = value; }
            }
            public nodes<T> RNode
            {
                get { return rnode; }
                set { rnode = value; }
            }
            public nodes<T> PNode
            {
                get { return pnode; }
                set { pnode = value; }
            }
            public nodes() { }
            public nodes(T data)
            {
                this.data = data;
            }
        }

        //构造一棵已知的二叉树   
        static nodes<string> BinTree()
        {
            nodes<string>[] binTree = new nodes<string>[8];

            //创建节点   
            binTree[0] = new nodes<string>("A");
            binTree[1] = new nodes<string>("B");
            binTree[2] = new nodes<string>("C");
            binTree[3] = new nodes<string>("D");
            binTree[4] = new nodes<string>("E");
            binTree[5] = new nodes<string>("F");
            binTree[6] = new nodes<string>("G");
            binTree[7] = new nodes<string>("H");

            //使用层次遍历二叉树的思想，构造一个已知的二叉树   
            binTree[0].LNode = binTree[1];
            binTree[0].RNode = binTree[2];
            binTree[1].RNode = binTree[3];
            binTree[2].LNode = binTree[4];
            binTree[2].RNode = binTree[5];
            binTree[3].LNode = binTree[6];
            binTree[3].RNode = binTree[7];

            //返回二叉树根节点   
            return binTree[0];
        }

        //先序遍历   
        static void PreOrder<T>(nodes<T> rootNode)
        {
            if (rootNode != null)
            {
                Console.WriteLine(rootNode.Data);
                PreOrder<T>(rootNode.LNode);
                PreOrder<T>(rootNode.RNode);
            }
        }
        //中序遍历二叉树     
        static void MidOrder<T>(nodes<T> rootNode)
        {
            if (rootNode != null)
            {
                MidOrder<T>(rootNode.LNode);
                Console.WriteLine(rootNode.Data);
                MidOrder<T>(rootNode.RNode);
            }
        }

        //后序遍历二叉树    
        static void AfterOrder<T>(nodes<T> rootNode)
        {
            if (rootNode != null)
            {
                AfterOrder<T>(rootNode.LNode);
                AfterOrder<T>(rootNode.RNode);
                Console.WriteLine(rootNode.Data);
            }
        }
        //层次遍历二叉树          
        static void LayerOrder<T>(nodes<T> rootNode)
        {
            nodes<T>[] Nodes = new nodes<T>[20];
            int front = -1;
            int rear = -1;
            if (rootNode != null)
            {
                rear++;
                Nodes[rear] = rootNode;
            }
            while (front != rear)
            {
                front++;
                rootNode = Nodes[front];
                Console.WriteLine(rootNode.Data);
                if (rootNode.LNode != null)
                {
                    rear++;
                    Nodes[rear] = rootNode.LNode;
                }
                if (rootNode.RNode != null)
                {
                    rear++;
                    Nodes[rear] = rootNode.RNode;
                }
            }
        }



       

   
        static void GetChildren(IList<nodes<string>> list ,nodes<string> root)
        {
       
            if (root.LNode!=null)
            {
                GetChildren(list,root.LNode);
            }
            list.Add(root);
            if (root.RNode!=null)
            {
                GetChildren(list,root.RNode);
            }
            
        }

        //测试的主方法    
        static void Main(string[] args)
        {
            nodes<string> rootNode = BinTree();
            Console.WriteLine("先序遍历方法遍历二叉树：");
            PreOrder<string>(rootNode);
            Console.WriteLine("中序遍历方法遍历二叉树：");
            MidOrder<string>(rootNode);
            Console.WriteLine("后序遍历方法遍历二叉树：");
            AfterOrder<string>(rootNode);
            Console.WriteLine("层次遍历方法遍历二叉树：");
            LayerOrder<string>(rootNode);

            Console.WriteLine("test");
            IList<nodes<string>> list=new List<nodes<string>>();
            GetChildren(list, rootNode);
            foreach (var s in list)
            {
                Console.WriteLine(s.Data); 
            }
            Console.Read();
        }
    }

     
}
