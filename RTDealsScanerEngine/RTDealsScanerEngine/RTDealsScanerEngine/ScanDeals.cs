using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RTDealsScanerEngine.RTDealsDataAccess;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;



namespace RTDealsScanerEngine
{
    class ScanDeals
    {

        public static DataSet GetRtDealsDataSet()
        {
            MySqlConnection conn = clsSQLControl.CreateConnection();
           // DBUtils db = new DBUtils();
            DataSet ds = new DataSet();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            conn.Open();
            string[] mysqltext = { "select * from sourcerssseed order by SourceID","select * from rssdeals where DATE_ADD(inTime,INTERVAL 3 DAY)>NOW()" };
            string[] tables = { "sourcerssseed","rssdeals" };
            for (int i = 0; i < mysqltext.Length; i++)
            {
                MySqlCommand mysqlcom = new MySqlCommand(mysqltext[i], conn);
                adapter.SelectCommand = mysqlcom;
                adapter.Fill(ds, tables[i]);
            }
            conn.Close();
            return ds;
        }


        public static void GetRSSDeals(int PercentageLimitation)
        {
                DateTime starttime = DateTime.Now;
                DataSet ds = GetRtDealsDataSet();
                Similarity s=new Similarity();
                string strHtml="";
                int TotalInsert = 0;
                int TotalFailed = 0;
                int TotalDuplicate = 0;
                int TotalSimilar = 0;
                int k = 0;  //For SubCount
                string[] SubSourceName=new string[ds.Tables["sourcerssseed"].Rows.Count];
                int[] SubInsert = new int[ds.Tables["sourcerssseed"].Rows.Count];
                int[] SubDuplicate = new int[ds.Tables["sourcerssseed"].Rows.Count];
                int[] SubFailed = new int[ds.Tables["sourcerssseed"].Rows.Count];
                string SimilarDuplicateResult="";
                string SourceFailedResult = "";
                if (ds != null)
                {
                     foreach (DataRow dr in ds.Tables["sourcerssseed"].Rows)
                     {
                   
                             int id = Convert.ToInt16(dr["SourceID"]);
                             string Additional = dr["Additional"].ToString();
                             string[] AddiURL = Additional.Split(',');
                             SubSourceName[k]=id.ToString();
                             for (int i = 0; i < AddiURL.Length; i++)
                             {
                                 try
                                 {
                                     if (AddiURL[i] == "")
                                         continue;

                                     string[] temp = AddiURL[i].Split('*');
                                     string RssType = temp[0];
                                     string RssUrl = temp[1];
                                  
                                     List<RssDealsModel> lrd = new List<RssDealsModel>();
                                     RSS.Feed feed = new RSS.Feed(RssUrl, DateTime.Parse(System.DateTime.Now.AddDays(-3).ToShortDateString()));
                                     feed.Read();
                                     for (int j = 0; j < feed.Channel.Items.Count; j++)
                                     {
                                 
                                         strHtml += "  <a href=" + feed.Channel.Items[j].link + " target=_blank><B>" + feed.Channel.Items[j].title + "</B></a><br>";
                                         strHtml += "  <font color=red>" + feed.Channel.Items[j].pubDate + "</font><br>";
                                         strHtml += "  " + feed.Channel.Items[j].description + "<br>";
                                         // if (strHtml.Length >= 8000)
                                         // strHtml = strHtml.Substring(0, 7999);
                                         strHtml = "";
                                         int ishot = 0;
                                         int isFinance = 0;
                                         int isFree = 0;
                                         int isTravel = 0;
                                         int isDrug = 0;
                                         int isElectronic = 0;

                                         if (RssType.ToLower().Contains("hot"))
                                             ishot = 1;
                                         else if (RssType.ToLower().Contains("finance"))
                                             isFinance = 1;
                                         else if (RssType.ToLower().Contains("free"))
                                             isFree = 1;
                                         else if (RssType.ToLower().Contains("travel"))
                                             isTravel = 1;
                                         else if (RssType.ToLower().Contains("drug"))
                                             isDrug = 1;
                                         else if (RssType.ToLower().Contains("electronic"))
                                             isElectronic = 1;
                                         if (ds.Tables["rssdeals"].Rows.Count == 0)
                                         {
                                             string ss = InsertRssDeals(id, feed.Channel.Items[j].title, feed.Channel.Items[j].link, 0, ishot, feed.Channel.Items[j].link, strHtml, feed.Channel.Items[j].pubDate, isFinance, isFree, isTravel, isDrug, isElectronic);
                                             
                                         }
                                        
                                         int count = 0;
                                         bool isSimilar = false;
                                         bool isExist = false;
                                         string Title = "";
                                         string SourceID="";
                                         string DealsID="";
                                         //string UniqueParameter="";
                                         int Percentage = 0;
                                        
                                          isExist = isRssDealExist(feed.Channel.Items[j].link);

                                          if (!isExist) //Skip Compare if already exsit
                                          {
                                              ds = GetRtDealsDataSet();
                                              foreach (DataRow drr in ds.Tables["rssdeals"].Rows)
                                              {
                                               
                                                  Title = drr["Title"].ToString();
                                                  SourceID = drr["SourceID"].ToString();
                                                  DealsID = drr["dealsID"].ToString();
                                                  count++;
                                                  if (SubSourceName[k] == SourceID)
                                                      continue; //Skipped Compare with its' Source


                                                  Percentage = Convert.ToInt16(s.sim(feed.Channel.Items[j].title, Title) * 100);    //get_semblance_By_2words(feed.Channel.Items[j].title, Title);
                                                  if (Percentage >= 65)
                                                  {
                                                      isSimilar = true;
                                                      break;  //Stop Comparation
                                                  }
                                                  else
                                                  {
                                                      isSimilar = false;
                                                  }
                                                  //  }
                                              }
                                          }

                                         if (isSimilar || isExist)
                                         {
                                             if (isExist)
                                             {
                                                 Console.ForegroundColor = ConsoleColor.DarkCyan;
                                                 SimilarDuplicateResult = string.Format(feed.Channel.Items[j].title + " Found Duplicate Record At {0}", DateTime.Now.ToString());
                                             }
                                             else
                                             {
                                                 TotalSimilar++;
                                                 Console.ForegroundColor = ConsoleColor.Cyan;
                                                 SimilarDuplicateResult = string.Format(feed.Channel.Items[j].title + " from {2} Found Duplicate Record {3}% At {0}-{1}: {4}", SourceID, DealsID, SubSourceName[k], Percentage, DateTime.Now.ToString());
                                             }
                                             TotalDuplicate++;
                                             SubDuplicate[k]++;
                                             
                                             Console.WriteLine(SimilarDuplicateResult);
                                             continue;  //Fetch next Node

                                         }
                         
                                       
                                                 string result = InsertRssDeals(id, feed.Channel.Items[j].title, feed.Channel.Items[j].link, 0, ishot, feed.Channel.Items[j].link, strHtml, feed.Channel.Items[j].pubDate, isFinance, isFree, isTravel, isDrug,isElectronic);
                                                 string resultHistory = InsertRssDealsHistory(id, feed.Channel.Items[j].title, feed.Channel.Items[j].link, 0, ishot, feed.Channel.Items[j].link, strHtml, feed.Channel.Items[j].pubDate, isFinance, isFree, isTravel, isDrug,isElectronic);
                      
                                               
                                                     
                                                 if (result == "")
                                                 {
                                                     TotalInsert++;
                                                     SubInsert[k]++;
                                                     Console.ForegroundColor = ConsoleColor.White;
                                                     Console.WriteLine(feed.Channel.Items[j].title + " Inserted Successfully! from "+ SubSourceName[k] +" At " + DateTime.Now.ToShortTimeString());
                                                 }
                                                 else
                                                 {
                                                     TotalFailed++;
                                                     SubFailed[k]++;
                                                     Console.ForegroundColor = ConsoleColor.Red;
                                                     Console.WriteLine(result + " At " + DateTime.Now.ToShortTimeString());
                                                 }

                                                 //Thread.Sleep(100);

                                         
                                            
                                       
                                     
                                       
                                     }
                                 }
                                 catch (Exception ex)
                                 {
                                     
                                     string ss = ex.Message;
                                     SourceFailedResult += string.Format("{0} {1} At {2}***", SubSourceName[k], ss, DateTime.Now.ToString());
                                     //continue;
                                 }

                             }


                             k++;
                     }
                   
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(string.Format("Total Inserted: {0}, Total Duplicate: {1}, Total Failed: {2}", TotalInsert, TotalDuplicate, TotalFailed));
                for (int m = 0; m < ds.Tables["sourcerssseed"].Rows.Count; m++)
                {
                    Console.WriteLine(string.Format("{0} Inserted: {1}, Duplicate: {2}, Failed: {3}", SubSourceName[m], SubInsert[m], SubDuplicate[m], SubFailed[m]));
                }
           // string[] SourceErrors=Regex.Split(SourceFailedResult,"***");
         //   for (int p = 0; p < SourceErrors.Length; p++)
          //  {
          //      Console.ForegroundColor = ConsoleColor.White;
          //      Console.WriteLine(SourceErrors[p]);
         //   }
            Console.WriteLine("Total similar deals found : " + TotalSimilar.ToString());
            Console.WriteLine("Scan Time is : " + (DateTime.Now - starttime).Minutes + " Minutes " + (DateTime.Now - starttime).Seconds + " Seconds ");
        }

        private static int SimilarWordsCompare(string p, string Title)
        {
            throw new NotImplementedException();
        }


        public static string InsertRssDeals(int Sourceid, string title, string url, int? rate, int isHot, string UniqueParameter, string SendContent, string publicDate, int isFinance, int isFree, int isTravel, int isDrug,int isElectronic)
        {
            
          //  MySqlConnection conn = clsSQLControl.CreateConnection();

            string mysql = "INSERT INTO rssdeals(sourceid, title, url, inTime, rate, UniqueParameter, SendContent,PubDate,ishot,isFinance,isFree,isTravel,isDrug,isElectronic) ";
            mysql += "VALUES ( '" + Sourceid + "' , '" + title + "' , '" + url + "' , " + "NOW()" + " , '" + rate + "' , '" + UniqueParameter + "' , '" + SendContent + "' , '" + publicDate + "' , '" + isHot + "' , '" + isFinance + "' , '" + isFree + "' , '" + isTravel + "' , '" + isDrug + "','" + isElectronic + "')";

            try
            {
                DBUtils db = new DBUtils();
                db.ExecuteNonQuery(mysql);
                db.CloseConnection();
                return "";
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex.Message;
            }
        }


        public static string InsertRssDealsHistory(int Sourceid, string title, string url, int? rate, int isHot, string UniqueParameter, string SendContent, string publicDate, int isFinance, int isFree, int isTravel, int isDrug, int isElectronic)
        {

            //  MySqlConnection conn = clsSQLControl.CreateConnection();

            string mysql = "INSERT INTO rssdealsHistory(sourceid, title, url, inTime, rate, UniqueParameter, SendContent,PubDate,ishot,isFinance,isFree,isTravel,isDrug,isElectronic) ";
            mysql += "VALUES ( '" + Sourceid + "' , '" + title + "' , '" + url + "' , " + "NOW()" + " , '" + rate + "' , '" + UniqueParameter + "' , '" + SendContent + "' , '" + publicDate + "' , '" + isHot + "' , '" + isFinance + "' , '" + isFree + "' , '" + isTravel + "' , '" + isDrug + "','" + isElectronic + "')";

            try
            {
                DBUtils db = new DBUtils();
                db.ExecuteNonQuery(mysql);
                db.CloseConnection();
                return "";
            }
            catch (Exception ex)
            {
                //throw ex;
                return ex.Message;
            }
        }




        public static bool isRssDealExist(string UniqueParameter)
        {
            MySqlConnection conn=clsSQLControl.CreateConnection();
            conn.Open();
            MySqlCommand mysql =conn.CreateCommand();
            mysql.CommandText= "select * from rssdeals where UniqueParameter='" + UniqueParameter + "'";
            MySqlDataReader reader =mysql.ExecuteReader();
            if (reader.Read())
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
         
        }

        public static int get_semblance_By_2words(string word1, string word2)
        {
            int count = 0;
            int re = 0;
            int maxLength;
            int i, l;
            List<string> tb1 = new List<string>();
            List<string> tb2 = new List<string>();
            i = 0;
            l = 1;
            maxLength = word1.Length;
            if (word1.Length < word2.Length)
                maxLength = word2.Length;
            while (l <= word1.Length)
            {
                while (i < word1.Length - 1)
                {
                    if (i + l > word1.Length)
                        break;
                    tb1.Add(word1.Substring(i, l));
                    i++;
                }
                i = 0;
                l++;
            }

            i = 0;
            l = 1;

            while (l <= word2.Length)
            {
                while (i < word2.Length - 1)
                {
                    if (i + l > word2.Length)
                        break;
                    tb2.Add(word2.Substring(i, l));
                    i++;
                }
                i = 0;
                l++;
            }
            int jumper = 0;
            foreach (string subStr in tb1)
            {
                int tempRe = 0;
               
                if (jumper == subStr.Length)
                    continue;

                if (tb2.Contains(subStr))
                {
                    count++;
                    tempRe = subStr.Length * 100 / maxLength;
                    if (tempRe > re)
                    {
                        re = tempRe;
                        jumper = subStr.Length;
                       // continue;
                    }
                    if (tempRe == 100)
                        break;
                }
            }
            int s = count;
            return re;//re.ToString() + "%";
        }






     
    }
}
