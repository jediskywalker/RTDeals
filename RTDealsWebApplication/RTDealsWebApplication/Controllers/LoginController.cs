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

namespace WebSite.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            ViewData["Message"] = "";
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(string UserName, string Password)
        {
            string _userName = UserName.Trim();
            string _password = Password.Trim();

          //  CustomerModel customer=CustomerDB.getCustomer
            CustomerModel customer = CustomerDB.getCustomerByUsername(_userName);
            if (customer != null && customer.Password == Password &&  customer.Status == "A")
            {
                Session["Customer"] = customer;

                return RedirectToAction("Index", "Customer");
            }

            ViewData["Message"] = "Failed to log in";
            ViewData["Username"] = _userName;

            return View();
        }

    }
}
