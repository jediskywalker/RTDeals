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
            RegexPattern rp=new RegexPattern();
            List<DealsSourceModel> ldsm = DealsDB.GetAllDealSource();
            foreach (DealsSourceModel dsm in ldsm)
            {
                SendDeals.SendRTDeals(dsm.SourceName, "free");
                Thread.Sleep(2000);  // Use this for test purpose, can be removed after using our own email server
            }

        }
        //test

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
