using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealProcessing
{
    public class ProductSubCategory
    {
        public int ProductID { get; set; }
        public int SubCategoryID { get; set; }
        public int Accuracy { get; set; }

        public override string ToString()
        {
            return string.Format("<ProductSubCategory pid:{0} subid:{1} acc:{2}>; ", ProductID, SubCategoryID, Accuracy);
        }
    }
}
