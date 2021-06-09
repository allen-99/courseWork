using System;
using System.Collections.Generic;
namespace test
{
    public enum Colour // Перечислимый тип, отвечающий за цвет узла
    {
        Red, // Красный цвет
        Black // Чёрный цвет
    }
    public struct Data
    {
        public int valueOfPrice;
        public List<SalesNode> indexList;
    };

    public class RedBlackTree
    {
        public class Node // Класс узла
        {
            public Colour color;      // Поле цвета
            public Node left;        // Поле-узел слева
            public Node right;        // Поле-узел справа
            public Node parent;        // Поле-узел, являющийся родителем
            public ushort count = 1;    // Счётчик вставок узла с данным ключом в дерево
            public  Data data; 

            public Node(int money, SalesNode indexInList)
            {
                this.data.indexList = new List<SalesNode> { };
                this.data.valueOfPrice = money;
                this.data.indexList.Add(indexInList);             
            }
            public Node(int money)
            {
                this.data.indexList = new List<SalesNode> { };
                this.data.valueOfPrice = money;
            }
            public Node(Colour color)
            {
                this.data.indexList = new List<SalesNode> { };
                this.color = color;
            }
            public Node(int money, Colour color, SalesNode indexInList)
            {
                this.data.indexList = new List<SalesNode> { };
                this.data.valueOfPrice = money;
                this.data.indexList.Add(indexInList);
                this.color = color;
            }
            public bool IsLess(Node node2)
            {
                return (this.data.valueOfPrice < node2.data.valueOfPrice);
            }
            public bool IsMore(Node node2)
            {
                return (this.data.valueOfPrice > node2.data.valueOfPrice);
            }
            public bool IsEqual(Node node2)
            {
                return (this.data.valueOfPrice == node2.data.valueOfPrice);
            }
        }
        public Node root; // Узел-корень дерева
        private Node nil;

        public RedBlackTree()
        {
            nil = new Node(Colour.Black);
            nil.parent = nil;
            nil.left = nil;
            nil.right = nil;
            nil.data.valueOfPrice = 0;
            nil.data.indexList = new List<SalesNode> { };
            root = nil;

        }
        public void Clear()
        {
            Console.WriteLine("Clearing the tree.");
            while (root != nil)
                Delete(root);
            DisplayTree();
        }

        private void LeftRotate(Node X)
        {
            Node Y = X.right; // set Y
            X.right = Y.left; // turn Y's left subtree into X's right subtree

            if (Y.left != nil)
                Y.left.parent = X;

            if (Y != nil)
                Y.parent = X.parent; // link X's parent to Y

            if (X.parent == nil)
                root = Y;
            else if (X == X.parent.left)
                X.parent.left = Y;
            else
                X.parent.right = Y;

            Y.left = X; // put X on Y's left

            if (X != nil)
                X.parent = Y;
        }

        private void RightRotate(Node Y)
        {
            Node X = Y.left;
            Y.left = X.right;

            if (X.right != nil)
                X.right.parent = Y;

            if (X != nil)
                X.parent = Y.parent;

            if (Y.parent == nil)
                root = X;
            else if (Y == Y.parent.right)
                Y.parent.right = X;
            else
                Y.parent.left = X;

            X.right = Y; // put Y on X's right

            if (Y != nil)
                Y.parent = X;
        }

        public Node Find(int money)
        {
            bool isFound = false;
            Node temp = root;
            Node node = new Node(money);

            while (!isFound)
            {
                if (temp == nil)
                    break;
                if (node.IsLess(temp))
                    temp = temp.left;
                else if (node.IsMore(temp))
                    temp = temp.right;
                else
                    isFound = true;
            }

            if (isFound)
                return temp;
            else
                return nil;
        }

        public Node FindMinimum(Node node)
        {
            Node temp = node;

            if (temp == nil)
                return nil;

            while (temp.left != nil)
                temp = temp.left;

            return temp;
        }

        public void DisplayTree()
        {
            if (root == nil)
            {
                Console.WriteLine("The tree is empty.");
                return;
            }
            if (root != nil)
            {
                Display(root, 0);
                Console.WriteLine("____________________________________");
            }
        }

        private void Display(Node current, int n)
        {
            if (current != nil)
            {
                Display(current.right, n + 1);

                for (int i = 0; i < n; i++)
                    Console.Write("  ");
                Console.Write("{0}", current.data);
                if (current.color == Colour.Black)
                    Console.WriteLine(" (B, {0})", current.count);
                else
                    Console.WriteLine(" (R, {0})", current.count);

                Display(current.left, n + 1);
            }
        }

        public void Insert(int money, SalesNode index)
        { 
            if (this.Find(money) != nil)
            {
                this.Find(money).data.indexList.Add(index);
                return;
            }

            Node Z = new Node(money,index);
            Node Y = nil;
            Node X = root;

            while (X != nil)
            {
                Y = X;
                if (Z.IsEqual(X))
                {
                    X.count++;
                    return;
                }
                else if (Z.IsLess(X))
                    X = X.left;
                else
                    X = X.right;
            }
            Z.parent = Y;

            if (Y == nil)
                root = Z;
            else if (Z.IsLess(Y))
                Y.left = Z;
            else
                Y.right = Z;

            Z.left = nil;
            Z.right = nil;
            Z.color = Colour.Red; // colour the new node red

            InsertFixUp(Z); // call method to check for violations and fix
        }

        private void InsertFixUp(Node Z)
        {
            while (Z != root && Z.parent.color == Colour.Red)
            {
                if (Z.parent == Z.parent.parent.left)
                {
                    Node Y = Z.parent.parent.right;

                    if (Y.color == Colour.Red) // Case 1: uncle is red
                    {
                        Z.parent.color = Colour.Black;
                        Y.color = Colour.Black;
                        Z.parent.parent.color = Colour.Red;
                        Z = Z.parent.parent;
                    }

                    else // Case 2: uncle is black
                    {
                        if (Z == Z.parent.right)
                        {
                            Z = Z.parent;
                            LeftRotate(Z);
                        }
                        // Case 3: recolour & rotate
                        Z.parent.color = Colour.Black;
                        Z.parent.parent.color = Colour.Red;
                        RightRotate(Z.parent.parent);
                    }
                }
                else
                {
                    Node X = Z.parent.parent.left;

                    if (X.color == Colour.Red) // Case 1
                    {
                        Z.parent.color = Colour.Black;
                        X.color = Colour.Black;
                        Z.parent.parent.color = Colour.Red;
                        Z = Z.parent.parent;
                    }

                    else // Case 2
                    {
                        if (Z == Z.parent.left)
                        {
                            Z = Z.parent;
                            RightRotate(Z);
                        }
                        // Case 3 
                        Z.parent.color = Colour.Black;
                        Z.parent.parent.color = Colour.Red;
                        LeftRotate(Z.parent.parent);
                    }
                }
            }

            root.color = Colour.Black; // re-colour the root black as necessary
        }

        public void Delete(int money)
        {
            Node Z = Find(money);
            Delete(Z);
        }

        public void Delete(Node Z)
        {
            Node Y = Z;
            Node X = nil;
            Colour SavedColor = Y.color;

            if (Z.count > 1)
            {
                Z.count--;
                return;
            }
            if (Z == nil)
            {
                Console.WriteLine("Nothing to delete!");
                return;
            }
            if (Z.left == nil)
            {
                X = Z.right;
                Transplant(Z, Z.right);
            }
            else if (Z.right == nil)
            {
                X = Z.left;
                Transplant(Z, Z.left);
            }
            else
            {
                Y = FindMinimum(Z.right);
                Console.WriteLine("Minimum {0} was found.", Y.data);
                if (Y == nil)
                {
                    Console.WriteLine("Node does not have minimum.");
                    return;
                }
                SavedColor = Y.color;
                X = Y.right;
                if (Y.parent == Z)
                    X.parent = Y;
                else
                {
                    Transplant(Y, Y.right);
                    Y.right = Z.right;
                    Y.right.parent = Y;
                }
                Transplant(Z, Y);

                Y.left = Z.left;
                Y.left.parent = Y;
                Y.color = Z.color;
            }

            if (SavedColor == Colour.Black)
                DeleteFixUp(X);
        }

        private void Transplant(Node X, Node Y)
        {
            if (X.parent == nil)
                root = Y;
            else if (X == X.parent.left)
                X.parent.left = Y;
            else
                X.parent.right = Y;

            Y.parent = X.parent;
        }

        private void DeleteFixUp(Node X)
        {
            while (X != root && X.color == Colour.Black)
            {
                Node Y = X.parent;
                if (X == Y.left)
                {
                    Node W = Y.right;

                    if (W.color == Colour.Red)
                    {
                        W.color = Colour.Black; //case 1
                        Y.color = Colour.Red;
                        LeftRotate(Y);
                        W = Y.right;
                    }

                    if (W.left.color == Colour.Black && W.right.color == Colour.Black)
                    {
                        W.color = Colour.Red; //case 2
                        X = Y;
                    }
                    else
                    {
                        if (W.right.color == Colour.Black)
                        {
                            W.left.color = Colour.Black; //case 3
                            W.color = Colour.Red;
                            RightRotate(W);
                            W = Y.right;
                        }

                        W.color = Y.color; //case 4
                        Y.color = Colour.Black;
                        W.right.color = Colour.Black;
                        LeftRotate(Y);
                        X = root;
                    }
                }
                else //mirror code from above with "right" & "left" exchanged
                {
                    Node W = Y.left;

                    if (W.color == Colour.Red)
                    {
                        W.color = Colour.Black;
                        Y.color = Colour.Red;
                        RightRotate(Y);
                        W = Y.left;
                    }

                    if (W.right.color == Colour.Black && W.left.color == Colour.Black)
                    {
                        W.color = Colour.Red;
                        X = Y;
                    }
                    else
                    {
                        if (W.left.color == Colour.Black)
                        {
                            W.right.color = Colour.Black;
                            W.color = Colour.Red;
                            LeftRotate(W);
                            W = Y.left;
                        }

                        W.color = Y.color;
                        Y.color = Colour.Black;
                        W.left.color = Colour.Black;
                        RightRotate(Y);
                        X = root;
                    }
                }
            }

            X.color = Colour.Black;
        }

       
        public virtual void PrintRange(Node node, int k1, int k2)
        {

            /* base case */
            if (node == nil)
            {
                return;
            }

            /* Since the desired o/p is sorted, recurse for left subtree first
             If root->data is greater than k1, then only we can get o/p keys
             in left subtree */
            if (k1 < node.data.valueOfPrice)
            {
                PrintRange(node.left, k1, k2);
            }

            /* if root's data lies in range, then prints root's data */
            if (k1 <= node.data.valueOfPrice && k2 >= node.data.valueOfPrice)
            {
                for (int i = 0; i < node.data.indexList.Count; i++)
                    //Console.Write(SalesTable.sales[node.data.indexList[i]].nameOfProduct + " ");
                    Console.Write(node.data.indexList[i].nameOfProduct + " ");
            }

            /* If root->data is smaller than k2, then only we can get o/p keys
             in right subtree */
            if (k2 > node.data.valueOfPrice)
            {
                PrintRange(node.right, k1, k2);
            }
        }
    }
}



