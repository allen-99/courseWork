using System;
namespace hash_table
{
    partial class List
    {
        private int size;
        private Node Head;

        public List(int num)
        {
            Head.data = num;
            size = 1;
        }
        public List()
        {
            Head = null;
            size = 0; 
        }

        public class Node
        {
            public Node pNext;
            public int data;
            public Node(int data)
            {
                this.data = data;
                this.pNext = null;
            }
        }

        public void Add(int key)
        {
            
            
            Node Temp = new Node(key);


            if (Head == null)
            {                
                Head = Temp;
                Head.pNext = null;
                size++;
                
            }
            else
            {
                if (Head.data >= Temp.data)
                {
                    Temp.pNext = Head;
                    Head = Temp;
                    size++;
                }
                else 
                {
                    Node For_search = Head;

                    while (For_search.pNext != null && For_search.pNext.data < Temp.data)
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
            Console.WriteLine(key + " element was added");
        }

        public int GetSize() => size;

        public void Remove(int key)
        {
            Node Temp = Head;
            int in_list = 0;
            while (Temp != null && Temp.pNext != null )
            {
                if (Temp.data == key) in_list++;
                Temp = Temp.pNext;
            }
            if (in_list == 0)
            {
                Console.WriteLine(key +" cannot be removed");
                return;
            }
            else
            {
                if (Head.data == key)
                {
                    Head = Head.pNext;
                    size--;
                }
                else
                {
                    Temp = Head;
                    Node Curr = Temp;
                    while (Temp.data < key)
                    {
                        Curr = Temp;
                        Temp = Temp.pNext;
                    }
                    Curr.pNext = Temp.pNext;
                    size--;

                }
                Console.WriteLine(key + " element was removed");
            }
        }


        public Node Search(int key)
        {
            Node Temp = Head;
            while (Temp != null)
            {
                if (Temp.data == key) return Temp;
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
