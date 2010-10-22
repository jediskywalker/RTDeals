using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealProcessing;

namespace RTDealsProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            testdb();
        }

        static void testdb()
        {
            List<Brand> brands = DealProcessingDB.GetAllBrands();

            int cnt = brands.Count;
        }
    }
}
