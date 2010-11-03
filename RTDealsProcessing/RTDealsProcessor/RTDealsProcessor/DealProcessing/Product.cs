using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int BrandID { get; set; }
        public int SubCategoryID { get; set; }
        public int Accuracy { get; set; }
        public string Aliases { get; set; }

        public Brand MyBrand { get; set; }
        public SubCategory MySubcategory { get; set; }
        public List<ProductModel> MyModels { get; set; }

        public Product()
        {
            MyModels = new List<ProductModel>();
            MyBrand = null;
            MySubcategory = null;
        }

        public override string ToString()
        {
            string mods = "";
            foreach (ProductModel mod in MyModels)
            {
                mods += mod.ToString();
            }
            return string.Format("<Category id:{0} Name:{1} Pri:{2} Spcl:{3} als:{4} subs:{5}>; ", ProductID, Name, SubCategoryID, Aliases, mods);
        }

    }

    public class ProductMatch
    {
        public int ProductID { get; set; }
        public Product ProductMatched { get; set; }
        public int Accuracy { get; set; }
        public string MatchLevel { get; set; } // P: product; M: model

        public ProductMatch()
        {
            ProductMatched = null;
            MatchLevel = "P"; 
        }

        public override string ToString()
        {
            return string.Format("<Product id:{0} accu:{1} lvl:{2}>; ", ProductID, Accuracy, MatchLevel);
        }

    }

}