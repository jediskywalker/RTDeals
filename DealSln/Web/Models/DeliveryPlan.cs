using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class DeliveryPlan
    {

        public int idCustomerDeliveryPlan { get; set; }
        public int CustomerID { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool AllWeekDay { get; set; }
        public bool NightPause { get; set; }
        public bool RealTime { get; set; }
        public int Interval { get; set; }
        public DateTime LastDeliveryTime { get; set; }
        public DateTime FirstTime { get; set; }
        public DateTime SecondTime { get; set; }
        public DateTime ThirdTime { get; set; }
        public DateTime FourthTime { get; set; }
        public DateTime FifthTime { get; set; }

    }
}