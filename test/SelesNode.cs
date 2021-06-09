using System;
namespace test
{
    public class SalesNode
    {
        public string login;
        public string address;
        public string nameOfProduct;
        public int price;
        public bool typeOfPayment;

        public SalesNode(string login, string address, string name, int price, bool type)
        {
            this.login = login;
            this.address = address;
            this.nameOfProduct = name;
            this.price = price;
            this.typeOfPayment = type;
        }
        public SalesNode()
        {
            this.login = "";
            this.address = "";
            this.nameOfProduct = "";
            this.price = 0;
            this.typeOfPayment = false;
        }

    }

}
