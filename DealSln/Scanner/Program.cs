using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
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
            try
            {
                while (DateTime.Now < DateTime.Now.AddDays(1))
                {
                    if (count == 0)
                    {
                        Console.Write("Please enter a number between 0-100:");
                        PercentageLimitation = Convert.ToInt16(Console.ReadLine());
                        //SimiPecentage = Convert.ToInt16(Console.ReadLine());
                    }

                    //int ssss = 100 / count;
                    count++;
                    // DuplicateDealsCheck.DuplicateResult(SimiPecentage);

                    ScanDeals.GetRSSDeals(PercentageLimitation);
                    Console.WriteLine(count.ToString() + " round(s)");
                    Thread.Sleep(2400);

                   // Thread.Sleep(1200);
                    //for (int i = 60; i >= 0; i--)
                    //{

                    //     Console.Write(i);
                    //     System.Threading.Thread.Sleep(1000);
                    //     Console.Clear();
                    //     i = 60;
                    // }

                }
            }
            catch(Exception e)
            {
                SendEmail.SendDealsEmail("xhdf_x@hotmail.com", "rtdeals@hotmail.com", "Scaner Error @ " + DateTime.Now.ToString(),e.Message);


            }
        }


    }
}
