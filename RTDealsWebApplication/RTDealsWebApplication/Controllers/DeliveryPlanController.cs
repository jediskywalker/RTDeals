﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDealsWebApplication.Controllers
{
    public class DeliveryPlanController : Controller
    {
        //
        // GET: /DeliveryPlan/

        public ActionResult Index(FormCollection collection, string submitbutton)
        {
            if(submitbutton == "Save" )
            {
                
                // DBAccess.DeliverySchedule.InsertUpdateDeliveryPlan();
                            

            }
            return View();
        }
        //weekdays=" + weekdays + "&tmpinterval=" + tmpinterval + "&times=" + times, true);
        public string InsertUpdateSchedule(string weekdays, string tmpinterval, string times, string np)
        {

            Models.DeliveryPlan tmpPlan = new Models.DeliveryPlan();
            

            // day option
            string[] deliverydays = weekdays.Trim(',').Split(',');
            if (deliverydays.Length == 7)
                tmpPlan.AllWeekDay = true;  // is this flag necessary ????
            foreach(string day in deliverydays){
                switch (day)
                { 
                    case "1":
                        tmpPlan.Sunday = true;
                        break;
                    case "2":
                        tmpPlan.Monday = true;
                        break;
                    case "3":
                        tmpPlan.Tuesday = true;
                        break;
                    case "4":
                        tmpPlan.Wednesday = true;
                        break;
                    case "5":
                        tmpPlan.Thursday = true;
                        break;
                    case "6":
                        tmpPlan.Friday = true;
                        break;
                    case "7":
                        tmpPlan.Saturday = true;
                        break;
                    default:
                        break;
                }
            
            }

// time option, real or fixed

            // A: if interval > 0; means real time
            int tmpfre = Convert.ToInt32(tmpinterval);
            if (tmpfre > 0)
            {
                tmpPlan.Interval = tmpfre;
                tmpPlan.RealTime = true;
            }



            //B: if times not empty; means fixed times
            // order them first, then assign value by sequence
            string[] fxtimes = times.Trim(',').Split(',');
            if (fxtimes.Length > 0)
            {
                tmpPlan.RealTime = false;
                foreach (string time in fxtimes)
                { 
                                
                }            
            }

            // nightpause
            tmpPlan.NightPause = (np == "true");

            
            return "good"; 
        
        }

    }
}