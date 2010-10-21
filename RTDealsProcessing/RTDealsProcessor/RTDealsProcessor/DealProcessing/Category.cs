using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        public bool IsSpecial { get; set; }

        public List<SubCategory> SubCategories { get; set; }

        public Category()
        {
            SubCategories = new List<SubCategory>();
        }

        public override string ToString()
        {
            string subs = "";
            foreach (SubCategory sub in SubCategories)
            {
                subs += sub.ToString();
            }
            return string.Format("<Category id:{0} Name:{1} Pri:{2} Spcl:{3} subs:{4}>; ", CategoryID, Name, Priority, IsSpecial, subs);
        }
        
    }

}