using System;
using System.IO;
using System.Collections.Generic;


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
            inputFromFile();
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

        private bool inputFromFile()
        {
            string path = @"inputMyTable.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    //свяь логина с моим
                    string line;
                    if ((line = reader.ReadLine()) == null)
                    {
                        Console.WriteLine("No data in file");
                        return false;
                    }
                    string[] fields = line.Split(new[] { '|' });
                    if (!checkingLogin(fields[0]))
                    {
                        Console.WriteLine("Incorrect data");
                        return false;
                    }
                    string login = fields[0];
                    if (!checkTypeOfMethod(fields[1]))
                    {
                        Console.WriteLine("Incorrect type of payment");
                        return false;
                    }                    NodeforMyList salesNode = new NodeforMyList();
                    salesNode.login = login;
                    salesNode.method = fields[1];
                    hTable[hashFunction(salesNode.login, salesNode.method)].Add(salesNode);

                }
                return true;


            }
        }


        public void add(NodeforMyList node)
        {
            
            hTable[hashFunction(node.login, node.method)].Add(node);
            
        }
        public bool search(NodeforMyList node)
        {
            return (hTable[hashFunction(node.login, node.method)].Search(node) != null);
                
        }

        public void remove(NodeforMyList node, SalesTable salesTable)
        {
            hTable[hashFunction(node.login, node.method)].Remove(node);
            salesTable.

        }
        static bool checkingLogin(string s)
        {
            if ((s[0] >= 'A' && s[0] <= 'Z') || (s[0] >= 'a' && s[0] <= 'z'))
            {
                for (int i = 1; i < s.Length; i++)
                {
                    if (!((s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= 'a' && s[0] <= 'z') || s[i] == '.' || s[i] == '_' || (s[i] >= '0' && s[i] <= '9')))
                    {
                        return false;
                    }
                }
                return true;
            }
            else return false;

        }

        static bool checkTypeOfMethod(string s)
        {
            return (s == "безналичный" || s == "наличный");

        }
        static bool convertTypeOfMethod(string a)
        {
            if (a == "наличный") return false;
            else return true;
        }

    }
}
