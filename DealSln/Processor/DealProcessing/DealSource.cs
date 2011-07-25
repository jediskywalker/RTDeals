using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class DealSource
    {
        public int SourceID { get; set; }
        public string SourceName { get; set; }
        public int StoreID { get; set; }

        public List<SubCategory> MySubCategories { get; set; }

        public DealSource()
        {
            MySubCategories = new List<SubCategory>();
        }

        public override string ToString()
        {
            return string.Format("<Store id:{0} Name:{1} subs:{2}>; ", SourceID, SourceName, StoreID);
        }

    }

}