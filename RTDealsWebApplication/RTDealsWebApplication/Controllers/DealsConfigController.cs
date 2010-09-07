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
using System.Threading;



namespace RTDealsWebApplication.Controllers
{
    public class DealsConfigController : Controller
    {
        //
        // GET: /DealsConfig/

        public ActionResult Index()
        {
            ViewData["DealSource"] = DealsDB.GetAllDealSource();
            //foreach (DealsSourceModel dsm in ldsm)
            //{
                //
            //}

              //SourceRssSeedModel srs = RssSeedDB.GetSourceRssSeedByID(dsm.SourceID);
              //ViewData["AdditionalRSS"]=
            
            return View();

        }

        //
        // GET: /DealsConfig/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /DealsConfig/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /DealsConfig/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /DealsConfig/Edit/5
 
        public ActionResult Edit()
        {
            ViewData["DealSource"] = DealsDB.GetAllDealSource();
            return View();
        }

        //
        // POST: /DealsConfig/Edit/5

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {

                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void ProcessDeals()
        {
            // RegexPattern rp=new RegexPattern();
            //  List<DealsSourceModel> ldsm = DealsDB.GetAllDealSource();
            // foreach (DealsSourceModel dsm in ldsm)
            // {
            //    SendDeals.SendRTDeals(dsm.SourceName, "free");
            //   Thread.Sleep(2000);  // Use this for test purpose, can be removed after using our own email server
            //   }

            //string url = "http://feeds.feedburner.com/dealsea-latest";
            List<DealsSourceModel> ldsm = DealsDB.GetAllDealSource();
            foreach (DealsSourceModel dsm in ldsm)
            {
                string strHtml = "";
                //string Keywords = "laptop";
                try
                {

                    SourceRssSeedModel srs = RssSeedDB.GetSourceRssSeedByID(dsm.SourceID);
                    if (srs != null)
                    {
                        string url =srs.RSSAddress;
                        RTDealsWebApplication.RSS.Feed feed = new RTDealsWebApplication.RSS.Feed(url, DateTime.Parse(System.DateTime.Now.AddDays(-3).ToShortDateString()));
                        feed.Read();
                        strHtml += "[Count：" + feed.Channel.Items.Count + "]<br><br>";
                        for (int i = 0; i < feed.Channel.Items.Count; i++)
                        {
                           // if (!feed.Channel.Items[i].title.ToLower().Contains(Keywords))
                             //   continue;
                            //                        arr = feed.Channel.Items[i].title.Split(cSplit);
                            strHtml += "  <a href=" + feed.Channel.Items[i].link + " target=_blank><B>" + feed.Channel.Items[i].title + "</B></a><br>";
                            strHtml += "  <font color=red>" + feed.Channel.Items[i].pubDate + "</font><br>";
                            strHtml += "  " + feed.Channel.Items[i].description + "<br>";
                        }



                        SendEmail.SendDealsEmail("xhdf_x@hotmail.com", "rtdeals@hotmail.com", "Real Time Deals Alert @ " + dsm.SourceName, strHtml);
                        Thread.Sleep(2000);
                        // Response.Write(strHtml);
                    }
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    continue;
                }

            }

        

        }

        public string GetDealType(int id)  // Dynamiclly show sub menus
        {
            string result = "";
            if (id < 0)
                return result;

            SourceRssSeedModel srs = RssSeedDB.GetSourceRssSeedByID(id);
            string[] DealRSSArray = srs.Additional.Split(',');
            StringBuilder sb = new StringBuilder();
            sb.Append("<table><tr>");
            for (int i = 0; i < DealRSSArray.Length; i++)
            {

                string[] RSSDetals =DealRSSArray[i].Split('*');
                sb.Append("<td>" + RSSDetals[0] + "<input type='checkbox' id='ckd' name='ckd' value='" + RSSDetals[1] + "'/></td>");
            }
            sb.Append("</tr></table>");
            result = sb.ToString();
            return result;
        }


        //
        // GET: /DealsConfig/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /DealsConfig/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
