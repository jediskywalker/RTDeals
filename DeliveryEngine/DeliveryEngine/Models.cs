using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliveryEngine
{
    class Deals
    {
        public int dealsID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public bool Ishot { get; set; }
        public bool IsFinance { get; set; }
        public bool IsFree { get; set; }
        public bool IsTravel { get; set; }
        public bool IsDrug { get; set; }    

    }


    class Customers
    {
        public int CustomerID { get; set; }
        public string KeyWords { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }

    class ScheduledDelivery
    {
        public int scheduleID { get; set; }
        public int customerID { get; set; }
        public bool isRealTime { get; set; }
        public DateTime scheduledDeliveryTime { get; set; }
        public string dealsID { get; set; }
        public bool delivered { get; set; }
        

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
