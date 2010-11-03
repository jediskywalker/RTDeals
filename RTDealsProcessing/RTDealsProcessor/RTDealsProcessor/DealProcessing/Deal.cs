using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Deal
    {
        public int DealID { get; set; }
        public int SourceID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Detail { get; set; }
        public DateTime inTime { get; set; }
        public string CleanTitle { get; set; }
        public int RawDealID { get; set; }

        public int ProductID { get; set; }
        public int StoreID { get; set; }
        public string DealType { get; set; }
        public bool FreeShipping { get; set; }
        public List<SubCategory> MySubCategories { get; set; }

        public Deal()
        {
            MySubCategories = new List<SubCategory>();
        }

        public override string ToString()
        {
            return string.Format("<Deal id:{0} srcid:{1} title:{2} tm:{3} rawid:{4}>; ", DealID, SourceID, Title, inTime, RawDealID);
        }
    }
}