using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RTDealsWebApplication.Controllers
{
    public class DeliveryPlanController : Controller
    {

        public ActionResult Test()
        {
            return View();
        }

        //
        // GET: /DeliveryPlan/

        public ActionResult Index(FormCollection collection, string submitbutton)
        {
            if(submitbutton == "Save" )
            {
                
               
                            

            }
            return View();
        }
        //weekdays=" + weekdays + "&tmpinterval=" + tmpinterval + "&times=" + times, true);
        public string InsertUpdateSchedule(string weekdays, string tmpinterval, string times, string np)
        {

            Models.DeliveryPlan tmpPlan = new Models.DeliveryPlan();

            tmpPlan.CustomerID = 7;

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

            if (!string.IsNullOrEmpty(times.Trim()))
            {
                //B: if times not empty; means fixed times            
                string[] fxtimes = times.Trim(',').Split(',');
                // order them first, then assign value by sequence
                Array.Sort(fxtimes);

                if (fxtimes.Length > 0)             
                {
                    tmpPlan.RealTime = false;
                    for (int i = 0; i < fxtimes.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                tmpPlan.FirstTime = Convert.ToDateTime(fxtimes[0]);
                                break;
                            case 1:
                                tmpPlan.SecondTime = Convert.ToDateTime(fxtimes[1]);
                                break;
                            case 2:
                                tmpPlan.ThirdTime = Convert.ToDateTime(fxtimes[2]);
                                break;
                            case 3:
                                tmpPlan.FourthTime = Convert.ToDateTime(fxtimes[3]);
                                break;
                            case 4:
                                tmpPlan.FifthTime = Convert.ToDateTime(fxtimes[4]);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            // nightpause
            tmpPlan.NightPause = (np == "true");

            // call SP to update or insert
           DBAccess.DeliverySchedule.InsertUpdateDeliveryPlan(tmpPlan);

            return "good"; 
        
        }

    }
}
