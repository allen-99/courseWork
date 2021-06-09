using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            MyTable myTable = new MyTable { };
            SalesTable salesTable = new SalesTable(myTable) { };
            salesTable.addNewSales(myTable,"login", "город", "что-то с чем-то", 105, "безналичный");
            salesTable.findRangeRBTree(100, 1000);
  
            NodeforMyList nodeforMyList = new NodeforMyList("logina", "безналичный");
            myTable.add(nodeforMyList);
            myTable.add(nodeforMyList);
            myTable.add(nodeforMyList);


        }
    }
}
