using System;
using System.Collections.Generic;

namespace DealProcessing
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int BrandID { get; set; }
        public int Accuracy { get; set; }
        public string Aliases { get; set; }

        public Brand MyBrand { get; set; }
        
        public List<ProductModel> MyModels { get; set; }

        public List<ProductSubCategory> MySubcategories { get; set; }

        public Product()
        {
            MyModels = new List<ProductModel>();
            MyBrand = null;
            MySubcategories = new List<ProductSubCategory>();
        }

        public override string ToString()
        {
            string mdls = "";
            foreach (ProductModel mdl in MyModels)
            {
                mdls += mdl.ToString();
            }
            return string.Format("<Category id:{0} Name:{1} ali:{2} mdls:{5}>; ", ProductID, Name, Aliases, mdls);
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