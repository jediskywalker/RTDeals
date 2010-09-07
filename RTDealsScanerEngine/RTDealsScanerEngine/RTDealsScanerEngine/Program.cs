using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;

namespace RTDealsScanerEngine
{
    class Program
    {
      // private readonly string conMysql ="server=192.168.1.107;userid=jediskywalker;Password=19810408;database=rtdeals;port=3306";
        static void Main(string[] args)
        {
            int count = 0;
          //  int SimiPecentage = 0 ;
            int PercentageLimitation = 0;
            while (DateTime.Now < DateTime.Now.AddDays(1))
            {
              if (count == 0)
               {
                   Console.Write("Please enter a number between 0-100:");
                   PercentageLimitation = Convert.ToInt16(Console.ReadLine()); 
                   //SimiPecentage = Convert.ToInt16(Console.ReadLine());
               }
                
             count++;
              // DuplicateDealsCheck.DuplicateResult(SimiPecentage);
                ScanDeals.GetRSSDeals(PercentageLimitation);
                Thread.Sleep(100000);
            }
        }






    }
}
