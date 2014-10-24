namespace DataStucture
{
    using System.Runtime.CompilerServices;
    public class TreeNode
    {
        public int Data { get; set; }

        public TreeNode LeftNode { get; set; }

        public TreeNode RightNode { get; set; }
    }

    public class BinaryTree
    {
        public TreeNode root;

        public BinaryTree()
        {
            root = null;
        }
       

        public void Insert(int value)
        {
            var newNode = new TreeNode {Data = value};
            if (root == null)
            {
                root = newNode;
            }
            else
            {
                TreeNode parent = root;
                while (true)
                {
                    if (value < parent.Data)
                    {
                        if (parent.LeftNode == null)
                        {
                            parent.LeftNode = newNode;
                            break;
                        }
                        else
                        {
                            parent = parent.LeftNode;
                        }
                    }
                    else
                    {
                        if (parent.RightNode == null)
                        {
                            parent.RightNode = newNode;
                            break;
                        }
                        else
                        {
                            parent = parent.RightNode;
                        }
                    }
                }
            }
        }

        public int FindMin()
        {
            TreeNode node = root;
            while (node.LeftNode != null)
            {
                node = node.LeftNode;
            }
            return node.Data;
        }

        public int FindMax()
        {
            TreeNode node = root;
            while (node.RightNode != null)
            {
                node = node.RightNode;
            }
            return node.Data;
        }

        public TreeNode Find(int key)
        {
            TreeNode node = root;
            while (true)
            {
                if (key > node.Data)
                {
                    node = node.RightNode;
                }
                else
                {
                    node = node.LeftNode;
                }
                if (node == null)
                {
                    return null;
                }
                if (node.Data == key)
                    return node;
            }
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool Delete(int key)
        {
            TreeNode node = root;
            TreeNode parent = null;
            bool isLeftChild = false;

            while (node.Data != key)
            {
                parent = node;
                if (key < node.Data)
                {
                    node = node.LeftNode;
                    isLeftChild = true;
                }
                else
                {
                    node = node.RightNode;
                    isLeftChild = false;
                }
                if (node == null)
                {
                    return false;
                }
            }
            if (node.LeftNode == null && node.RightNode == null)
            {
                if (node == root)
                {
                    root = null;
                }
                else
                {
                    if (isLeftChild)
                    {
                        parent.LeftNode = null;
                    }
                    else
                    {
                        parent.RightNode = null;
                    }
                }
            }
            else if (node.LeftNode == null)
            {
                if (node == root)
                {
                    root = node.RightNode;
                }
                else
                {
                    if (isLeftChild)
                    {
                        parent.LeftNode = node;
                    }
                    else
                    {
                        parent.RightNode = node;
                    }
                }
            }
            else
            {
                if (node.RightNode == null)
                {
                    if (node == root)
                    {
                        root = node.LeftNode;
                    }
                    else
                    {
                        if (isLeftChild)
                        {
                            parent.LeftNode = node.LeftNode;
                        }
                        else
                        {
                            parent.RightNode = node.RightNode;
                        }
                    }
                }
                else
                {
                    TreeNode succesor = FindSuceesor(node);
                    if (node == root)
                    {
                        root = succesor;
                    }
                    else
                    {
                        if (isLeftChild)
                        {
                            parent.LeftNode = succesor;
                        }
                        else
                        {
                            parent.RightNode = succesor;
                        }
                    }
                }
            }
            return true;
        }


        private TreeNode FindSuceesor(TreeNode delNode)
        {
            TreeNode succesorParent = delNode;

            TreeNode succesor = delNode.RightNode;
            while (succesor != null)
            {
                succesorParent = succesor;

                succesor = succesor.LeftNode;
            }
            if (succesor != delNode.RightNode)
            {
                succesor.RightNode = delNode.RightNode;

                succesorParent.LeftNode = succesor.RightNode;
            }
            succesor.LeftNode = delNode.LeftNode;
            return succesor;
        }
    }
}