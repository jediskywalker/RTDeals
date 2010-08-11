using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using RTDealsWebApplication.Models;
using RTDealsWebApplication.DBAccess;
using RTDealsWebApplication.Common;


namespace RTDealsWebApplication.Common
{
    public class SendDeals
    {

        public static void SendRTDeals(string DealSourceName,string Keywords)
        {
            RegexPattern rp=new RegexPattern();
            List<RegexPatternModel> lrpm = DealsDB.GetDealPetternByName(DealSourceName);
            //For Test Purpose
            //lrpm[0].TitlePattern = rp.headlinedealsTitlePattern; 
            //lrpm[0].ValuePattern = rp.headlinedealsValuePattern;  
            List<DealsSourceModel> ldsm = DealsDB.GetDealsSourceByName(DealSourceName);
            string DealsURL = ldsm[0].SourceURL;
            HttpWebRequest MyDealRequest = (HttpWebRequest)WebRequest.Create(DealsURL);
            MyDealRequest.Method = "GET";
            WebResponse MyDealResponse = MyDealRequest.GetResponse();
            StreamReader sr = new StreamReader(MyDealResponse.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            result = ExtractSubstring(result, lrpm[0].TitlePattern, lrpm[0].ValuePattern);
            string[] ResultArray = Regex.Split(result, " SplitFromHere");
            sr.Close();
            MyDealResponse.Close();
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");

            for (int i = 0; i < ResultArray.Length; i++)
            {
                if (ResultArray[i].ToLower().Contains(Keywords.ToLower()) &&  (lrpm[0].ExcludePattern==null?true :!ResultArray[i].ToLower().Contains(lrpm[0].ExcludePattern)))
                {
                    if(lrpm[0].ReplacementPattern!=null)
                      ResultArray[i] = Regex.Replace(ResultArray[i], lrpm[0].ReplacementPattern, DealsURL + "/" + lrpm[0].ReplacementPattern);
                    if (!sb.ToString().Contains(ResultArray[i]))
                        sb.Append("<tr><td>" + ResultArray[i] + "</td></tr>");
                }
            }
            sb.Append("</table>");
            //Send Deals Email
            if (sb.ToString() != "<table></table>")
                SendEmail.SendDealsEmail("xhdf_x@hotmail.com", "rtdeals@hotmail.com", "Real Time Deals Alert @ "+DealSourceName, sb.ToString());

        }

        public static string ExtractSubstring(string stringinput,string TitlePattern,string ValuePattern)
        {
            Regex DealTitleRegex = new Regex(TitlePattern, System.Text.RegularExpressions.RegexOptions.Singleline);
            string thedealtitle = "";
            string subString = "";
            if (DealTitleRegex.IsMatch(stringinput))
            {
                subString = DealTitleRegex.Match(stringinput).Value;

            }
            Regex reg1 = new Regex(ValuePattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            MatchCollection matches1 = reg1.Matches(subString);
            int k = 0; //Count
            foreach (Match match in matches1)
            {
                k++;
                if (k < matches1.Count)
                    thedealtitle += match.Groups[1].Value + " SplitFromHere";
                else
                    thedealtitle += match.Groups[1].Value;
            }

            return thedealtitle;
        }

        public static bool IsMatchKeywords(string inputString, string keywords)
        {






            return false;
        }




        public static string GetUniqueParameters()
        {


            return "";

        }







    }
}