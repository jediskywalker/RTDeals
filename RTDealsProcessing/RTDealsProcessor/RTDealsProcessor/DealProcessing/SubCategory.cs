using System;
using System.Collections.Generic;


namespace DealProcessing
{
    public class SubCategory
    {
        public int SubCategoryID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }


        public override string ToString()
        {
            return string.Format("<SubCategory id:{0} name:{1} catid:{2}>; ", SubCategoryID, Name, CategoryID);
        }
    }

}