using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string BrandID { get; set; }
        public int SubCategoryID { get; set; }

        public List<ProductModel> Models { get; set; }

        public Product()
        {
            Models = new List<ProductModel>();
        }

        public override string ToString()
        {
            string mods = "";
            foreach (ProductModel mod in Models)
            {
                mods += mod.ToString();
            }
            return string.Format("<Category id:{0} Name:{1} Pri:{2} Spcl:{3} subs:{4}>; ", ProductID, Name, SubCategoryID, mods);
        }

    }

}