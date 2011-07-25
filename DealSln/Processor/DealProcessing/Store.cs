using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Store
    {
        public int StoreID { get; set; }
        public string Name { get; set; }
        public string StoreType { get; set; }
        public string Aliases { get; set; }
        public string Accuracy { get; set; }

        public List<SubCategory> MySubCategories { get; set; }

        public Store()
        {
            MySubCategories = new List<SubCategory>();
        }

        public override string ToString()
        {
            return string.Format("<Store id:{0} Name:{1} alias:{2} acc:{3}>; ", StoreID, Name, Aliases, Accuracy);
        }

    }
}