using System;
namespace test
{
    public class NodeforMyList
    {
        public string login;
        public string method;
        public NodeforMyList()
        {
            login = "";
            method = "";
        }
        public NodeforMyList(string login, string method)
        {
            this.login = login;
            this.method = method;
        }
    }
}
