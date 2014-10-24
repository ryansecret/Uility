using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;
using System.Threading;
using System.Xml;

namespace DataStucture
{
    internal class Program
    {
        #region Nested type: A
        [Flags]
        public enum TestE
        {
            a=0,b=1,c=2,d=3,e=4
        }


        public struct Sample
        {
             public string Name { get; set; }
        }
        
        private class A

        {

            public  void test(int i) 
            { lock (this)
            {
                if (i > 10)
                { i--;
                Console.WriteLine(i);
                  test(i);
                 }
              }
            }
            private static void Main()

            {

                TestE testE =TestE.b|TestE.c|TestE.d;
                if ((testE & TestE.a) == TestE.a)
                {
                    Console.WriteLine(testE.ToString());
                }
                Sample sample = new Sample();
                sample.Name ="asdfa";
                Console.WriteLine(sample.Name);
                //var b = new BinaryTree();

                //var node = new Node(5);

                //b.GenerateTree(node);

                //node = new Node(13);

                //b.GenerateTree(node);

                //node = new Node(6);

                //b.GenerateTree(node);

                //node = new Node(26);

                //b.GenerateTree(node);

                //node = new Node(7);

                //b.GenerateTree(node);

                //b.Show();
                //string dd = "d";
                //string s = "";
                
               
                //var sss = object.ReferenceEquals(dd, s);
                //List<int> list = new List<int>() { 13, 8, 4, 15, 22, 5, 17, 9 };
                ////Sort.MergeSortFunction(list, 0, list.Count-1);
                //Sort.QuickSort(list,0,list.Count-1);
                //Console.WriteLine(string.Join(",",list));
                //A a = new A();
                //a.test(22);

                Queue<int> queue = new Queue<int>();
                queue.EnQueue(33);
                queue.EnQueue(44);
                queue.EnQueue(55);
                int d=  queue.DeQueue();
                d = queue.DeQueue();
                Console.WriteLine(d);
                string dd = "adsfadfadf";
                Print(dd,1);
                int result = FactorialRecurse(4,1,1);
                result = FactorialContinuation(3,x=>x);
                Console.WriteLine(result) ; 
                Console.Read();
            }
        }
        public static int FactorialContinuation(int n, Func<int, int> continuation)
        {
            if (n == 0) return continuation(1);
            return FactorialContinuation(n - 1,
                r => continuation(n * r));
        }
        public static int FibonacciContinuation(int n, Func<int, int> continuation)
        {
            if (n < 2) return continuation(n);
            return FibonacciContinuation(n - 1,
                r1 => FibonacciContinuation(n - 2,
                    r2 => continuation(r1 + r2)));
        }

         public static int FactorialRecurse(int n, int acc1, int acc2)
        {
            if (n == 0)
                return acc1;
            else
            {
                return FactorialRecurse(n - 1, acc2, n*acc2);
            }
        }
        static void Print(string text, int offset)
        {
              
            var matrix = text
                .Select((c, i) => new { Char = c, Index = i })
                .GroupBy(c => c.Index % offset, c => c.Char.ToString());
            Array.ForEach(
                matrix.ToArray(),
                a => Console.WriteLine(String.Join("|", a.Reverse().ToArray())));
        }

        #endregion

        #region Nested type: BinarySearchTree

        /// 二叉查找树
        public class BinarySearchTree

        {
            private BinarySearchTreeNode root;

            /// 生成一个二叉查找树
            public BinarySearchTree()

            {
                root = null;
            }

            /// 生成一个二叉查找树
            /// <param name="nodeValue">二叉查找树根节点的值</param>
            public BinarySearchTree(int nodeValue)

            {
                root = new BinarySearchTreeNode(nodeValue);
            }

            /// 在二叉查找树上插入一个节点
            /// <param name="nodeValue">插入节点的值</param>
            public void InsertBinarySearchTreeNode(int nodeValue)

            {
                var insertNode = new BinarySearchTreeNode(nodeValue);

                if (root == null)

                {
                    root = insertNode;

                    return;
                }

                else

                    root.InsertNode(insertNode);

                return;
            }

            /// 在二叉查找树上查询一个数
            /// <param name="searchValue">需要查询的值</param>
            /// <returns>是否找到查询的值</returns>
            public bool SearchKey(int searchValue)

            {
                if (root.key == searchValue) return true;

                else

                    return root.SearchKey(searchValue);
            }

            /// 二叉查找树中序遍历
            public void MiddleDisplay()

            {
            }

            /// 二叉查找树前序遍历
            public void FrontDisplay()

            {
                root.FrontDisplay();
                return;
            }

            /// 二叉查找树后序遍历
            public void BehindDisplay()

            {
                root.BehindDisplay();
                return;
            }

            /// 二叉查找树排序
            /// <param name="a">需要排序的数组</param>
            public static void BinarySearchTreeSort(int[] a)

            {
                var t = new BinarySearchTree();

                for (int i = 0; i < a.Length; i ++)

                    t.InsertBinarySearchTreeNode(a[i]);

                t.MiddleDisplay();
                return;
            }

            /// 二叉查找树查找
            /// <param name="a">进行查找的数组</param>
            /// <param name="searchKey">需要查找的树</param>
            public static bool BinarySearchTreeSearch(int[] a, int searchKey)

            {
                var t = new BinarySearchTree();

                for (int i = 0; i < a.Length; i ++)

                    t.InsertBinarySearchTreeNode(a[i]);

                return t.SearchKey(searchKey);
            }
        }

        #endregion

        #region Nested type: BinarySearchTreeNode

        public class BinarySearchTreeNode

        {
            public int key; // 二叉查找树节点的值

            public BinarySearchTreeNode left; // 二叉查找树节点的左子节点

            public BinarySearchTreeNode right; // 二叉查找树节点的右子节点

            /// 二叉查找树节点构造函数
            public BinarySearchTreeNode(int nodeValue)

            {
                key = nodeValue; //nodeValue 节点的值

                left = null;
                right = null;
            }

            /// 插入节点
            public void InsertNode(BinarySearchTreeNode node)

            {
                if (node.key > key)

                {
                    if (right == null)

                    {
                        right = node; //node插入的节点

                       
                    }

                    else

                        right.InsertNode(node);
                }

                else

                {
                    if (left == null)

                    {
                        left = node;
                        
                    }

                    else

                        left.InsertNode(node);
                }
            }

            /// 二叉查找树查询
            public bool SearchKey(int searchValue)

            {
                if (key == searchValue) //searchValue需要查询的值

                    return true; // 是否找到查询的值

                if (searchValue > key)

                {
                    if (right == null) return false;

                    else

                        return right.SearchKey(searchValue);
                }

                else

                {
                    if (left == null) return false;

                    else

                        return left.SearchKey(searchValue);
                }
            }

            // 中序遍历

            public void MiddleDisplay()

            {
                if (left != null)

                    left.MiddleDisplay();

                Console.WriteLine(key);

                if (right != null)

                    right.MiddleDisplay();
            }

            // 前序遍历

            public void FrontDisplay()

            {
                Console.WriteLine(key);

                if (left != null)

                    left.FrontDisplay();

                if (right != null)

                    right.FrontDisplay();
            }

            // 后序遍历

            public void BehindDisplay()

            {
                if (left != null)

                    left.BehindDisplay();

                if (right != null)

                    right.BehindDisplay();

                Console.WriteLine(key);
            }
        }

        #endregion

        #region Nested type: BinaryTree

        private class BinaryTree

        {
            private Node root;

            public void GenerateTree(Node node) //高内聚，低耦合

            {
                if (root == null)

                {
                    root = node;
                    return;
                } //如果树是空，第一次加节点

                root.Insert(node);
            }

            public void ShowInOrder(Node node) //中序遍历(in order)：左中右。先(前)序遍历(pre order)：中左右。后序遍历(post order)：左右中。

            {
                if (node == null) return; //递归必须有个终止条件,递归方法中一定要接受参数

                ShowInOrder(node.Left);

                node.Show();

                ShowInOrder(node.Right);
            }

            public void Show()

            {
                ShowInOrder(root);
            }
        }

        #endregion

        #region Nested type: Node

        private class Node

        {
            private readonly int n;

            public Node Left;

            public Node Right;

            public Node(int x)

            {
                n = x;
            }

            public void Insert(Node node)

            {
                if (node.n > n)

                {
                    if (Right == null)

                        Right = node;

                    else

                        Right.Insert(node);
                }

                else

                {
                    if (Left == null)

                    {
                        Left = node;
                    }

                    else

                    {
                        Left.Insert(node);
                    }
                }
            }

            //递归

            public void Show()

            {
              
                Console.WriteLine(n);
            }
        }

        #endregion

        public class Test : IEnumerable<string>
        {

            private Queue eQueue = new Queue();
            public IEnumerator<string> GetEnumerator()
            {
               
                IEnumerable<string> enumerable =new Test();
                yield return null;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
             
        }
    }

    public class QueueEx : Queue
    {
        public override bool IsSynchronized
        {
            get { return base.IsSynchronized; }
        }
    }

    public  class  Queue<T>
     {
         Stack<T> _inStack=new Stack<T>();

         private Stack<T> _outStack = new Stack<T>(); 
         public void EnQueue(T item)
         {
             _inStack.Push(item);
         }

         public T DeQueue()
         {
             if (_outStack.Count>0)
             {
               return  _outStack.Pop();
             }
             else
             {
                 while (_inStack.Count>0)
                 {
                     _outStack.Push(_inStack.Pop());
                 }
                return _outStack.Pop();
             }
         }
     }

}