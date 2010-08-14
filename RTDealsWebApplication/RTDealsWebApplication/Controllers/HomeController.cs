﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDealsWebApplication.Controllers
{
    //Test Purpose
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session["Customer"] = null;
            return RedirectToAction("Index", "Home");
        }

    }
}
