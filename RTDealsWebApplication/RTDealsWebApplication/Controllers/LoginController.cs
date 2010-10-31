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
using System.Xml;
using System.Xml.Linq;

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
        public ActionResult Register()
        {
            string strHostName = Dns.GetHostName(); //Get Host Name
            IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //Got IP Address
            string strAddr = ipEntry.AddressList[0].ToString();
            string url = "http://www.geobytes.com/IpLocator.htm?GetLocation&IpAddress=24.107.53.155";
            UserGeoLocator ul = new UserGeoLocator();
            ul.GetUserLocation("24.103.54.177");
            return View();
        }

        

        public ActionResult EmailVerification()
        {

            return View();

        }





        public class UserGeoLocator
        {
            private const string ApiUrl = "http://ipinfodb.com/ip_query.php?ip={0}";
            public void GetUserLocation(string ipAddress)
            {
                if (string.IsNullOrEmpty(ipAddress))
                {
                    //return null;
                }
                string reqUrl = string.Format(ApiUrl, ipAddress);
                HttpWebRequest httpReq = HttpWebRequest.Create(reqUrl) as HttpWebRequest;
                try
                {
                    string result = string.Empty;
                    HttpWebResponse response = httpReq.GetResponse() as HttpWebResponse;
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                    }
                 //   return ProcessResponse(result);

                    StringReader sr = new StringReader(result);
                    XElement respElement = XElement.Load(sr);
                    UserLocationModel ulm = new UserLocationModel();
                    ulm.City = (string)respElement.Element("City");
                    ulm.Status = (string)respElement.Element("Status");
                    ulm.CountryCode = (string)respElement.Element("CountryCode");
                    ulm.CountryName = (string)respElement.Element("CountryName");
                    ulm.ZipPostalCode = (string)respElement.Element("ZipPostalCode");
                    ulm.RegionCode = (string)respElement.Element("RegionCode");
                    ulm.RegionName = (string)respElement.Element("RegionName");
                    ulm.Latitude = (string)respElement.Element("Latitude");
                    ulm.Longitude = (string)respElement.Element("Longitude");
                    ulm.Timezone = (string)respElement.Element("Timezone");
                    ulm.Gmtoffset = (string)respElement.Element("Gmtoffset");
                    ulm.Dstoffset = (string)respElement.Element("Dstoffset");

                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            //private SiteUser ProcessResponse(string strResp)
            //{
            //    StringReader sr = new StringReader(strResp);
            //    XElement respElement = XElement.Load(sr);
            //    string callStatus = (string)respElement.Element("Status");
            //    if (string.Compare(callStatus, "OK", true) != 0)
            //    {
            //        return null;
            //    }
            //    SiteUser user = new SiteUser()
            //    {
            //        IP = (string)respElement.Element("Ip"),
            //        City = (string)respElement.Element("City"),
            //        Country = (string)respElement.Element("CountryName"),
            //        CountryCode = (string)respElement.Element("CountryCode"),
            //        RegionCode = (string)respElement.Element("RegionCode"),
            //        RegionName = (string)respElement.Element("RegionName"),
            //        PostalCode = (string)respElement.Element("ZipPostalCode"),
            //        Latitude = (decimal)respElement.Element("Latitude"),
            //        Longitude = (decimal)respElement.Element("Longitude")
            //    };
            //    return user;
            //}
        }




    }
}
