using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RTDealsScanerEngine.RTDealsDataAccess;
using System.Data.SqlClient;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace RTDealsScanerEngine
{
    class DuplicateDealsCheck
    {
        public static DataSet GetRtDealsDataSet()
        {
            SqlConnection conn = clsSQLControl.CreateConnection();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            conn.Open();
            string[] mysqltext = {"select * from rssdeals" };
            string[] tables = {"rssdeals" };
            for (int i = 0; i < mysqltext.Length; i++)
            {
                SqlCommand mysqlcom = new SqlCommand(mysqltext[i], conn);
                adapter.SelectCommand = mysqlcom;
                adapter.Fill(ds, tables[i]);
            }
            conn.Close();
            return ds;
        }



        public static void DuplicateResult(int SimiPecentage)
        {
            DateTime starttime = DateTime.Now;
            DataSet ds = GetRtDealsDataSet();
            Similarity s = new Similarity();
            int Total = 0;
            if (ds != null)
            {
                string Title = "";
                string SourceID = "";
                string DealsID = "";
                int Percentage = 0;


                foreach (DataRow dr in ds.Tables["rssdeals"].Rows)
                {
                    Title = dr["Title"].ToString();
                    SourceID = dr["SourceID"].ToString();
                    DealsID = dr["dealsID"].ToString();
                       

                    foreach (DataRow drr in ds.Tables["rssdeals"].Rows)
                    {
                        string Title1 = "";
                        string SourceID1 = "";
                        string DealsID1 = "";
                        Title1 = drr["Title"].ToString();
                        SourceID1 = drr["SourceID"].ToString();
                        DealsID1 = drr["dealsID"].ToString();
                        if ((DealsID != DealsID1) && (Convert.ToInt32(DealsID) < Convert.ToInt32(DealsID1)))
                        {
                            Percentage = Convert.ToInt16(s.sim(Title, Title1) * 100);
                            if (Percentage > SimiPecentage)
                            {
                                Total++;
                                Console.WriteLine(string.Format("{0}-{1} found duplicate at {2}-{3} Similarity:{4}%", SourceID, DealsID, SourceID1, DealsID1, Percentage));
                            }
                        }

                    }
                }
                Console.WriteLine("Total Duplicate Found: " + Total.ToString());
            }

            


        }



    }
}
