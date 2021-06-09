using System;
namespace test
{
    public class ListHT
    {
        private int size;
        private Node Head;

        public ListHT(NodeforMyList num)
        {
            Head.data = num;
            size = 1;
        }
        public ListHT()
        {
            Head = null;
            size = 0; 
        }

        public class Node
        {
            public Node pNext;
            public NodeforMyList data;
            public Node(NodeforMyList data)
            {
                this.data = data;
                this.pNext = null;
            }
        }

        public void Add(NodeforMyList node)
        {
            
            
            Node Temp = new Node(node);


            if (Head == null)
            {                
                Head = Temp;
                Head.pNext = null;
                size++;
                
            }
            else
            {
                if (String.Compare(Head.data.login, Temp.data.login) <= 0)
                {
                    Temp.pNext = Head;
                    Head = Temp;
                    size++;
                }
                else 
                {
                    Node For_search = Head;

                    while (For_search.pNext != null && String.Compare(For_search.pNext.data.login, Temp.data.login) < 0)
                    {
                        For_search = For_search.pNext;
                    }
                    if (For_search.pNext == null)
                    {
                        For_search.pNext = Temp;
                        Temp.pNext = null;
                        size++;
                    }
                    else
                    {
                        Temp.pNext = For_search.pNext;
                        For_search.pNext = Temp;
                        size++;
                    }
                }

            }
            //Console.WriteLine(node.login + " element was added");
        }

        public int GetSize() => size;

        public void Remove(NodeforMyList key)
        {
            Node Temp = Head;
            int in_list = 0;
            while (Temp != null && Temp.pNext != null )
            {
                if (Temp.data.login == key.login && Temp.data.method == key.method) in_list++;
                Temp = Temp.pNext;
            }
            if (in_list == 0)
            {
                Console.WriteLine(key +" cannot be removed");
                return;
            }
            else
            {
                if (Head.data.login == key.login && Head.data.method == key.method)
                {
                    Head = Head.pNext;
                    size--;
                }
                else
                {
                    Temp = Head;
                    Node Curr = Temp;
                    while (!(Temp.data.login == key.login && Temp.data.method == key.method))
                    {
                        Curr = Temp;
                        Temp = Temp.pNext;
                    }
                    Curr.pNext = Temp.pNext;
                    size--;

                }
                Console.WriteLine(key.login + " element was removed");
            }
        }


        public Node Search(NodeforMyList key)
        {
            Node Temp = Head;
            while (Temp != null)
            {
                if (Temp.data.login == key.login && Temp.data.method == key.method) return Temp;
                if (Temp.pNext == null) break;
                Temp = Temp.pNext;
            }
               
            return null;
            
        }

        public string Print()
        {
            if (size == 0)
            {
                return("");
            }
            Node Tmp = Head;
            string Writeline = "";
            int i = 0;
            while (i < size)
            {
                Writeline += Tmp.data + " ";
                Tmp = Tmp.pNext;
                i++;
            }

              return (Writeline);
        }

    }


}
