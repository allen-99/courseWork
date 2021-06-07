using System;


namespace test
{
    public class MyTable
    {
        static int SIZE = 10;
        public ListHT[] hTable;

        
        public MyTable()
        {
            hTable = new ListHT[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                hTable[i] = new ListHT();
            }
        }

        private int hashFunction(string login, string method)
        {
            int asciiOfString = 0;
            for (int i = 0; i < login.Length; i++)
            {
                asciiOfString += (int)login[i];
            }
            for (int i = 0; i < method.Length; i++)
            {
                asciiOfString += (int)method[i];
            }
            return (asciiOfString % SIZE);
        }

        public void add(NodeforMyList node)
        {
            
            hTable[hashFunction(node.login, node.method)].Add(node);
            //comment
            
        }

        public void remove(NodeforMyList node)
        {

        }

    }
}
