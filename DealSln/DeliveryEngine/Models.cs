﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 9.12.2010 Li
namespace DeliveryEngine
{
    class Deals
    {
        public int dealsID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public bool Ishot { get; set; }
        public bool IsFinance { get; set; }
       // public bool IsFree { get; set; }
        public bool IsTravel { get; set; }
        public bool IsDrug { get; set; }
        public bool IsElectronic { get; set; }
        public bool IsAppliances { get; set; }
        public bool IsBeauty { get; set; }
        public bool IsOfficeSupplies { get; set; }
        public bool IsJewelry { get; set; }
        public bool IsAppeal { get; set; }
        public bool IsRestaurant { get; set; }
        public bool IsOthers { get; set; }
        public string PubDate { get; set; }
        public DateTime InDate { get; set; }

    }


    class Customers
    {
        public int CustomerID { get; set; }
        public string KeyWords { get; set; }
        public string Catekeywords { get; set; }
        public string Custkeywords { get; set; }
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
        public string Keywords { get; set; }

    }
}
