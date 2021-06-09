using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace test
{
    public class SalesTable
    {
        public static string[] dormitory = new string[18] { "город", "корпус 11", "корпус 10", "корпус 9", "корпус 8.1", "корпус 8.2", "корпус 7.1", "корпус 7.2", "корпус 6.1", "корпус 6.2", "корпус 1.10", "корпус 2.1", "корпус 2.2", "корпус 2.3", "корпус 2.4", "корпус 2.5", "корпус 2.6", "корпус 2.7" };

        public static List<SalesNode> sales;

        public RedBlackTree redBlackTree;



        public SalesTable(MyTable myTable)
        {
            sales = new List<SalesNode> { };
            redBlackTree = new RedBlackTree();
            inputFromFile(myTable);

        }

        public void addNewSales(MyTable myTable, string login, string address, string name, int price, string type)
        {
            
            if (checkingAdress(address) && checkingLogin(login)
                && checkingNameOfProduct(name) && checkRangeOfPrice(price) && checkTypeOfMethod(type))
            {
                SalesNode salesNode = new SalesNode { };
                salesNode.login = login;
                salesNode.address = address;
                salesNode.nameOfProduct = name;
                salesNode.price = price;
                salesNode.typeOfPayment = convertTypeOfMethod(type);
                NodeforMyList nodeforMyList = new NodeforMyList(login, type);
                if (!myTable.search(nodeforMyList))
                {
                    Console.WriteLine("this sales isn't in my list");
                    return;
                }
                sales.Add(salesNode);
                redBlackTree.Insert(salesNode.price, salesNode);
            }
            else
            {
                Console.WriteLine("invalid data");
            }

        }
        public void removeSales(string login, string type)
        {

        }
        
        public void createRBTree()
        {
            for (int i = 0; i < sales.Count; i++)
                redBlackTree.Insert(sales[i].price, sales[i]);
        }

        
        public void findRangeRBTree(int min, int max)
        {
            redBlackTree.PrintRange(redBlackTree.root, min, max);
        }

        private void inputFromFile(MyTable myTable)
        {
            string path = @"inputMainTable.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    //свяь логина с моим
                    string line;
                    if ((line = reader.ReadLine()) == null)
                    {
                        Console.WriteLine("No data in file");
                        continue;
                    }
                    string[] fields = line.Split(new[] { '|' });
                    /*string: login location(with one space) name price(int) pay(bool)*/
                    /*login*/
                    if (!checkingLogin(fields[0]))
                    {
                        Console.WriteLine("Incorrect data");
                        continue;
                    }
                    string login = fields[0];
                    if (!checkingAdress(fields[1]))
                    {
                        Console.WriteLine("Incorrect file");
                        continue;
                    }
                    string location = fields[1];
                    if (!checkingNameOfProduct(fields[2]))
                    {
                        Console.WriteLine("Incorrect name of product");
                        continue;
                    }
                    string nameOfProduct = fields[2];
                    int price = Convert.ToInt32(fields[3]);
                    if (!checkRangeOfPrice(price))
                    {
                        Console.WriteLine("Incorrect range of price");
                        continue;
                    }
                    if (!checkTypeOfMethod(fields[4]))
                    {
                        Console.WriteLine("Incorrect type of payment");
                        continue;
                    }
                    bool typeOfPayment = convertTypeOfMethod(fields[4]);
                    NodeforMyList nodeforMyList = new NodeforMyList(fields[0], fields[4]);
                    if (!myTable.search(nodeforMyList))
                    {
                        Console.WriteLine($"there is no { fields[0]} person in my table");
                        continue;
                    };
                    SalesNode salesNode = new SalesNode();
                    salesNode.login = login;
                    salesNode.address = location;
                    salesNode.nameOfProduct = nameOfProduct;
                    salesNode.price = price;
                    salesNode.typeOfPayment = typeOfPayment;
                    sales.Add(salesNode);
                }
                createRBTree();
            }
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
        static bool checkingAdress(string s)
        {
            int count = 0;
            for (int i = 0; i < dormitory.Length; i++)
            {
                if (s == dormitory[i]) count++;
            }
            if (count == 1) return true;
            else return false;
        }
        static bool checkingNameOfProduct(string s)
        {
            if ((s[0] >= 'А' && s[0] <= 'Я') || (s[0] >= 'а' && s[0] <= 'я'))
            {
                for (int i = 1; i < s.Length; i++)
                {
                    if (!((s[i] >= 'А' && s[i] <= 'Я') || (s[i] >= 'а' && s[0] <= 'я') || s[i] == '.' || s[i] == ' ' || s[i] == '-' || (s[i] >= '0' && s[i] <= '9')))
                    {
                        return false;
                    }
                }
                return true;
            }
            else return false;
        }
        static bool checkRangeOfPrice(int a)
        {
            return (a >= 0 && a <= 999999);
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
